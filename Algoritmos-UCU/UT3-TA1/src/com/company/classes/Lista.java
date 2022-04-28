package com.company.classes;

import org.jetbrains.annotations.NotNull;

public class Lista implements ILista
{
    public static final short ID = 0;
    public static final short NOMBRE = 1;

    private INodo primero;
    private INodo ultimo;
    private int largo;

    public Lista()
    {
        this.primero = null;
        this.ultimo = null;
    }

    public Lista(INodo nodo)
    {
        this.primero = nodo;
        this.ultimo = nodo;
    }

    public void insertarComienzo(INodo nodo)
    {
        if (this.esVacia())
        { this.ultimo = nodo; }
        else
        { nodo.setSiguiente(this.primero); }
        this.primero = nodo;
        this.largo++;
    }

    public void insertarComienzo(@NotNull ILista lista)
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

    public void insertarFinal(INodo nodo)
    {
        if (this.esVacia())
        { this.primero = nodo; }
        else
        { this.ultimo.setSiguiente(nodo); }
        this.ultimo = nodo;
        this.largo++;
    }

    public void insertarFinal(@NotNull ILista lista)
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

    public INodo buscar(String id)
    {
        if (this.esVacia())
        { return null; }

        INodo aux = this.primero;
        while (aux != null)
        {
            if (aux.getId().equals(id))
            { return aux; }
            aux = aux.getSiguiente();
        }
        return null;
    }

    public boolean existe(String id)
    { return buscar(id) != null; }

    public INodo getAt(int indice)
    {
        if (this.esVacia() || indice < 0 || indice >= this.largo)
        { return null; }

        INodo aux = this.primero;
        for (int i = 0; i < indice; i++)
        { aux = aux.getSiguiente(); }
        return aux;
    }

    public boolean eliminar(String id)
    {
        if (this.esVacia())
        { return false; }

        if (this.primero.getId().equals(id))
        {
            this.primero = this.primero.getSiguiente();
            if (this.primero == null)
            { this.ultimo = null; }
            this.largo--;
            return true;
        }

        INodo aux = this.primero;
        while (aux.getSiguiente() != null)
        {
            if (aux.getSiguiente().getId().equals(id))
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

        INodo aux = this.primero;
        for (int i = 0; i < indice - 1; i++)
        { aux = aux.getSiguiente(); }
        aux.setSiguiente(aux.getSiguiente().getSiguiente());
        if (aux.getSiguiente() == null)
        { this.ultimo = aux; }
        return true;
    }

    public String imprimir(int labels, String separador)
    {
        if (this.esVacia())
        { return ""; }

//        StringBuilder res = new StringBuilder(getLabel(this.primero, label));
        StringBuilder res = new StringBuilder(this.primero.imprimir(labels));
        INodo aux = this.primero.getSiguiente();
        while (aux != null)
        {
            res.append(separador);
//            res.append(getLabel(aux, label));
            res.append(aux.imprimir(labels));
            aux = aux.getSiguiente();
        }
        return res.toString();
    }

    public String imprimir(String separador)
    { return imprimir(Nodo.ID, separador); }

    public String imprimir(int labels)
    { return imprimir(labels, " | "); }

    public String imprimir()
    { return imprimir(" | "); }

    public int length()
    { return this.largo; }

    public int cantElementos()
    { return length(); }

    @Override
    public boolean esVacia()
    { return this.primero == null; }

    public INodo getPrimero()
    { return this.primero; }

    public INodo getUltimo()
    { return this.ultimo; }

    public void vaciar()
    {
        this.primero = null;
        this.ultimo = null;
        this.largo = 0;
    }

    // Radix LSD Bucket Sort
    public void ordenar(short tipo)
    {
        ILista[] buckets = new ILista[37];

        // Obtener LSD
        short longestLen = 0;
        INodo aux = this.primero;
        while (aux != null)
        {
            if (aux.getId().length() > longestLen)
            { longestLen = (short) getLabel(aux, tipo).length(); }
            aux = aux.getSiguiente();
        }

        // i = Lugar actual en el string (de LSD a MSD)
        for (short i = (short) (longestLen - 1); i >= 0; i--)
        {
            // Reiniciar buckets
            for (short j = 0; j < buckets.length; j++)
            { buckets[j] = new Lista(); }

            // Insertar en buckets
            aux = this.primero;
            while (aux != null)
            {
                INodo siguiente = aux.getSiguiente();
                aux.setSiguiente(null);

                short c = 0;
                if (aux.getId().length() > i)
                { c = getIndex(getLabel(aux, tipo).charAt(i)); }
                buckets[c].insertarFinal(aux);

                aux = siguiente;
            }

            // Combinar los buckets
            this.vaciar();
            for (ILista b : buckets)
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

    private static String getLabel(@NotNull INodo nodo, short i)
    {
        return switch (i) {
            case ID -> nodo.getId();
            case NOMBRE -> nodo.getNombre();
            default -> "";
        };
    }

//    public static ILista ordenar(ILista lista)
//    { return lista; /* 🎵HopeSort */ }
}
