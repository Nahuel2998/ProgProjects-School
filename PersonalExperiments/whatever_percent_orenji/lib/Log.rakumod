#! /usr/bin/env raku

enum GameEvent is export <BONUS_PANEL DROP_PANEL DRAW_PANEL CARD_USED BATTLE_START BATTLE_END TURN_START TURN_END>;

class Log is export {
  has Supplier $.supplier .= new;
  
  method log(Str :$event! where * ~~ GameEvent::, *%data) { 
    %data<event> = $event;
    $!supplier.emit(%data);
  }
  
  submethod CALL-ME(Str :$event! where * ~~ GameEvent::, *%data) {
    self.log(:$event, |%data);
  }
  
  multi method sup(--> Supply:D) { $.supplier.Supply }
  multi method sup(Mu $filter --> Supply:D) { self.sup.grep($filter) }
  multi method sup(Mu $filter, Int $times --> Supply:D) { self.sup($filter).head($times) }
}