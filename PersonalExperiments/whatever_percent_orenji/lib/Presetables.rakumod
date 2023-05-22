#! /usr/bin/env raku
use v6.d;

use General;

enum CardType is export <Battle Boost Event Gift Trap>;

class CardPreset does Descriptable is export {
  has UInt $.star-cost is required;
  has UInt $.level-req is required;
  has Code $.action    is required;
  has Set  $.tags .= new; # Card-specific tags. Example: pudding, sweet

  has CardType $.type  is required;
}

class PanelPreset does Descriptable is export {
  has Str  $.repr   is required; # Character representation
  has Code $.action = -> *%_ { };
  has Code $.step   = -> *%_ { };

  has Set  $.tags .= new;
}

class Character does Descriptable is export {
  has  Int $.atk is required;
  has  Int $.def is required;
  has  Int $.evd is required;
  has  Int $.rec is required;
  has UInt $.hp  is required;

  has  Int $.mov         = 0;
  has UInt $.card-limit  = 3;

  has CardPreset $.hyper is required;

  has Passive $.passive;
}
