#! /usr/bin/env raku

use lib $*PROGRAM.sibling('lib');
use Classes;
use Panels;
use BoardBuilder;
use Boards;

my $card-preset = CardPreset.new( :name( "Hola" ), 
                                  :description( "yes" ), 
                                  :star-cost( 4 ), 
                                  :level-req( 2 ),
                                  :actions( {
                                    CardType::<Boost> => { say "hola" }
                                  } )
                                  :tags( <pudding sweet>.Set ),
                                );

my $card = Card.new( :preset( $card-preset ) );

# say $card.raku;
# $card.actions<Boost>();

my @panels = make-board(%boards{'Pudding Chase'});

my $char = Character.new( :name( "Enrique" ),
                          :description( "el ta" ),
                          :atk( 4 ),
                          :def(-1 ),
                          :evd( 1 ),
                          :rec( 5 ),
                          :hp( 4 ),
                          :hyper( $card-preset ),
                        );

my $board = Game.new( :board(@panels), 
                      :draw-pile($card),
                    );

my $player = Player.new( :number( 1 ), 
                         :board( $board ),
                         :$char, 
                         :dice-range( 0..40 ),
                         :dice-count( 4 ),
                       );

# say $player.hp;
#     $player.hp = 0;
# say $player.hp;
#     $player.revive;
# say $player.hp;
#     $player.hp = 2;
# say $player.hp;
#     $player.heal(5);
# say $player.hp;

say "====[ Board ]====";

say $board.board;

say "\n====[ Player ]====";

$player.walk(1);
$player.walk(3);
$player.walk(1);

say "Stars: {$player.stars}";
say "Cards: {$player.deck}";
say "At: {$player.x}, {$player.y}";

