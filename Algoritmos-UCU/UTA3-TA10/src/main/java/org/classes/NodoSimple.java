package org.classes;

// No usado.
// El punto era crear un nodo con un solo tipo de dato, para ahorrar memoria.
// Por restricciones de tiempo, no ha sido usado.
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
