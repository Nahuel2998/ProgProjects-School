#! /usr/bin/env raku

use Classes;
use Terminal::ANSIColor;

our %panels is export = %(
  '_' => PanelPreset.new( :repr( '_' ), 
                          :name( "Blank" ), 
                          :description( "It's boring." ),
                          :action( -> *%_ { } ),
                        ),
  'W' => PanelPreset.new( :repr( colored('W', 'bold magenta') ), 
                          :name( "Warp" ), 
                          :description( "Warps the player to another warpy panel." ),
                          :action( 
                            -> :$player!, :$board!, *%_ {
                              $player.warp-to: $board.panels.grep({ $_.tags<warpy> && $_ !=== $player.position }).roll;
                            } ),
                          :tags( <warpy>.Set ),
                        ),
  'D' => PanelPreset.new( :repr( colored('D', 'bold green') ), 
                          :name( "Draw" ), 
                          :description( "Draws one card." ),
                          :action(
                            -> :$player!, :$board!, *%_ {
                              $player.draw;
                            }
                          ),
                        ),
  '+' => PanelPreset.new( :repr( colored('+', 'bold yellow') ), 
                          :name( "Bonus" ), 
                          :description( "Gives the player diceroll * level(max 3) stars." ),
                          :action( 
                            -> :$player!, :$board!, *%_ {
                              my $amount = $player.ask-rolldice('BONUS') * min($player.level, 3);

                              $player.gain-stars: $amount;
                              $board.log.(:event('BONUS_PANEL'), :$amount, :$player);
                            } 
                          ),
                        ),
  '-' => PanelPreset.new( :repr( colored('-', 'bold blue') ), 
                          :name( "Drop" ), 
                          :description( "Drops diceroll * level player stars." ),
                          :action(
                            -> :$player!, :$board!, *%_ {
                              my $amount = $player.ask-rolldice('DROP') * $player.level;

                              $player.lose-stars: $amount;
                              $board.log.(:event('DROP_PANEL'), :$amount, :$player);
                            } 
                          ),
                        ),
  'E' => PanelPreset.new( :repr( colored('E', 'bold red') ), 
                          :name( "Blank" ), 
                          :description( "It's boring." ),
                          :action( -> *%_ { } ),
                        ),
  '1' => PanelPreset.new( :repr( colored('1', 'bold white') ), 
                          :name( "Home[1]" ), 
                          :description( "Home of Player 1." ),
                          :action(
                            -> :$player!, *%_ {
                              $player.heal( 1 );

                              if $player.number == 1 {
                                $player.norma-check;
                              }
                            }
                          ),
                          :tags( <home1>.Set ),
                        ),
  '2' => PanelPreset.new( :repr( colored('2', 'bold white') ), 
                          :name( "Home[2]" ), 
                          :description( "Home of Player 2." ),
                          :action(
                            -> :$player!, *%_ {
                              $player.heal( 1 );

                              if $player.number == 2 {
                                $player.norma-check;
                              }
                            }
                          ),
                          :tags( <home2>.Set ),
                        ),
  '3' => PanelPreset.new( :repr( colored('3', 'bold white') ), 
                          :name( "Home[3]" ), 
                          :description( "Home of Player 3." ),
                          :action(
                            -> :$player!, *%_ {
                              $player.heal( 1 );

                              if $player.number == 3 {
                                $player.norma-check;
                              }
                            }
                          ),
                          :tags( <home3>.Set ),
                        ),
  '4' => PanelPreset.new( :repr( colored('4', 'bold white') ), 
                          :name( "Home[4]" ), 
                          :description( "Home of Player 4." ),
                          :action(
                            -> :$player!, *%_ {
                              $player.heal( 1 );

                              if $player.number == 4 {
                                $player.norma-check;
                              }
                            }
                          ),
                          :tags( <home4>.Set ),
                        ),
                      );