#! /usr/bin/env raku

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
  has Set $.tags .= new;     # Card-specific tags. Example: pudding, sweet
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
  has Str  $.repr; # Character representation
  has Code $.action;

  has Set  $.tags .= new;
}

class Panel is export {
  has Int $.x is required;
  has Int $.y is required;
  has PanelPreset $.preset is required;

  has @.next     of Panel;
  has @.previous of Panel;

  has @.players  of Player;

  method action { $!preset.action }
  method repr   { $!preset.repr   }
  method tags   { $!preset.tags   }

  has SetHash[Effect] $.effects .= new;
}

### CharacterStuff
class Character does Descriptable is export {
  has Int $.atk is required;
  has Int $.def is required;
  has Int $.evd is required;
  has Int $.rec is required;
  has Int $.hp  is required;

  has Int $.card-limit = 3;

  has CardPreset $.hyper is required;

  has Passive $.passive;
}

### PlayerStuff
class Player is export {
  has Int  $.number is required;
  has Game $.board  is required;

  has Int $.level  is rw = 1;
  has Int $.stars  is rw = 0;
  has Int $.wins   is rw = 0;

  has Character $.char is required;

  has Int $.hp  is rw = $!char.hp;
  has Int $.rec is rw = $!char.rec;

  has Int $.atk is rw = $!char.atk;
  has Int $.def is rw = $!char.def;
  has Int $.evd is rw = $!char.evd;

  has Panel $.position is rw = $!board.panels.List.flat.grep({ $_ ~~ Panel && $_.tags{"home$!number"} })[0];

  has @.deck of Card;
  has SetHash[Effect] $.effects .= new;

  has Range $.dice-range is rw = 1..6;
  has Int   $.dice-count is rw = 1;
  has Int   $.direction  is rw = 0; # Even number is forwards, odd is backwards

  method x(--> Int:D) { $!position.x }
  method y(--> Int:D) { $!position.y }
  method dead(--> Bool:D) { !$!hp }

  method turn-start {
    $!atk = $!char.atk;
    $!def = $!char.def;
    $!evd = $!char.evd;
    $!dice-range = 1..6;
    $!dice-count = 1;
    $!direction  = 0;

    for $.effects.keys {
      $_.action(:player(self));
      $_.uses-left--;
    }
  }

  method gain-stars(Int $amount where * >= 0) {
    $!stars += $amount;
  }

  method lose-stars(Int $amount where * >= 0) {
    $!stars = max($!stars - $amount, 0);
  }

  method heal(Int $amount) {
    $!hp = min($!hp + $amount, $!char.hp);
  }

  method hurt(Int $amount) {
    $!hp = max($!hp - $amount, 0);
  }

  method draw {
    @!deck.push( $!board.draw-pile.pop ) if $!board.draw-pile;
  }

  method discard(Int $index) {
    my $card = @!deck.splice: $index, 1;

    $!board.discard-pile.push( $card ) unless $card.tags<deletable>;
  }

  method warp-to(Panel $panel) {
    $!position = $panel;
  }

  method revive {
    $!hp  = $!char.hp;
    $!rec = $!char.rec;
  }

  method walk(Int $times where * > 0) {
    for ^$times {
      my @next = $!direction %% 2 ?? $!position.next !! $!position.previous;

      $!position = @next == 1 ?? @next[0] !! @next[self.ask-nextpanel(@next)];
    }

    $!position.action.(:player(self), :board($!board));
  }

  method ask-rolldice(--> Int:D) {
    prompt "Enter to roll!";

    my $res = [+] $!dice-range.roll($!dice-count);
    say "Rolled: $res";

    $res
  }

  # TODO: Actually implement this
  method ask-pickcard(--> Int:D) {
    my Int $pick = prompt "Pick a card: ";
    
    (^@!deck).pick
  }

  # TODO: Actually implement this
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

  has @.draw-pile    of Card;
  has @.discard-pile of Card;

  has Int $.chapter = 1;
  
  has SetHash[Effect] $.events .= new;

  method panels { @!board.List.flat.grep({ $_ ~~ Panel }) }
  method board  { @!board>>.map({ $_ ~~ Panel ?? .repr !! ' ' }).join: "\n" }
}
