#! /usr/bin/env raku

sub normalize-roll(Int:D $roll, Int:D :$min = 0, Int :$max --> Int:D) is export {
  my $res = max $roll, $min;
  $res = min $roll, $max with $max;

  $res
}
