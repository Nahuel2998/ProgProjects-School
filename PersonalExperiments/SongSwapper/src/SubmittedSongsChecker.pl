#!/usr/bin/perl
use strict;
use warnings;

while (<>)
{ print qx|yt-dlp -e $_|; }
