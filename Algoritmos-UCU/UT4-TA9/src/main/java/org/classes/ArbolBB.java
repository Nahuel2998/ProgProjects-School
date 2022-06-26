package org.classes;

import org.jetbrains.annotations.NotNull;

public class ArbolBB<K extends Comparable<K>, T> implements IArbolBB<K, T>
{
    private IElementoAB<K, T> raiz;

    /**
     * Separador utilizado entre elemento y elemento al imprimir la lista
     */
    public static final String SEPARADOR_ELEMENTOS_IMPRESOS = "-";

    public ArbolBB()
    { this.raiz = null; }

    public ArbolBB(IElementoAB<K, T> raiz)
    { this.raiz = raiz; }

    /**
     * @param unElemento
     * @return
     */
    public boolean insertar(IElementoAB<K, T> unElemento)
    {
        if (this.esVacio())
        {
            this.raiz = unElemento;
            return true;
        }

        return this.raiz.insertar(unElemento);
    }

    @Override
    public boolean insertarBalanceado(IElementoAB<K, T> nuevoNodo)
    {
        try
        { this.raiz = this.insertarBalanceado(this.raiz, nuevoNodo); }
        catch (IllegalArgumentException e)
        { return false; }

        return true;
    }

    private IElementoAB<K, T> insertarBalanceado(IElementoAB<K, T> raiz, IElementoAB<K, T> nuevoNodo) throws IllegalArgumentException
    {
        if (raiz == null)
        { return nuevoNodo; }
        else if (raiz.getEtiqueta().compareTo(nuevoNodo.getEtiqueta()) > 0)
        { raiz.setHijoIzq(insertarBalanceado(raiz.getHijoIzq(), nuevoNodo)); }
        else if (raiz.getEtiqueta().compareTo(nuevoNodo.getEtiqueta()) < 0)
        { raiz.setHijoDer(insertarBalanceado(raiz.getHijoDer(), nuevoNodo)); }
        else
        { throw new IllegalArgumentException("El elemento ya se encuentra en el arbol."); }

        return balancear(raiz);
    }

    /**
     * @param unaEtiqueta
     * @return
     */
    public IElementoAB<K, T> buscar(K unaEtiqueta)
    { return this.esVacio() ? null : this.raiz.buscar(unaEtiqueta); }

    @Override
    public boolean existe(K unaEtiqueta)
    { return buscar(unaEtiqueta) != null; }

    /**
     * @return recorrida en inorden del arbol, null en caso de ser vacío
     */
    public String inOrdenString()
    { return esVacio() ? null : this.raiz.inOrden(SEPARADOR_ELEMENTOS_IMPRESOS); }

    /**
     * @return recorrida en preOrden del arbol, null en caso de ser vacío
     */
    /**
     * @return
     */
    public boolean esVacio()
    { return (this.raiz == null); }

    /**
     * @return True si habían elementos en el árbol, false en caso contrario
     */
    public boolean vaciar()
    {
        if (!this.esVacio())
        {
            this.raiz = null;
            return true;
        }
        return false;
    }

    @Override
    public Lista<K, T> inOrden()
    {
        Lista<K, T> listaInOrden = null;
        if (!this.esVacio())
        {
            listaInOrden = new Lista<>();
            this.raiz.inOrden(listaInOrden);
        }
        return listaInOrden;
    }

    @Override
    public int obtenerAltura()
    { return this.raiz.obtenerAltura(); }

    @Override
    public int obtenerNivel(K unaEtiqueta)
    { return this.raiz.obtenerNivel(unaEtiqueta); }

    @Override
    public IElementoAB<K, T> balancear(@NotNull IElementoAB<K, T> nodo)
    {
        nodo.actualizarAltura();
        int balance = nodo.obtenerBalance();

        if (balance > 1)
        {
            if (nodo.getHijoDer().obtenerAlturaHijoDer() < nodo.getHijoDer().obtenerAlturaHijoIzq())
            { nodo.setHijoDer(rotarDerecha(nodo.getHijoDer())); }
            nodo = rotarIzquierda(nodo);
        }
        else if (balance < -1)
        {
            if (nodo.getHijoIzq().obtenerAlturaHijoIzq() < nodo.getHijoIzq().obtenerAlturaHijoDer())
            { nodo.setHijoIzq(rotarIzquierda(nodo.getHijoIzq())); }
            nodo = rotarDerecha(nodo);
        }
        return nodo;
    }

    private @NotNull IElementoAB<K, T> rotarIzquierda(@NotNull IElementoAB <K, T> nodo)
    {
        IElementoAB<K, T> hijoDer = nodo.getHijoDer();
        IElementoAB<K, T> hijoDerHijoIzq = hijoDer.getHijoIzq();

        hijoDer.setHijoIzq(nodo);
        nodo.setHijoDer(hijoDerHijoIzq);

        nodo.actualizarAltura();
        hijoDer.actualizarAltura();

        return hijoDer;
    }

    private @NotNull IElementoAB<K, T> rotarDerecha(@NotNull IElementoAB <K, T> nodo)
    {
        IElementoAB<K, T> hijoIzq = nodo.getHijoIzq();
        IElementoAB<K, T> hijoIzqHijoDer = hijoIzq.getHijoDer();

        hijoIzq.setHijoDer(nodo);
        nodo.setHijoIzq(hijoIzqHijoDer);

        nodo.actualizarAltura();
        hijoIzq.actualizarAltura();

        return hijoIzq;
    }

    @Override
    public void eliminar(K unaEtiqueta)
    {
        if (!this.esVacio())
        { this.raiz = this.raiz.eliminar(unaEtiqueta); }
    }

    @Override
    public void eliminarBalanceado(K unaEtiqueta)
    { this.raiz = this.eliminarBalanceado(raiz, unaEtiqueta); }

    private IElementoAB<K, T> eliminarBalanceado(IElementoAB<K, T> raiz, K unaEtiqueta)
    {
        if (raiz == null)
        { return null; }
        else if (raiz.getEtiqueta().compareTo(unaEtiqueta) > 0)
        { raiz.setHijoIzq(eliminarBalanceado(raiz.getHijoIzq(), unaEtiqueta)); }
        else if (raiz.getEtiqueta().compareTo(unaEtiqueta) < 0)
        { raiz.setHijoDer(eliminarBalanceado(raiz.getHijoDer(), unaEtiqueta)); }
        else
        {
            if (raiz.esHoja())
            { return null; }

            if (raiz.getHijoIzq() == null)
            { raiz = raiz.getHijoDer(); }
            else if (raiz.getHijoDer() == null)
            { raiz = raiz.getHijoIzq(); }
            else
            {
                IElementoAB<K, T> hojaIzquierda = raiz.getHijoDer().obtenerHojaIzquierda();
                raiz.setEtiqueta(hojaIzquierda.getEtiqueta());
                raiz.setDatos(hojaIzquierda.getDatos());
                raiz.setHijoDer(eliminarBalanceado(raiz.getHijoDer(), hojaIzquierda.getEtiqueta()));
            }
        }
        return this.balancear(raiz);
    }
}
