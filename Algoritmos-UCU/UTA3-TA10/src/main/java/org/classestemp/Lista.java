package org.classestemp;

import org.jetbrains.annotations.NotNull;

import java.lang.reflect.Array;

public class Lista<K extends Comparable<K>, T> implements ILista<K, T>
{
    public static final short ETIQUETA = 0;
    public static final short DATO = 1;

    private INodo<K, T> primero;
    private INodo<K, T> ultimo;
    private int largo;

    public Lista()
    {
        this.primero = null;
        this.ultimo = null;
    }

    public Lista(INodo<K, T> nodo)
    {
        this.primero = nodo;
        this.ultimo = nodo;
    }

    @Override
    public void insertarComienzo(INodo<K, T> nodo)
    {
        if (this.esVacia())
        { this.ultimo = nodo; }
        else
        { nodo.setSiguiente(this.primero); }
        this.primero = nodo;
        this.largo++;
    }

    public void insertarComienzo(@NotNull ILista<K, T> lista)
    {
        if (lista.esVacia())
        { return; }

        if (this.esVacia())
        { this.ultimo = lista.getUltimo(); }
        else
        { lista.getUltimo().setSiguiente(this.primero); }
        this.primero = lista.getPrimero();
        this.largo += lista.cantElementos();
    }

    @Override
    public void insertarFinal(INodo<K, T> nodo)
    {
        if (this.esVacia())
        { this.primero = nodo; }
        else
        { this.ultimo.setSiguiente(nodo); }
        this.ultimo = nodo;
        this.largo++;
    }

    public void insertarFinal(@NotNull ILista<K, T> lista)
    {
        if (lista.esVacia())
        { return; }

        if (this.esVacia())
        { this.primero = lista.getPrimero(); }
        else
        { this.ultimo.setSiguiente(lista.getPrimero()); }
        this.ultimo = lista.getUltimo();
        this.largo += lista.cantElementos();
    }

    @Override
    public INodo<K, T> buscar(K clave)
    {
        if (this.esVacia())
        { return null; }

        INodo<K, T> aux = this.primero;
        while (aux != null)
        {
            if (aux.getEtiqueta().equals(clave))
            { return aux; }
            aux = aux.getSiguiente();
        }
        return null;
    }

    @Override
    public boolean existe(K clave)
    { return buscar(clave) != null; }

    @Override
    public INodo<K, T> getAt(int indice)
    {
        if (this.esVacia() || indice < 0 || indice >= this.largo)
        { return null; }

        INodo<K, T> aux = this.primero;
        for (int i = 0; i < indice; i++)
        { aux = aux.getSiguiente(); }
        return aux;
    }

    @Override
    public boolean eliminar(K clave)
    {
        if (this.esVacia())
        { return false; }

        if (this.primero.getEtiqueta().equals(clave))
        {
            this.primero = this.primero.getSiguiente();
            if (this.primero == null)
            { this.ultimo = null; }
            this.largo--;
            return true;
        }

        INodo<K, T> aux = this.primero;
        while (aux.getSiguiente() != null)
        {
            if (aux.getSiguiente().getEtiqueta().equals(clave))
            {
                aux.setSiguiente(aux.getSiguiente().getSiguiente());
                this.largo--;
                if (aux.getSiguiente() == null)
                { this.ultimo = aux; }
                return true;
            }
            aux = aux.getSiguiente();
        }
        return false;
    }

    @Override
    public boolean eliminarAt(int indice)
    {
        if (this.esVacia() || indice < 0 || indice >= this.largo)
        { return false; }

        this.largo--;

        if (indice == 0)
        {
            this.primero = this.primero.getSiguiente();
            if (this.primero == null)
            { this.ultimo = null; }
            return true;
        }

        INodo<K, T> aux = this.primero;
        for (int i = 0; i < indice - 1; i++)
        { aux = aux.getSiguiente(); }
        aux.setSiguiente(aux.getSiguiente().getSiguiente());
        if (aux.getSiguiente() == null)
        { this.ultimo = aux; }
        return true;
    }

//    @Override
//    public String imprimir()
//    {
        // TODO: This.
//    }

//    @Override
//    public String imprimir(String separador)
//    {
        // TODO: This.
//    }

//    @Override
    public String imprimirEtiquetas(String separador)
    {
        // FIXME: Replace with a call to imprimir()
        if (this.esVacia())
        { return ""; }

        StringBuilder res = new StringBuilder(this.primero.imprimirEtiqueta());
        INodo<K, T> aux = this.primero.getSiguiente();
        while (aux != null)
        {
            res.append(separador);
            res.append(aux.imprimirEtiqueta());
            aux = aux.getSiguiente();
        }
        return res.toString();
    }

    public int length()
    { return this.largo; }

    @Override
    public int cantElementos()
    { return length(); }

    @Override
    public boolean esVacia()
    { return this.primero == null; }

    @Override
    public INodo<K, T> getPrimero()
    { return this.primero; }

    @Override
    public INodo<K, T> getUltimo()
    { return this.ultimo; }

    public void vaciar()
    {
        this.primero = null;
        this.ultimo = null;
        this.largo = 0;
    }

    // Radix LSD Bucket Sort
    // FIXME: yeah
    public void ordenar(short tipo)
    {
//        ILista<K, T>[] buckets = new ILista<K, T>[37];
        ILista<K, T>[] buckets = (ILista<K, T>[]) Array.newInstance(this.getClass(), 37);

        // Obtener LSD
        short longestLen = 0;
        INodo<K, T> aux = this.primero;
        while (aux != null)
        {
            if (aux.getEtiqueta().toString().length() > longestLen)
            { longestLen = (short) getLabel(aux, tipo).length(); }
            aux = aux.getSiguiente();
        }

        // i = Lugar actual en el string (de LSD a MSD)
        for (short i = (short) (longestLen - 1); i >= 0; i--)
        {
            // Reiniciar buckets
            for (short j = 0; j < buckets.length; j++)
            { buckets[j] = new Lista<K, T>(); }

            // Insertar en buckets
            aux = this.primero;
            while (aux != null)
            {
                INodo<K, T> siguiente = aux.getSiguiente();
                aux.setSiguiente(null);

                short c = 0;
                if (aux.getEtiqueta().toString().length() > i)
                { c = getIndex(getLabel(aux, tipo).charAt(i)); }
                buckets[c].insertarFinal(aux);

                aux = siguiente;
            }

            // Combinar los buckets
            this.vaciar();
            for (ILista<K, T> b : buckets)
            { this.insertarFinal(b); }
        }
    }

    private static short getIndex(char c)
    {
        if (c >= 97)
        { return (short) (c - 86); }

        if (c >= 65)
        { return (short) (c - 54); }

        return (short) (c - 47);
    }

    private String getLabel(@NotNull INodo<K, T> nodo, short i)
    {
        return switch (i) {
            case ETIQUETA -> nodo.getEtiqueta().toString();
            case DATO -> nodo.getDato().toString();
            default -> "";
        };
    }

//    public static ILista ordenar(ILista lista)
//    { return lista; /* 🎵HopeSort */ }
}
