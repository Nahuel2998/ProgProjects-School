#! /usr/bin/env raku
use v6.d;

use lib $*PROGRAM.sibling('lib');
use lib $*PROGRAM.sibling('lib/Preset');
use lib $*PROGRAM.sibling('lib/Util');
use Classes;
use Presetables;
use Preset::Panels;
use Preset::Boards;
use Preset::Cards;
use Util::BoardBuilder;
use Util::Log;

my $card-preset = CardPreset.new( :name( "Hola" ), 
                                  :description( "yes" ), 
                                  :star-cost( 4 ),
                                  :level-req( 2 ),
                                  :type( CardType::Boost ),
                                  :action( { say "hola" } )
                                  :tags( <pudding sweet>.Set ),
                                );

my $card = Card.new( :preset( $card-preset ) );

# say $card.raku;
# $card.actions<Boost>();

my @panels = make-board(%boards{'Icy Hideout'});

my $char = Character.new( :name( "Enrique" ),
                          :description( "el ta" ),
                          :atk( 4 ),
                          :def(-1 ),
                          :evd( 1 ),
                          :rec( 5 ),
                          :hp( 4 ),
                          :hyper( $card-preset ),
                        );

my @cards = %cards.values.map({ Card.new(:preset($_)) });

my $board = Game.new( :board(@panels), 
                      :board-str(printable-board(@panels)),
                      :draw-pile(@cards),
                    );

my @players = gather { 
  take Player.new( :number( $_ + 1 ),
                   :$board,
                   :$char, 
#                   :dice-range( ^40 ),
                   :stars( 500 ),
                 ) for ^4 };

$board.players = @players;

my $player = $board.players[0];

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

say $board.board-str;

# Logger
# $board.log.sup.tap(-> $_ { .put });

say "\n====[ Player ]====";

%cards{'Flip Out'}.action(:$player, :$board);

$player.do-move;

# say "Stars: {$_.stars}" for $board.players;
say "Stars: {$player.stars}";
say "Cards: {$player.deck}";
say "At: {$player.x}, {$player.y}";
