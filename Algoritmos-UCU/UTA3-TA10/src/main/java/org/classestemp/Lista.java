package org.classestemp;

import jdk.jshell.spi.ExecutionControl;

public class Lista<K extends Comparable<K>, T> implements ILista<K, T>
{
    private Nodo<K, T> primero;

    public Lista()
    { primero = null; }

    @Override
    public void insertar(Nodo<K, T> unNodo)
    {
        // Implementar método
    }

    @Override
    public Nodo<K, T> buscar(K clave)
    {
        // TODO: Implementar método
    }

    @Override
    public boolean eliminar(K clave)
    {
        if (esVacia())
        {
            return false;
        }
        if (primero.getSiguiente() == null)
        {
            if (primero.getEtiqueta().equals(clave))
            {
                primero = null;
                return true;
            }
        }
        Nodo<K, T> aux = primero;
        if (aux.getEtiqueta().compareTo(clave) == 0)
        {
            //Eliminamos el primer elemento
            Nodo<K, T> temp1 = aux;
            Nodo<K, T> temp = aux.getSiguiente();
            primero = temp;
            return true;
        }
        while (aux.getSiguiente() != null)
        {
            if (aux.getSiguiente().getEtiqueta().equals(clave))
            {
                Nodo<K, T> temp = aux.getSiguiente();
                aux.setSiguiente(temp.getSiguiente());
                return true;

            }
            aux = aux.getSiguiente();
        }
        return false;
    }

    @Override
    public String imprimir()
    {
        String aux = "";
        if (!esVacia())
        {
            Nodo<K, T> temp = primero;
            while (temp != null)
            {
                temp.imprimirEtiqueta();
                temp = temp.getSiguiente();
            }
        }
        return aux;
    }

    public String imprimir(String separador)
    {
        String aux = "";
        if (esVacia())
        {
            return "";
        }
        else
        {
            Nodo<K, T> temp = primero;
            aux = "" + temp.getEtiqueta();
            while (temp.getSiguiente() != null)
            {
                aux = aux + separador + temp.getSiguiente().getEtiqueta();
                temp = temp.getSiguiente();
            }

        }
        return aux;

    }

    @Override
    public int cantElementos()
    {
        int contador = 0;
        if (esVacia())
        {
            System.out.println("Cantidad de elementos 0.");
            return 0;
        }
        else
        {
            INodo aux = primero;
            while (aux != null)
            {
                contador++;
                aux = aux.getSiguiente();
            }
        }
        return contador;
    }

    @Override
    public boolean esVacia()
    { return primero == null; }

    @Override
    public Nodo<K, T> getPrimero()
    { return primero; }

    @Override
    public void setPrimero(Nodo<K, T> unNodo)
    { this.primero = unNodo; }
}
