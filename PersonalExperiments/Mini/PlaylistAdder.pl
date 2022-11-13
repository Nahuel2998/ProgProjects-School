#!/usr/bin/env perl

use strict;
use warnings;
use autodie;

use Carp;
use HTTP::Tiny;
use JSON;
use TOML qw(from_toml to_toml);
use YouTube::Util 'extract_youtube_video_id';
use feature 'say';
# use Data::Dumper;

say 'what where' and <STDIN> and die unless -e "config.toml";

my $WINFAG = $^O eq 'MSWin32';

my $config = from_toml(do { local(@ARGV, $/) = "config.toml"; <> });

my $API_KEY = $config->{Auth}->{api_key};
my $CLIENT_ID = $config->{Auth}->{client_id};
my $CLIENT_SECRET = $config->{Auth}->{client_secret};
my $REFRESH_TOKEN = $config->{Auth}->{refresh_token};
my $PLAYLIST = $config->{playlist};
my $http = HTTP::Tiny->new;

my $access_token = refresh_access();
# say $access_token;

my $video_id = extract_youtube_video_id(shift);
# say $video_id;

my $options = {
  headers => {
    Authorization  => "Bearer $access_token",
    Accept         => "application/json",
    'Content-Type' => "application/json",
  },
  content => {
    snippet => {
      playlistId => $PLAYLIST,
      resourceId => {
        kind    => 'youtube#video',
        videoId => $video_id,
      }
    }
  },
};

my $json = encode_json($options->{content});
my $curl_cmd = qq|curl -s --request POST 'https://youtube.googleapis.com/youtube/v3/playlistItems?part=snippet&key=$API_KEY' --header 'Authorization: $options->{headers}->{Authorization}' --header 'Accept: $options->{headers}->{Accept}' --header 'Content-Type: $options->{headers}->{'Content-Type'}' --data '$json' --compressed|;
{
  no warnings 'uninitialized';
  $curl_cmd = $curl_cmd =~ s/^curl/curl.exe/r =~ s/"/\\"/gr =~ s/'({)|(})'/$1$2/gr =~ s/'/"/gr =~ s/--compressed//r if $WINFAG;
}
my $resp = decode_json(qx|$curl_cmd|);

binmode STDOUT, ':utf8';
say exists $resp->{error} 
  ? "fuck. $resp->{error}->{message}" 
  : "did. Added: $resp->{snippet}->{title}";
<STDIN>;

sub refresh_access {
  my $resp = $http->post_form('https://www.googleapis.com/oauth2/v4/token', {
    refresh_token => $REFRESH_TOKEN,
    client_id     => $CLIENT_ID,
    client_secret => $CLIENT_SECRET,
    grant_type    => 'refresh_token',
  });
  croak "$resp->{status} $resp->{reason}" unless $resp->{success};
  my $json = $resp->{content};
  my $obj = decode_json($json);
  $obj->{access_token};
}
