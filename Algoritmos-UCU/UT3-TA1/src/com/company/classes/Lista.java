package com.company.classes;

public class Lista implements ILista
{
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

    public void insertarFinal(INodo nodo)
    {
        if (this.esVacia())
        { this.primero = nodo; }
        else
        { this.ultimo.setSiguiente(nodo); }
        this.ultimo = nodo;
        this.largo++;
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

    public String imprimir(String separador)
    {
        if (this.esVacia())
        { return ""; }

        StringBuilder res = new StringBuilder(this.primero.getId());
        INodo aux = this.primero;
        while (aux != null)
        {
            res.append(separador);
            res.append(aux.getId());
            aux = aux.getSiguiente();
        }
        return res.toString();
    }

    public String imprimir()
    { return imprimir(", "); }

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

    // TODO: Radix Sort?
    public void ordenar()
    { }
}
