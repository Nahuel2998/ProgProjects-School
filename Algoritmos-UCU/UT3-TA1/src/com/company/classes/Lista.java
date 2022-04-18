package com.company.classes;

public class Lista implements ILista
{
    private INodo primero;
    private INodo ultimo;
    private int largo;

    public Lista()
    {
        primero = null;
        ultimo = null;
    }

    public Lista(INodo nodo)
    {
        this.primero = nodo;
        this.ultimo = nodo;
    }

    public void insertarComienzo(INodo nodo)
    {
        if (esVacia())
        { ultimo = nodo; }
        else
        { nodo.setSiguiente(primero); }
        primero = nodo;
        largo++;
    }

    public void insertarFinal(INodo nodo)
    {
        if (esVacia())
        { primero = nodo; }
        else
        { ultimo.setSiguiente(nodo); }
        ultimo = nodo;
        largo++;
    }

    public INodo buscar(String id)
    {
        if (esVacia())
        { return null; }

        INodo aux = primero;
        while (aux != null)
        {
            if (aux.getId().equals(id))
            { return aux; }
            aux = aux.getSiguiente();
        }
        return null;
    }

    public INodo buscarAt(int indice)
    {
        if (esVacia() || indice < 0 || indice >= largo)
        { return null; }

        INodo aux = primero;
        for (int i = 0; i < indice; i++)
        { aux = aux.getSiguiente(); }
        return aux;
    }

    public boolean eliminar(String id)
    {
        if (esVacia())
        { return false; }

        if (primero.getId().equals(id))
        {
            primero = primero.getSiguiente();
            if (primero == null)
            { ultimo = null; }
            largo--;
            return true;
        }

        INodo aux = primero;
        while (aux.getSiguiente() != null)
        {
            if (aux.getSiguiente().getId().equals(id))
            {
                aux.setSiguiente(aux.getSiguiente().getSiguiente());
                largo--;
                return true;
            }
            aux = aux.getSiguiente();
        }
        return false;
    }

    public boolean eliminarAt(int indice)
    {
        if (esVacia() || indice < 0 || indice >= largo)
        { return false; }

        largo--;

        if (indice == 0)
        {
            primero = primero.getSiguiente();
            if (primero == null)
            { ultimo = null; }
            return true;
        }

        INodo aux = primero;
        for (int i = 0; i < indice - 1; i++)
        { aux = aux.getSiguiente(); }
        aux.setSiguiente(aux.getSiguiente().getSiguiente());
        if (aux.getSiguiente() == null)
        { ultimo = aux; }
        return true;
    }

    public int length()
    { return largo; }

    public int cantElementos()
    { return length(); }

    @Override
    public boolean esVacia()
    { return primero == null; }

    public INodo getPrimero()
    { return primero; }

    public INodo getUltimo()
    { return ultimo; }
}
