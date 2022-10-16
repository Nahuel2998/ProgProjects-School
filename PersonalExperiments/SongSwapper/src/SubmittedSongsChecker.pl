#!/usr/bin/perl
use strict;
use warnings;

while (<>)
{ 
  next if /^#|^$/;
  print qx|yt-dlp -q --no-warnings -e $_|; 
}
