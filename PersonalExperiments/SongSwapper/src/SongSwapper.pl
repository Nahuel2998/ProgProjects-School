#!/usr/bin/perl
use strict;
use warnings;
use feature 'say';
use Data::Dumper;
use List::Util 'shuffle';
use File::Path 'remove_tree';
use File::Spec;
use File::Fetch;

$File::Fetch::WARN = 0;
my $WINFAG = $^O eq 'MSWin32';
my $ONLY_PAIRS;
my %allSongs;
my %rigged;
my %do_not;
my @pairs;

my $ffmpeg = "ffmpeg";
my $yt_dlp = "yt-dlp";
my $begone = "/dev/null";
my $ffmpeg_location = "";

if ($WINFAG)
{
  $ffmpeg = '..\\..\\libs\\ffmpeg.exe';
  $yt_dlp = '..\\..\\libs\\yt-dlp.exe';
  $begone = 'NUL';
  $ffmpeg_location = '--ffmpeg-location ..\\..\\libs\\';
}

local $| = 1;
## Check for restrictions
print "Calculating restrictions... ";
while (<>)
{
  next if /^#/;
  last unless (/Restrict/i .. /^$/);
  chomp;

  if (/ <!> /)
  {
    my @pair = ( $`, $' );
    # @do_not{$pair[0], $pair[1]} = ($pair[1], $pair[0]);

    for (0..1)
    {
      if (defined $do_not{$pair[$_]})
      { push @{ $do_not{$pair[$_]} }, $pair[$_ - 1]; }
      else
      { $do_not{$pair[$_]} = [ $pair[$_ - 1] ]; }
    }
  }

  elsif (/ !> /)
  {
    for my $key (split ' | ', $`)
    {
      my @bans = split ' | ', $';

      if (defined $do_not{$key})
      { push @{ $do_not{$key} }, @bans; }
      else
      { $do_not{$key} = [ @bans ]; }
    }
  }

  elsif (/ <-> /)
  { push @pairs, ( $`, $' ); }

  elsif (/ -> /)
  {
    # Look at how simple this was before, the good old times
    # $rigged{$1} = $2;

    my @chain = split ' -> ';

    for my $i (0 .. $#chain - 1)
    { $rigged{$chain[$i]} = $chain[$i + 1]; }
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

my @temp_participants = keys %allSongs;
my @participants;
my @closedCircles;

if ($ONLY_PAIRS && @temp_participants % 2 != 0)
{ say "Odd number of participants (Can't make all pairs.)" and readline and die ":(" }

# Remove forced pairs from main circle
@temp_participants = Difference(\@temp_participants, @pairs);

if (%rigged || %do_not)
{
  # Fix a bug
  if ($ONLY_PAIRS && %rigged)
  { say "dude I told you to not use ' -> ' if you want only pairs go fix that shit I ain't handling this" and readline and die ":("; }

  ## Remove invalid riggings
  while (my ($key, $value) = each %rigged)
  {
    unless ((grep { $key eq $_ } @temp_participants) || (grep { $value eq $_ } @temp_participants))
    { delete $rigged{$key}; }
  }
  ## end

  ## Remove invalid prohibitions
  for my $key (keys %do_not)
  {
    unless (grep { $key eq $_ } @temp_participants)
    { delete $do_not{$key}; }
  }
  ## end

  # Remove already used participants
  @temp_participants = Difference(\@temp_participants, values %rigged);

  # hola
  # The following is the most complex loop in this thing
  # yeah it's not so smart and I'll be trying to fix it in the future
  # Me from the future: lma
  ## Handle prohibited pairings
  HANDLE_PROHIBITED:
  for my $key (sort { $do_not{$b}->@* <=> $do_not{$a}->@* } keys %do_not)
  {
    # Rigged pairings take priority over bans
    next if exists $rigged{$key};

    my @possible = Difference(\@temp_participants, ($do_not{$key}->@*, $key)) or say "No one can get $key\'s songs" and readline and die ":(";
    my $res = $possible[int rand @possible];

    my $temp = $res;
    # Check if it'd form a circle
    while (defined $temp)
    {
      if ($temp eq $key)
      {
        push $do_not{$key}->@*, $res;

        # Redo unless it really has to be closed here
        redo HANDLE_PROHIBITED unless @possible == 1;

        last;
      }
    }
    continue
    { $temp = $rigged{$temp}; }

    $rigged{$key} = $res;

    @temp_participants = @temp_participants[grep { $temp_participants[$_] ne $res } 0..$#temp_participants];

    if ($ONLY_PAIRS)
    {
      $rigged{$res} = $key;
      @temp_participants = @temp_participants[grep { $temp_participants[$_] ne $key } 0..$#temp_participants];
    }
  }
  ## end

  # yeah this one too
  ## Build chains of participants
  for my $key (keys %rigged)
  {
    next unless defined $rigged{$key};

    # Detect a naturally formed pair
    # I should probably remove this now since it's not very useful but ye
    if (defined $rigged{$rigged{$key}} && $key eq $rigged{$rigged{$key}})
    {
      push @pairs, ($key, $rigged{$key});
      @temp_participants = Difference(\@temp_participants, ($key, $rigged{$key}));

      delete $rigged{$rigged{$key}};
      delete $rigged{$key};

      next;
    }

    my @res;
    my $first = $key;
    my $is_closed;
    while (defined $key)
    {
      if ($first eq $key && @res)
      {
        $is_closed++;
        last;
      }

      push @res, $key;

      my $old_key = $key;
      $key = $rigged{$old_key};

      delete $rigged{$old_key};
    }

    if ($is_closed)
    { push @closedCircles, [ @res ]; }
    else
    { push @participants, [ @res ]; }

    @temp_participants = Difference(\@temp_participants, @res);
  }
  ## end

  ## Join chains that should be joined
  for my $i (reverse 0 .. $#participants)
  {
    for my $j (reverse 0 .. $#participants)
    {
      my @tail_chain = $participants[$i]->@*;

      if (@{ $participants[$j] }[-1] eq $tail_chain[0])
      {
        push @{ $participants[$j] }, @tail_chain[1..$#tail_chain];
        splice @participants, $i, 1;

        last;
      }
    }
  }
  ## end

  # Add the unchained participants
  push @participants, @temp_participants;

  # uoh
  @participants = flat (shuffle @participants);
}
else
{ @participants = shuffle @temp_participants; }
say "No one can get $participants[0]\'s songs" and readline and die ":(" if @participants == 1;
if ($ONLY_PAIRS || @participants == 2)
{
  push @pairs, @participants;
  @participants = ();
}
say "did\n";

say "Participants: ", @participants + @pairs + flat(@closedCircles);

print "Circles: ", @closedCircles + !!@participants, " ( ";
print "[", @participants + 0, "] " if @participants;
print "[", $_->@* + 0, "] " for @closedCircles;
say ")";

say "Pairs: ", @pairs / 2, "\n";

say "hola\ngonna do\n";

chdir +( File::Spec->splitpath( __FILE__ ) )[1];
mkdir 'Result' or remove_tree 'Result', {keep_root => 1};
chdir 'Result';

## Create Results
CreateResults(@participants) if @participants;

for my $circle (@closedCircles)
{ CreateResults($circle->@*); }

my @tempPairs = @pairs;
while (my @pair = splice @tempPairs, 0, 2)
{ CreateResults(@pair); }
## end

## Save who got who
open my $who, '>', 'who_got_who.txt' or die ":(\n$!";

local $" = ' -> ';
say $who "@participants" if @participants;

for my $circle (@closedCircles)
{ say $who "@{ $circle }"; }

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
    my %flags;
    for ($allSongs{$_[$i - 1]}->@*)
    {
      print 'doing... ';

      if (/\s(.*)$/)
      {
        my $flags_str = $1;

        if ($flags_str =~ /(!|re)/)
        { $flags{$1}++; }

        if ($flags_str =~ /\[\s*(\S*)\s*[~-]\s*(\S*)\s*\]/)
        { $flags{'trim'} = join (' ', grep { /\S/ } (($1 ? "-ss $1" : ""), ($2 ? "-to $2" : ""))); }

        s/\s.*//;
      }

      my $filename = "$_[$i]_$index";

      my $args = "-f ba -x --no-embed-metadata $ffmpeg_location";
      $args .= qq| --audio-format mp3 --audio-quality 0| unless $flags{'!'};
      $args .= qq| --downloader ffmpeg --downloader-args "ffmpeg_i:$flags{'trim'}"| if $flags{'trim'};

      qx|$yt_dlp -q --no-warnings $args $_ -o "$filename.%(ext)s" 2>$begone|;

      my $extension = "mp3";
      my $already_trimmed = !$?;

      # If it didn't succeed, fallback to old method
      if ($?)
      {
        s/listen/download/ if /newgrounds/;

        my $ff = File::Fetch->new(uri => "$_");
        if (defined $ff)
        {
          $? = ($ff->fetch()) ? 0 : 1;
          ( $extension ) = $ff->output_file =~ /.*\.([^\.]*)/ if $flags{'!'};
          rename $ff->output_file, "$filename.$extension";
        }
        else
        { $? = 1; }
      }

      # If we got a file and it isn't from youtube, strip the metadata
      unless (/youtu\.?be/ || $?)
      { 
        # what the fuCK Larry
        my $realfile = +( <"$filename.*"> )[-1];
        ( $extension ) = $realfile =~ /.*\.([^\.]*)/ if $flags{'!'};

        rename $realfile, "temp.$extension";

        StripMetadata($filename, $extension, $already_trimmed, %flags); 

        unlink "temp.$extension";
      }

      # If we couldn't save it, give up
      if ($?)
      {
        unlink "$filename.$extension";
        open my $file, '>', "$filename.txt" or die ":(\n$!";
        print $file "$_";
        print $file " $flags{'trim'}" if exists $flags{'trim'};
        close $file;
      }

      say $? ? 'fuck' : 'did';
    }
    continue
    { 
      $index++; 
      %flags = ();
    }

    print "\n";

    chdir '..';
  }
  ## end
}

sub StripMetadata
{
  my ($filename, $extension, $already_trimmed, %flags) = @_;

  my $args; 
  my $target_extension; 
  if ($flags{'re'})
  {
    $args = '-q:a 0 -map 0:a -map_metadata -1';
    $target_extension = 'mp3';
  }
  else
  {
    $args = '-map 0:a -c:a copy -map_metadata -1 -fflags +bitexact -flags:a +bitexact';
    $target_extension = $extension;
  }
  $args .= qq| $flags{'trim'}| if exists $flags{'trim'} and !$already_trimmed;

  qx|$ffmpeg -hide_banner -loglevel panic -i temp.$extension $args "$filename.$target_extension"| ;
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
