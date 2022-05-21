package org.classestemp;

public class TElementoAB<T>
{
    private Comparable etiqueta;
    private TElementoAB<T> hijoIzq;
    private TElementoAB<T> hijoDer;
    private T datos;

    /**
     * @param unaEtiqueta
     * @param unosDatos
     */
    @SuppressWarnings("unchecked")
    public TElementoAB(Comparable unaEtiqueta, T unosDatos) {
        etiqueta = unaEtiqueta;
        datos = unosDatos;
    }

    public TElementoAB<T> getHijoIzq() {
        return hijoIzq;
    }

    public TElementoAB<T> getHijoDer() {
        return hijoDer;
    }

    /**
     * @param unElemento
     * @return
     */
    @SuppressWarnings("unchecked")

    public boolean insertar(TElementoAB<T> unElemento) {

    }

    /**
     * @param unaEtiqueta
     * @return
     */

    public TElementoAB<T> buscar(Comparable unaEtiqueta) {


    }

    /**
     * @return recorrida en inorden del subArbol que cuelga del elemento actual
     */

    public String inOrden() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void inOrden(Lista<T> unaLista) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.

    }

    public Comparable getEtiqueta() {
        return etiqueta;
    }
/**
     * @return
     */
    public String imprimir() {
        return (etiqueta.toString());
    }

    public T getDatos() {
        return datos;
    }

    public void setHijoIzq(TElementoAB<T> elemento) {
        this.hijoIzq = elemento;

    }

    public void setHijoDer(TElementoAB<T> elemento) {
        this.hijoDer = elemento;
    }

    public int obtenerAltura() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public int obtenerTamanio() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public int obtenerNivel(Comparable unaEtiqueta) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public int obtenerCantidadHojas() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public TElementoAB eliminar(Comparable unaEtiqueta) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    private TElementoAB quitaElNodo() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
}
