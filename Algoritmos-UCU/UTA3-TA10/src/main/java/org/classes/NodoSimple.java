package org.classes;

public class NodoSimple<T>
{
    private T dato;
    private NodoSimple<T> siguiente = null;

    public NodoSimple()
    { this(null); }

    public NodoSimple(T dato)
    { this.dato = dato; }

    public void setSiguiente(NodoSimple<T> siguiente)
    { this.siguiente = siguiente; }

    public NodoSimple<T> getSiguiente()
    { return siguiente; }

    public T getDato()
    { return dato; }

    public void setDato(T dato)
    { this.dato = dato; }
}
