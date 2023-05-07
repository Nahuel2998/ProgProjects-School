#! /usr/bin/env raku

our %boards is export = %(
  'Pudding Chase' => q:to/END/,
      +<W<-
      v   ^
      D   D
      v   ^
    1<E<D<+<4
    v       ^
E<+<D       E<D<+
v   v       ^   ^
W   +       +   W
v   v       ^   ^
+>D>-       D>+>-
    v       ^
    2>+>D>E>3
      v   ^
      D   D
      v   ^
      ->W>+
END
  'Sweet Heaven' => q:to/END/,
    V     V<+ 
    v       ^
    M       D<-<+ 
    v       v   ^
  +<-<D<+<1<E   -<M<V 
  v   v     ^   ^
  -   E     D<E<D 
  v   v         ^
+<D>E<D         + 
v   v           ^
V   2           4   V 
    v           ^   ^
    +         D>E<D>+ 
    v         ^   ^
    D>E>D     E   - 
    v   v     ^   ^
V>M>-   E>3>+>D>->+ 
    v   ^       ^
    +>->D       M 
        v       ^
        +>V     V 
END
  'Clover' => q:to/END/,
    /<+<E<D 
    v     ^
    1     M 
    v     ^
    E<d<W<+<E<4</ 
    v       ^   ^
D<M<+       d   + 
v   v       ^   ^
E   W       W   E 
v   v       ^   ^
+   d       +>M>D 
v   v       ^
/>2>E>+>W>d>E 
      v     ^
      M     3 
      v     ^
      D>E>+>/ 
END
);
