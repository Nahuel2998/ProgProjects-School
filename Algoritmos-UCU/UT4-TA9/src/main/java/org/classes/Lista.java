package org.classes;

import org.jetbrains.annotations.NotNull;

import static org.classes.Nodo.DATO;
import static org.classes.Nodo.ETIQUETA;

public class Lista<K extends Comparable<K>, T> implements IListaIndexada<K, T>
{
    private INodo<K, T> primero;
    private INodo<K, T> ultimo;
    private int largo;

    public Lista()
    { this(null); }

    public Lista(INodo<K, T> nodo)
    { this.insertar(nodo); }

    @Override
    public void insertarComienzo(INodo<K, T> nodo)
    {
        if (nodo == null)
        { return; }

//        nodo.setSiguiente(null);

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
        if (nodo == null)
        { return; }

//        nodo.setSiguiente(null);

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
    public void insertar(INodo<K, T> nodo)
    { this.insertarFinal(nodo); }

    @Override
    public void insertar(@NotNull ILista<K, T> lista)
    { this.insertarFinal(lista); }

    @Override
    public INodo<K, T> buscar(K clave)
    {
        if (this.esVacia())
        { return null; }

        INodo<K, T> aux = this.primero;
        while (aux != null)
        {
            if (clave.equals(aux.getEtiqueta()))
            { return aux; }
            aux = aux.getSiguiente();
        }
        return null;
    }

    @Override
    public INodo<K, T> buscarDato(T dato)
    {
        if (this.esVacia())
        { return null; }

        INodo<K, T> aux = this.primero;
        while (aux != null)
        {
            if (dato.equals(aux.getDato()))
            { return aux; }
            aux = aux.getSiguiente();
        }
        return null;
    }

    @Override
    public ILista<K, T> buscarCada(K clave)
    {
        if (this.esVacia())
        { return null; }

        ILista<K, T> res = new Lista<>();

        INodo<K, T> aux = this.primero;
        while (aux != null)
        {
            if (clave.equals(aux.getEtiqueta()))
            { res.insertar(aux); }
            aux = aux.getSiguiente();
        }
        return res;
    }

//    public NodoSimple<INodo<K, T>> buscarCada(K clave)
//    {
//        if (this.esVacia())
//        { return null; }
//
//        NodoSimple<INodo<K, T>> res = new NodoSimple<>();
//        NodoSimple<INodo<K, T>> auxRes = new NodoSimple<>();
//        res.setSiguiente(auxRes);
//
//        INodo<K, T> aux = this.primero;
//        while (aux != null)
//        {
//            if (aux.getEtiqueta().equals(clave))
//            {
//                auxRes.setSiguiente(new NodoSimple<>(aux));
//                auxRes = auxRes.getSiguiente();
//            }
//            aux = aux.getSiguiente();
//        }
//        return res.getSiguiente();
//    }

//    @Override
//    public ListaMejorada<T> buscarCadaMejor(Integer clave)
//    {
//        if (this.esVacia())
//        { return null; }
//
//        ListaMejorada<T> res = new ListaMejorada<>();
//
//        INodo<Integer, T> aux = (INodo<Integer, T>) this.primero;
//        while (aux != null)
//        {
//            if (aux.getEtiqueta().equals(clave))
//            { res.insertar(aux); }
//            aux = aux.getSiguiente();
//        }
//        return res;
//    }

    @Override
    public boolean existe(K clave)
    { return buscar(clave) != null; }

    @Override
    public boolean existeDato(T dato)
    { return buscarDato(dato) != null; }

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

    @Override
    public String imprimir()
    { return this.imprimir(ETIQUETA + DATO); }

    @Override
    public String imprimir(int labels)
    { return this.imprimir(labels, " : "); }

    @Override
    public String imprimir(String separador)
    { return this.imprimir(ETIQUETA + DATO, separador); }

    @Override
    public String imprimir(int labels, String separador)
    { return this.imprimir(ETIQUETA + DATO, separador, ", "); }

    @Override
    public String imprimir(int labels, String separador, String separadorNodo)
    {
        if (this.esVacia())
        { return ""; }

        StringBuilder res = new StringBuilder(this.primero.imprimir(labels, separadorNodo));
        INodo<K, T> aux = this.primero.getSiguiente();
        while (aux != null)
        {
            res.append(separador);
            res.append(aux.imprimir(labels, separadorNodo));
            aux = aux.getSiguiente();
        }
        return res.toString();
    }

    public String imprimirEtiquetas(String separador)
    { return imprimir(ETIQUETA, separador); }

    public String imprimirDatos(String separador)
    { return imprimir(DATO, separador); }

    @Override
    public int length()
    { return this.largo; }

    @Override
    public int cantElementos()
    { return length(); }

    @Override
    public boolean esVacia()
    { return this.cantElementos() == 0; }

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

/*
    // Radix LSD Bucket Sort
    @Override
    public void ordenar(int tipo)
    {
        if (this.largo < 2)
        { return; }

        ILista<K, T>[] buckets = new ILista[37];
//        ILista<K, T>[] buckets = (ILista<K, T>[]) Array.newInstance(this.getClass(), 37);

        // Obtener LSD
        int longestLen = 0;
        INodo<K, T> aux = this.primero;
        while (aux != null)
        {
            if (aux.getLabel(ETIQUETA).length() > longestLen)
            { longestLen = getLabel(aux, tipo).length(); }
            aux = aux.getSiguiente();
        }

        // i = Lugar actual en el string (de LSD a MSD)
        for (int i = longestLen - 1; i >= 0; i--)
        {
            // Reiniciar buckets
            for (short j = 0; j < buckets.length; j++)
            { buckets[j] = new Lista<>(); }

            // Insertar en buckets
            aux = this.primero;
            while (aux != null)
            {
                INodo<K, T> siguiente = aux.getSiguiente();
                aux.setSiguiente(null);

                short c = 0;
                if (aux.getLabel(ETIQUETA).length() > i)
                { c = getIndex(getLabel(aux, tipo).charAt(i)); }
                buckets[c].insertar(aux);

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
*/

    private String getLabel(@NotNull INodo<K, T> nodo, int i)
    { return nodo.getLabel(i); }

//    public static ILista ordenar(ILista lista)
//    { return lista; /* 🎵HopeSort */ }
}
