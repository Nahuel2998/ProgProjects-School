#! /usr/bin/env raku

use Classes;
use Panels;
use Terminal::ANSIColor;

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

# Hey I wanted to try this gather take thing
sub printable-board(@board) is export {
  gather {
    for ^@board -> $i {
      take '', gather {
        for ^@board[$i] -> $j {
          next if $i - 1 < 0;
          my $curr = @board[$i][$j];
          if $curr ~~ Panel {
            take (given @board[$i - 1][$j] {
              when * (elem) $curr.next     { '↑' }
              when * (elem) $curr.previous { '↓' }
              default { ' ' }
            });
          } else { take ' ' }
        }
      }.join: '   ';
      take gather {
        given @board[$i][0] { take $_ ~~ Panel ?? .repr !! '   ' }
        for ^@board[$i] -> $j {
          next if $j - 1 < 0;
          my $curr = @board[$i][$j];
          if $curr ~~ Panel {
            take (given @board[$i][$j - 1] {
              when * (elem) $curr.next     { '←' }
              when * (elem) $curr.previous { '→' }
              default { ' ' }
            });
            take @board[$i][$j].repr;
          } else { take '    ' }
        }
      }.join;
    }
  }.join: "\n"
}