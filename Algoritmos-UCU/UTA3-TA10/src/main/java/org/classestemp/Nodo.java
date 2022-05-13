package org.classestemp;

import org.jetbrains.annotations.NotNull;

public class Nodo<K extends Comparable<K>, T> implements INodo<K, T>
{
    private final K etiqueta;
    private T dato;
    private INodo<K, T> siguiente = null;

    public Nodo(K etiqueta, T dato)
    {
        this.etiqueta = etiqueta;
        this.dato = dato;
    }

    public T getDato()
    { return this.dato; }

    public void setDato(T dato)
    { this.dato = dato; }

    @Override
    public K getEtiqueta()
    { return this.etiqueta; }

//    @Override
//    public String imprimir()
//    { return this.dato.toString(); }

//    @Override
    public String imprimirEtiqueta()
    { return this.getEtiqueta().toString(); }

    public INodo<K, T> clonar()
    { return new Nodo<>(this.etiqueta, this.dato); }

    public boolean equals(@NotNull Nodo<K, T> unNodo)
    { return this.dato.equals(unNodo.getDato()); }

//    @Override
//    public int compareTo(K etiqueta)
//    { return this.etiqueta.compareTo(etiqueta); }

    @Override
    public INodo<K, T> getSiguiente()
    { return this.siguiente; }

    @Override
    public void setSiguiente(INodo<K, T> nodo)
    { this.siguiente = nodo; }
}
