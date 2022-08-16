#!/usr/bin/perl
use strict;
use warnings;
use feature 'say';
use Data::Dumper;
use List::Util 'shuffle';
use File::Path 'remove_tree';
use File::Fetch;

my $WINFAG = $^O eq 'MSWin32';
my $ONLY_PAIRS;
my %allSongs;
my %rigged;
my %do_not;
my @pairs;

## Check for restrictions
print "Calculating restrictions... ";
while (<>)
{
  last unless (/Restrict/i .. /^$/);
  chomp;

  if (/( <!> )/) 
  {
    my @pair = split $1, $_, 2;
    # @do_not{$pair[0], $pair[1]} = ($pair[1], $pair[0]); 

    for (0..1)
    {
      if (defined $do_not{$pair[$_]})
      { push @{ $do_not{$pair[$_]} }, $pair[$_ - 1]; }
      else
      { $do_not{$pair[$_]} = [ $pair[$_ - 1] ]; }
    }
  }

  elsif (/( !> )/) 
  { 
    my ($key, @bans) = split /$1| \| /; 
    if (defined $do_not{$key})
    { push @{ $do_not{$key} }, @bans; }
    else
    { $do_not{$key} = [ @bans ]; }
  }

  elsif (/( <-> )/) 
  { push @pairs, (split $1, $_, 2); }

  elsif (/( -> )/) 
  {
    my @pair = split $1, $_, 2;
    $rigged{$pair[0]} = $pair[1]; 
  }

  elsif (/pairs/i)
  { $ONLY_PAIRS = 1; }
}
## end

## Get everyone's songs
local $/ = '';

while (<>)
{
  chomp;
  my ($name, @songs) = split "\n";

  $allSongs{$name} = [ @songs ];
}

$/ = "\n";
## end

my @tempParticipants = keys %allSongs;
my @participants;

if ($ONLY_PAIRS && @tempParticipants % 2 != 0)
{ say "Odd number of participants (Can't make all pairs.)" and readline and die ":(" }

# Remove forced pairs from main circle
@tempParticipants = Difference(\@tempParticipants, @pairs);

if (%rigged || %do_not)
{
  ## Remove invalid riggings
  while (my ($key, $value) = each %rigged)
  {
    unless (grep { $key eq $_ } @tempParticipants || grep { $value eq $_ } @tempParticipants)
    { delete $rigged{$key}; }
  }
  ## end

  ## Remove invalid prohibitions
  for my $key (keys %do_not)
  {
    unless (grep { $key eq $_ } @tempParticipants)
    { delete $do_not{$key}; }
  }
  ## end

  # Remove already used participants
  @tempParticipants = Difference(\@tempParticipants, values %rigged);

  ## Handle prohibited pairings
  for my $key (sort { $do_not{$b}->@* <=> $do_not{$a}->@* } keys %do_not)
  {
    # Rigged pairings take priority over bans
    next if exists $rigged{$key};

    my @possible = Difference(\@tempParticipants, $do_not{$key}->@*, $key) or say "No one can get $key\'s songs" and readline and die ":(";
    my $res = $possible[int rand @possible ];

    $rigged{$key} = $res;

    @tempParticipants = @tempParticipants[grep { $tempParticipants[$_] ne $res } 0..$#tempParticipants];

    if ($ONLY_PAIRS)
    {
      $rigged{$res} = $key;
      @tempParticipants = @tempParticipants[grep { $tempParticipants[$_] ne $key } 0..$#tempParticipants];
    }
  }
  ## end

  ## Build result list
  for my $key (keys %rigged)
  {
    next unless defined $rigged{$key};

    # Detect a naturally formed pair
    if (defined $rigged{$rigged{$key}} && $key eq $rigged{$rigged{$key}})
    {
      push @pairs, ($key, $rigged{$key});
      @tempParticipants = Difference(\@tempParticipants, ($key, $rigged{$key}));

      delete $rigged{$rigged{$key}};
      delete $rigged{$key};

      next;
    }

    my @res;
    while (defined $key)
    {
      push @res, $key;

      my $old_key = $key;
      $key = $rigged{$old_key};

      delete $rigged{$old_key};
    }
    push @participants, [ @res ];

    @tempParticipants = Difference(\@tempParticipants, @res);
  }
  push @participants, @tempParticipants;
  ## end

  # uoh
  @participants = flat (shuffle @participants);
}
else
{ @participants = shuffle @tempParticipants; }
say "No one can get $participants[0]\'s songs" and readline and die ":(" if @participants == 1;
if ($ONLY_PAIRS || @participants == 2)
{
  push @pairs, @participants;
  @participants = ();
}
say "did\n";

say "Participants: ", @participants + @pairs;
say "Pairs: ", @pairs / 2, "\n";

say "hola\ngonna do\n";

mkdir 'Result' or remove_tree 'Result', {keep_root => 1};
chdir 'Result';

CreateResults(@participants) if @participants;

my @tempPairs = @pairs;
while (my @pair = splice @tempPairs, 0, 2) 
{ CreateResults(@pair); }

## Save who got who
open my $who, '>', 'who_got_who.txt' or die ":(\n$!";

local $" = ' -> ';
say $who "@participants" if @participants;

$" = ' <-> ';
while (my @pair = splice @pairs, 0, 2) 
{ say $who "@pair"; }

close $who;
## end

say "done.";
<STDIN>;

# [Debug stuff]
# say Dumper %allSongs;
# say "@participants";

sub CreateResults
{ 
  ## Download files and save in individual folders
  for my $i (reverse 0 .. $#_)
  {
    mkdir $_[$i];
    chdir $_[$i];

    my $index++;
    for ($allSongs{$_[$i - 1]}->@*)
    { 
      print 'doing... ';

      if (/youtu\.?be/)
      { 
        if ($WINFAG)
        { qx|..\\..\\libs\\yt-dlp.exe -f ba -x --audio-format mp3 --audio-quality 0 --ffmpeg-location ..\\..\\libs\\ $_ -o "$_[$i]_$index.%(ext)s"|; }
        else
        { qx|yt-dlp -f ba -x --audio-format mp3 --audio-quality 0 $_ -o "$_[$i]_$index.%(ext)s"|; }
      }
      else
      { 
        s/listen/download/ if /newgrounds/; 
        my $ff = File::Fetch->new(uri => "$_");
        $? = ($ff->fetch()) ? 0 : 1;
        rename $ff->output_file, 'temp.mp3';

        unless ($?)
        {
          if ($WINFAG)
          { qx|..\\..\\libs\\ffmpeg.exe -hide_banner -loglevel panic -i temp.mp3 -map 0:a -c:a copy -map_metadata -1 "$_[$i]_$index.mp3"| }
          else
          { qx|ffmpeg -hide_banner -loglevel panic -i temp.mp3 -map 0:a -c:a copy -map_metadata -1 "$_[$i]_$index.mp3"| }
        }

        if ($?)
        {
          unlink "$_[$i]_$index.mp3";
          open my $file, '>', "$_[$i]_$index.txt" or die ":(\n$!";
          print $file "$_";
          close $file;
        }

        unlink "temp.mp3";
      }

      say $? ? 'fuck' : 'did';
    }
    continue 
    { $index++; }

    print "\n";

    chdir '..';
  }
  ## end
}

# Does @$a - @b
sub Difference
{
  my ($a, @b) = @_;
  my %in_b = map { $_ => 1 } @b;
  return grep { not $in_b{$_} } @{ $a };
}

sub flat 
{ return map { ref eq 'ARRAY' ? flat(@$_) : $_ } @_; }
