package org.classes;

public class ArbolBB<K extends Comparable<K>, T> implements IArbolBB<K, T> {

    private IElementoAB<K, T> raiz;

    /**
     * Separador utilizado entre elemento y elemento al imprimir la lista
     */
    public static final String SEPARADOR_ELEMENTOS_IMPRESOS = "-";

    public ArbolBB()
    { raiz = null; }

    /**
     * @param unElemento
     * @return
     */
    public boolean insertar(IElementoAB<K, T> unElemento) {
        if (esVacio()) {
            raiz = unElemento;
            return true;
        } else {
            return raiz.insertar(unElemento);
        }
    }

    /**
     * @param unaEtiqueta
     * @return
     */
    public IElementoAB<K, T> buscar(K unaEtiqueta) {
        if (esVacio()) {
            return null;
        } else {
            return raiz.buscar(unaEtiqueta);
        }
    }

    /**
     * @return recorrida en inorden del arbol, null en caso de ser vacío
     */
    public String inOrden()
    { return esVacio() ? null : raiz.inOrden(); }

    /**
     * @return recorrida en preOrden del arbol, null en caso de ser vacío
     */
    /**
     * @return
     */
    public boolean esVacio()
    { return (raiz == null); }

    /**
     * @return True si habían elementos en el árbol, false en caso contrario
     */
    public boolean vaciar()
    {
        if (!esVacio())
        {
            raiz = null;
            return true;
        }
        return false;
    }

    @Override
    public ILista<K, T> inorden()
    {
        Lista<K, T> listaInorden = null;
        if (!esVacio())
        {
            listaInorden = new Lista<>();
            raiz.inOrden(listaInorden);
        }
        return listaInorden;
    }

    @Override
    public int obtenerAltura() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public int obtenerTamanio() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public int obtenerNivel(K unaEtiqueta) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public int obtenerCantidadHojas() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

  @Override
    public void eliminar(K unaEtiqueta) {
        if (!esVacio()) {
            this.raiz = this.raiz.eliminar(unaEtiqueta);
        }
    }
   

}
