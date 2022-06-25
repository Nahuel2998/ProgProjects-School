package org.classes;

public class ArbolBB<K extends Comparable<K>, T> implements IArbolBB<K, T> {

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
        else
        { return raiz.insertar(unElemento); }
    }

    /**
     * @param unaEtiqueta
     * @return
     */
    public IElementoAB<K, T> buscar(K unaEtiqueta)
    { return this.esVacio() ? null : this.raiz.buscar(unaEtiqueta); }

    /**
     * @return recorrida en inorden del arbol, null en caso de ser vacío
     */
    public String inOrden()
    { return esVacio() ? null : this.raiz.inOrden(); }

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
    public ILista<K, T> inorden()
    {
        Lista<K, T> listaInorden = null;
        if (!this.esVacio())
        {
            listaInorden = new Lista<>();
            this.raiz.inOrden(listaInorden);
        }
        return listaInorden;
    }

    @Override
    public int obtenerAltura()
    { return this.raiz.obtenerAltura(); }

    @Override
    public int obtenerNivel(K unaEtiqueta)
    { return this.raiz.obtenerNivel(unaEtiqueta); }

    @Override
    public void eliminar(K unaEtiqueta)
    {
        if (!this.esVacio())
        { this.raiz = this.raiz.eliminar(unaEtiqueta); }
    }
}
