#! /usr/bin/env raku
use v6.d;

### General roles/classes
role Descriptable is export {
  has Str $.name        is required;
  has Str $.description is required;
}

#| Executed at the start of every turn.
class Passive does Descriptable is export {
  has Code $.action is required;
  has Int  $.max-stack;
  has Int  $.stack = 1;
}

#| A passive with a limited amount of uses.
class Effect is Passive is export {
  has Int $.uses-left is rw;
}
