#! /usr/bin/env raku

use Log;

### Class declarations
class Card   { ... }
class Panel  { ... }
class Player { ... }
class Game   { ... }

### General roles/classes
role Descriptable is export {
  has Str $.name        is required;
  has Str $.description is required;
}

#| Executed at the start of every turn.
class Passive does Descriptable is export {
  has Code $.action is required; 
  has Int  $.max-stack;
  has Int  $.stack = 1;
}

#| A passive with a limited amount of uses.
class Effect is Passive is export {
  has Int $.uses-left is rw;
}

### CardStuff
enum CardType is export <Battle Boost Event Gift Trap>;

class CardPreset does Descriptable is export {
  has Int $.star-cost is required;
  has Int $.level-req is required;
  has     %.actions   of Code is required;
  has Set $.tags .= new; # Card-specific tags. Example: pudding, sweet
}

class Card is export {
  has CardPreset $.preset is required;
  has %.flags is rw;  # Card state

  method star-cost { $!preset.star-cost }
  method level-req { $!preset.level-req }
  method actions   { $!preset.actions   }
  method tags      { $!preset.tags      }
}

### PanelStuff
class PanelPreset does Descriptable is export {
  has Str  $.repr   is required; # Character representation
  has Code $.action = -> *%_ { }; 
  has Code $.step   = -> *%_ { };

  has Set  $.tags .= new;
}

class Panel is export {
  has Int $.x is required;
  has Int $.y is required;
  has PanelPreset $.preset is required;

  has @.next     of Panel;
  has @.previous of Panel;

  has SetHash[Player] $.players .= new;

  method action {    $!preset.action  }
  method step   {    $!preset.step    }
  method repr   { " {$!preset.repr} " }
  method tags   {    $!preset.tags    }

  has SetHash[Effect] $.effects .= new;
}

### CharacterStuff
class Character does Descriptable is export {
  has  Int $.atk is required;
  has  Int $.def is required;
  has  Int $.evd is required;
  has  Int $.rec is required;
  has UInt $.hp  is required;

  has  Int $.mov         = 0;
  has UInt $.card-limit  = 3;

  has CardPreset $.hyper is required;

  has Passive $.passive;
}

### PlayerStuff
class Player is export {
  has UInt $.number is required;
  has Game $.board  is required;
  
  has Supply $.log = $!board.log.sup({ .<player> === self });

  has UInt $.level  is rw = 1;
  has UInt $.stars  is rw = 0;
  has UInt $.wins   is rw = 0;

  has Character $.char is required;

  has  Int $.hp  is rw = $!char.hp;
  has  Int $.rec is rw = $!char.rec;

  has UInt $.max-hp is rw = $!char.hp;

  has  Int $.atk is rw = $!char.atk;
  has  Int $.def is rw = $!char.def;
  has  Int $.evd is rw = $!char.evd;

  has  Int $.mov        is rw = $!char.mov; 
  has UInt $.card-limit is rw = $!char.card-limit;

  has Panel $.position is rw = $!board.panels.grep({ $_.tags{"home$!number"} })[0];

  has @.deck of Card;
  has SetHash[Effect] $.effects .= new;

  has Range $.dice-range is rw = 1..6;
  has %.dice-count is rw = %(
    DEFAULT => 1,
    BONUS   => 1,
    DROP    => 1,
    MOVE    => 1,
    ATTACK  => 1,
    DEFEND  => 1,
    EVADE   => 1,
    REVIVE  => 1,
  );
  has Int   $.direction  is rw = 0; # Even number is forwards, odd is backwards

  method x(--> Int:D) { $!position.x }
  method y(--> Int:D) { $!position.y }
  method dead(--> Bool:D) { !$!hp }

  method turn-start {
    $!atk        = $!char.atk;
    $!def        = $!char.def;
    $!evd        = $!char.evd;
    $!mov        = $!char.mov;
    $!card-limit = $!char.card-limit;
    $!max-hp     = $!char.hp;
    $!dice-range = 1..6;
    %!dice-count = %(
      DEFAULT => 1,
      BONUS   => 1,
      DROP    => 1,
      MOVE    => 1,
      ATTACK  => 1,
      DEFEND  => 1,
      EVADE   => 1,
      REVIVE  => 1,
    );
    $!direction  = 0;

    # for $.effects.keys {
    #   $.effects.unset($_) and next unless .uses-left;

    #   .action(:player(self));
    # }
  }

  method gain-stars(UInt:D $amount) {
    $!stars += $amount;
  }

  method lose-stars(UInt:D $amount) {
    $!stars = max($!stars - $amount, 0);
  }

  method heal(UInt:D $amount = 1) {
    $!hp = min($!hp + $amount, max($!max-hp, $!hp));
  }

  method hurt(UInt:D $amount = 1) {
    $!hp = max($!hp - $amount, 0);
  }

  method draw(UInt:D $times where * > 0 = 1) {
    for ^$times {
      @!deck.push( $!board.draw-pile.pop ) if $!board.draw-pile;
    }
  }

  method discard(UInt:D $index) {
    my $card = @!deck.splice: $index, 1;

    $!board.discard-pile.push( $card ) unless $card.tags<deletable>;
  }

  method warp-to(Panel:D $panel) {
    $!position.players.unset(self);
    $!position = $panel;
    $!position.players.set(self);
  }

  method revive {
    $!hp  = $!char.hp;
    $!rec = $!char.rec;
  }

  method walk(UInt:D $times is copy where * > 0 = 1) {
    $!position.players.unset(self);

    while $times <-> $steps-left {
      my @next = $!direction %% 2 ?? $!position.next !! $!position.previous;

      last unless @next;

      $!position = @next == 1 ?? @next[0] !! @next[self.ask-nextpanel(@next)];
      
      $!position.step.(:player(self), :$!board, :$steps-left);
      $steps-left--;
    }

    $!position.players.set(self);
    $!position.action.(:player(self), :$!board);
  }

  method ask-rolldice(Str:D $event = "DEFAULT", UInt:D $dice-multiplier where * > 0 = 1 --> Int:D) {
    prompt "Enter to roll!";

    my $res = [+] $!dice-range.roll(%!dice-count{$event} * $dice-multiplier);
    say "Rolled: $res";

    $res
  }

  # TODO: Actually implement this
  method ask-pickcard(--> Int:D) {
    my Int $pick = prompt "Pick a card: ";
    
    (^@!deck).pick
  }

  # FIXME: Make this more friendly?
  method ask-nextpanel(@next --> Int:D) {
    my @choices = @next.map({ 
      .x > self.x ?? "Right" !!
      .x < self.x ?? "Left"  !!
      .y > self.y ?? "Down"  !!
                     "Up"
    });
    my Int $pick = prompt "Where to go ($(@choices)): ";
    
    $pick.Int
  }

  # TODO: Implement me!
  method norma-check {

  }
}

### GameStuff
class Game is export {
  has @.players[4]   of Player;
  has @.board        is required;
  has $.board-str    is required; # FIXME: ugly
  
  has Log $.log .= new;

  has @.draw-pile    of Card;
  has @.discard-pile of Card;

  has Int $.chapter = 1;
  
  has SetHash[Effect] $.effects .= new;

  method panels { @!board.List.flat.grep({ $_ ~~ Panel }) }
}
