package org.classes;

import org.jetbrains.annotations.NotNull;

public class ListaMejorada<T>
{
    private ILista<Integer, T>[] buckets;

    public ListaMejorada()
    { this(4); }

    public ListaMejorada(int cantidadBuckets)
    { this.setCantidadBuckets(cantidadBuckets); }

//    @Override
    public void insertar(@NotNull INodo<Integer, T> nodo)
    { this.buckets[getIndex(nodo)].insertar(nodo); }

//    @Override
    public INodo<Integer, T> buscar(Integer clave)
    { return this.buckets[getIndex(clave)].buscar(clave); }

    public ILista<Integer, T> buscarCada(Integer clave)
    { return this.buckets[getIndex(clave)].buscarCada(clave); }

//    public NodoSimple<INodo<Integer, T>> buscarCada(Integer clave)
//    { return this.buckets[getIndex(clave)].buscarCada(clave); }

//    public ListaMejorada<T> buscarCada(Integer clave)
//    { return this.buckets[getIndex(clave)].buscarCadaMejor(clave); }

//    @Override
    public boolean eliminar(Integer clave)
    { return this.buckets[getIndex(clave)].eliminar(clave); }

//    @Override
    public int cantElementos()
    {
        int res = 0;
        for (ILista<Integer, T> bucket : this.buckets)
        { res += bucket.cantElementos(); }
        return res;
    }

//    @Override
    public boolean esVacia()
    {
        for (ILista<Integer, T> bucket : this.buckets)
        {
            if (!bucket.esVacia())
            { return false; }
        }
        return true;
    }

//    @Override
    public boolean existe(Integer clave)
    { return this.buscar(clave) != null; }

//    @Override
    public void ordenar(short tipo)
    {
        for (ILista<Integer, T> bucket : this.buckets)
        { bucket.ordenar(tipo); }
    }

    public void setCantidadBuckets(int cantidadBuckets)
    {
        ILista<Integer, T>[] bucketsOld = this.buckets;

        this.buckets = new ILista[cantidadBuckets];

        // Inicializar buckets
        for (int i = 0; i < this.buckets.length; i++)
        { this.buckets[i] = new Lista<>(); }

        if (bucketsOld == null)
        { return; }

        // Si existian datos anteriormente, insertarlos de nuevo
        for (ILista<Integer, T> bucket : bucketsOld)
        {
            INodo<Integer, T> aux = bucket.getPrimero();
            while (aux != null)
            {
                this.insertar(aux);
                aux = aux.getSiguiente();
            }
        }
    }

    private int getIndex(Integer clave)
    { return clave % buckets.length; }

    private int getIndex(@NotNull INodo<Integer, T> nodo)
    { return getIndex(nodo.getEtiqueta()); }
}
