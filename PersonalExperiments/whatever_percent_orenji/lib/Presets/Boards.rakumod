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
  'Training Program' => q:to/END/,
  M<-<+<M
  v     ^
  D     _<+<4<D<M 
  v     v   ^   ^
  1<E<D<V   E   - 
  v     ^   ^   ^
  +     *   D   + 
  v     ^   ^   ^
M<_>V<*<W>*>V<_>M 
v   v   v     ^
+   D   *     + 
v   v   v     ^
-   E   V>D>E>3 
v   v   ^     ^
M>D>2>+>_     D 
        v     ^
        M>+>->M 
END
  'Witch Forest' => q:to/END/,
                  V
                  ^
            M>d>/>* 
            ^
  V<+<E<1<-<- 
    v       ^
    +       + 
    v       ^
*<m<D   E>D>W 
v   v   ^
d   W>2>E 
v
-         V   d>M>*>/ 
v         ^   ^     v
e>W       +<4<-     V 
          v   ^
          +   D 
          v   ^
W   e<m<-<W   E 
^   v   v     ^
*<d<-   E     E 
        v     ^
        D>3>W>+ 
END
  'Sealed Archive' => q:to/END/,
  D<E<W       W<+<D
  v   ^       v   ^
D<-   +   +<D<4   -<D 
v     ^   v   ^     ^
+     D<+<E   +     E 
v     v   ^   ^     ^
W>1<+<_   W<-<_<D>+>W 
  v   v         ^
  D   -         + 
  v   v         ^
  +>E<W       W>E<+ 
    v         ^   ^
    +         -   D 
    v         ^   ^
W<+<D>_>->W   _>+>3<W 
v     v   v   ^     ^
E     +   E>+>D     + 
v     v   ^   v     ^
D>-   2>D>+   +   ->D 
  v   ^       v   ^
  D>+>W       W>E>D 
END
  'Christmas Miracle' => q:to/END/,
        +<W<+
        v   ^
        E   E 
        v   ^
      -<D<+<D<- 
      v       ^
+<E<D<1       4<D<-<+ 
v   v           ^   ^
W   +           +   W 
v   v           ^   ^
+>->_           _>E>+ 
    v           ^
    2           3 
    v           ^
    D>+>-   ->+>D 
    v   v   ^   ^
  E<+   D>_>D   +<E 
  v     ^   v     ^
  +   D>W   W>D   + 
  v   ^       v   ^
  W>->+       +>->W 
END
  'Icy Hideout' => q:to/END/,
    +>E>1 
    ^   v
    I   V 
    ^
    D     W   +>D>I>+ 
    ^     v   ^     v
    +<E<I<D</<E     E 
      v       ^     v
      /       I   V<4 
      v       ^
    W>D       D<W 
      v       ^
2>V   I       / 
^     v       ^
E     E>/>D>I>E>+ 
^     v   ^     v
+<I<D<+   W     D 
                v
            V   I 
            ^   v
            3<E<+ 
END
);
