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
    public void insertar(@NotNull ILista<Integer, T> lista)
    {
        INodo<Integer, T> aux = lista.getPrimero();

        while (aux != null)
        {
            this.insertar(aux);
            aux = aux.getSiguiente();
        }
    }

//    @Override
    public INodo<Integer, T> buscar(Integer clave)
    { return this.buckets[getIndex(clave)].buscar(clave); }

    public String imprimir(int labels, String separador, String separadorNodo)
    {
        StringBuilder res = new StringBuilder();
        for (ILista<Integer, T> bucket : this.buckets)
        {
            res.append(bucket.imprimir(labels, separador, separadorNodo));
            res.append(separador);
        }

        if (!res.isEmpty())
        { res.setLength(res.length() - separador.length()); }

        return res.toString();
    }

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
//    public void ordenar(short tipo)
//    {
//        for (ILista<Integer, T> bucket : this.buckets)
//        { bucket.ordenar(tipo); }
//    }

//    @Override
//    public void ordenar()
//    {
//        this.vaciar();
//        this.insertar(this.toLista());
//    }

    public void setCantidadBuckets(int cantidadBuckets)
    {
        ILista<Integer, T>[] bucketsOld = this.buckets;

        this.buckets = new ILista[cantidadBuckets];

        // Inicializar buckets
        this.vaciar();

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

    public void vaciar()
    {
        for (int i = 0; i < this.buckets.length; i++)
        { this.buckets[i] = new Lista<>(); }
    }

    public ILista<Integer, T> toLista()
    {
        ILista<Integer, T> res = new Lista<>();

        for (ILista<Integer, T> bucket : this.buckets)
        { res.insertar(bucket); }

        return res;
    }

    private int getIndex(Integer clave)
    { return clave % buckets.length; }

    private int getIndex(@NotNull INodo<Integer, T> nodo)
    { return getIndex(nodo.getEtiqueta()); }
}
