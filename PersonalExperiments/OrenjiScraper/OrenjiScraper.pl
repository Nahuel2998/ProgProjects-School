use strict;
use warnings;

use URI;
use Web::Scraper;
use Array::Transpose::Ragged 'transpose_ragged';
use Term::Table;
use feature 'say';
use Data::Dumper;

my $url = 'https://100orangejuice.fandom.com/wiki/Tomomo';
my $uri = URI->new($url);

my $section = "Recommended_Cards";
my $kind = "*";

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

# my $result = $scraper->scrape($uri);
# say for $result->{section_text}->@*;

my $res = $table_scraper->scrape($uri);
my @columns = map { [ grep { defined } $_->{cards}->@* ] } grep { %$_ } $res->{table}->{columns}->@*;
say $_ for get_table(\@columns)->render;

sub get_table {

  my @rows = transpose_ragged( shift );
  my @headers = qw( Viable Recommended Standard );

  Term::Table->new(
    header => [ reverse @headers[0 .. $#{$rows[0]}] ],
    rows => \@rows
  )
}
