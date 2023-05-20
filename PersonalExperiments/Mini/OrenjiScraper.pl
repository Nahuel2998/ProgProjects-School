#!/usr/bin/env perl
use strict;
use warnings;

use URI;
use Web::Scraper;
use Array::Transpose::Ragged 'transpose_ragged';
use Term::Table;
use Text::Wrap;
use feature 'say';
use Data::Dumper;

binmode STDOUT, ':utf8';

if (@ARGV < 2) {
  say 'yeah no' and die;
}

my $wanttable = 0;
my $section = "Overview";
my $kind = "p";

# TODO: +Add support for tips (may need another scraper)
#       +Add support for voicelines (definetely needs another scraper)
#       +Add support for hyper (yeah fix your scraper lma)
my %commands = (
  'cards'    => sub { $section = "Recommended_Cards"; $wanttable = 1; },
  'counter'  => sub { $section = "Counter_Cards"    ; $wanttable = 1; },
  'overview' => sub { $section = "Overview"         ;                 },
);

# TODO: +Maybe error handling of some sort?
my $command = lc shift;
( $commands{$command} // (say 'pero que me estas contando tio' and die) )->();

my $char = shift;
my $url = "https://100orangejuice.fandom.com/wiki/$char";
my $uri = URI->new($url) or say 'what who';

# FIXME: =yeah later, works for now
my $scraper = scraper {
  process qq{//span[\@id="$section"]/../following-sibling::$kind\[(preceding-sibling::h2[1]/span)[\@id="$section"]]}, 'section_text[]' => 'TEXT';
};

my $table_scraper = scraper {
  process_first qq{//span[\@id="$section"]/../following-sibling::table}, 'table' => scraper {
    process 'td', 'columns[]' => scraper {
      process 'span', 'cards[]' => '@data-card';
    };
  };
};

my $res = eval { ($wanttable ? $table_scraper : $scraper)->scrape($uri) };

say 'what who' and die if $@;

if ($wanttable) {
  # This is ugly as fuck but it works anyway
  # Needed to filter out undefs since my scraper sucks
  my @columns = map { [ grep { defined } $_->{cards}->@* ] } grep { %$_ } $res->{table}->{columns}->@*;
  if (@columns) { say $_ for get_table(\@columns)->render; } else { say 'no sale' };
} else {
  say wrap('] ', '| ', $_), "\n" for $res->{section_text}->@*;
}

sub get_table {
  my @rows = transpose_ragged( shift );
  my @headers = qw( Viable Recommended Standard );

  Term::Table->new(
    header => [ reverse @headers[0 .. $#{$rows[0]}] ],
    rows => \@rows
  )
}
