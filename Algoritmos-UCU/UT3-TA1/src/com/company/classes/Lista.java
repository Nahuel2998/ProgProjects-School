package com.company.classes;

public class Lista implements ILista
{
    private INodo primero;
    private INodo ultimo;
    private int largo;

    // TODO: Obtener elemento en posicion n.

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
        else
        {
            INodo aux = primero;
            while (aux != null)
            {
                if (aux.getId().equals(id))
                { return aux; }
                aux = aux.getSiguiente();
            }
        }
        return null;
    }

    public boolean eliminar(String id)
    {
        if (esVacia())
        { return false; }

        if (primero.getId().equals(id))
        {
            if (primero.getSiguiente() == null)
            { ultimo = null; }
            primero = null;
            largo--;
            return true;
        }

        INodo aux = primero;
        while (aux.getSiguiente() != null)
        {
            if (aux.getSiguiente().getId().equals(id))
            {
                aux.setSiguiente(aux.getSiguiente());
                largo--;
                return true;
            }
            aux = aux.getSiguiente();
        }
        return false;
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
