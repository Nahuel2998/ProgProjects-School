#! /usr/bin/env raku

use Classes;
use Panels;

sub make-board(Str $board-str) is export {
  my @auxboard = $board-str.lines>>.comb;

  my @board;

  # Build panels
  for @auxboard.kv -> $y, @lines {
    for @lines.kv -> $x, $panel {
      next if ($x + $y) % 2 || $panel eq ' ';

      @board[$y / 2][$x / 2] = Panel.new( :x( $x div 2 ), 
                                          :y( $y div 2 ), 
                                          :preset( %panels{$panel} ),
                                        );
    }
  }

  # Make connections
  for @auxboard.kv -> $y, @lines {
    for @lines.kv -> $x, $dir {
      next if ($x + $y) %% 2;

      given $dir {
        when '>' { 
          make-connection( @board[$y / 2][($x - 1) / 2], @board[$y / 2][($x + 1) / 2] );
        }
        when '<' {
          make-connection( @board[$y / 2][($x + 1) / 2], @board[$y / 2][($x - 1) / 2] );
        }
        when 'v' {
          make-connection( @board[($y - 1) / 2][$x / 2], @board[($y + 1) / 2][$x / 2] );
        }
        when '^' {
          make-connection( @board[($y + 1) / 2][$x / 2], @board[($y - 1) / 2][$x / 2] );
        }
      }
    }
  }

  return @board;
}

sub make-connection(Panel $from, Panel $to) {
  $from.next.push: $to;
  $to.previous.push: $from;
}
