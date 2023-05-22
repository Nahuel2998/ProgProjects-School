#! /usr/bin/env raku
use v6.d;

use Presetables;

our %cards is export = %(
  "Pudding" => 
    CardPreset.new( :name( "Pudding" ), 
                    :description( "Fully restore HP." ), 
                    :level-req( 4 ),
                    :star-cost( 0 ),
                    :type( CardType::Boost ),
                    :action(
                      -> :$player!, *%_ {
                        $player.heal($player.max-hp);
                      }
                    ),
                  ),
  "Saki's Cookie" => 
    CardPreset.new( :name( "Saki's Cookie" ), 
                    :description( "Heals 1 HP." ), 
                    :level-req( 1 ),
                    :star-cost( 0 ),
                    :type( CardType::Boost ),
                    :action(
                      -> :$player!, *%_ {
                        $player.heal( 1 );
                      }
                    ),
                  ),
  "Dash!" => 
    CardPreset.new( :name( "Dash!" ), 
                    :description( "For this turn, roll two dice for movement." ), 
                    :level-req( 1 ),
                    :star-cost( 3 ),
                    :type( CardType::<Boost> ),
                    :action(
                      -> :$player!, *%_ {
                        $player.dice-count<MOVE> = 2;
                      }
                    ),
                  ),
  "Nice Present" => 
    CardPreset.new( :name( "Nice Present" ), 
                    :description( "Draw 2 cards." ), 
                    :level-req( 2 ),
                    :star-cost( 10 ),
                    :type( CardType::Boost ),
                    :action(
                      -> :$player!, *%_ {
                        $player.draw( 2 );
                      }
                    ),
                  ),
  "Nice Jingle" => 
    CardPreset.new( :name( "Nice Jingle" ), 
                    :description( "Stock effect.\nThe next bonus panel gives you twice as many stars." ), 
                    :level-req( 1 ),
                    :star-cost( 0 ),
                    :type( CardType::Boost ),
                    :action(
                      -> :$player!, *%_ {
                        # TODO: Effect representation for the gui and stuff
                        #       When done, remove from player effects list
                        $player.log.grep({ .<event> eq 'BONUS_PANEL' }).head(1)
                               .tap(-> $_ { $player.gain-stars( .<amount> ) });
                      }
                    ),
                  ),
  "Flip Out" => 
    CardPreset.new( :name( "Flip Out" ), 
                    :description( "Stock effect.\nNext time you land on a drop panel, the player(s) with the highest number of stars will lose the same number of stars as you." ), 
                    :level-req( 1 ),
                    :star-cost( 0 ),
                    :type( CardType::Boost ),
                    :action(
                      -> :$player!, :$board!, *%_ {
                        # TODO: Effect representation for the gui and stuff
                        #       When done, remove from player effects list
                        my $max-stars = $board.players.map(*.stars).max;
                        $player.log.grep({ .<event> eq 'DROP_PANEL' }).head(1)
                               .tap(-> $ev { .lose-stars( $ev.<amount> ) for $board.players.grep({ $_ !=== $player && .stars == $max-stars }) });
                      }
                    ),
                  ),
);
