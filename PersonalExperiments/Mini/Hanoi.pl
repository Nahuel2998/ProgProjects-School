use strict;
use warnings;
no warnings 'uninitialized';
use Data::Dumper;
use feature 'say';

my $iters;
my $height = shift;
my @arrs = ( [ reverse 1..$height ], [ ], [ ] );
my @discs = map { center(make_disc($_), $height + 2) } 0..$height;

sub Hanoi
{
  $iters++;
  my ($disc, $from, $to, $aux) = @_;

  if ($disc == 1)
  { 
    push $to->@*, pop $from->@*; 
    PrintDiscs();
    return;
  }

  Hanoi($disc - 1, $from, $aux, $to);
  push $to->@*, pop $from->@*; 
  PrintDiscs();
  Hanoi($disc - 1, $aux, $to, $from);
}

PrintDiscs();
Hanoi($height, @arrs);
# say $iters;
# say Dumper @arrs;

sub make_disc
{
  my $length = shift;

  return ' ' unless $length;

  my $half_of = '#' x ($length / 2);
  return $length % 2 == 0 ? $half_of . $length . $half_of : '|' . $half_of . $length . $half_of . '|';
}

sub center 
{
  local $_ = shift;
  my $length = shift;

  my $padding = ' ' x ((($length-length) + 1) / 2);

  return $padding . $_ . $padding;
}

sub PrintDiscs
{
  for (reverse 0..($height - 1))
  {
    say $discs[$arrs[0][$_]]. 
        $discs[$arrs[1][$_]]. 
        $discs[$arrs[2][$_]];
  }
  say '- ' x ($height * 3);
}
