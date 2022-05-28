package org.classes;

import org.jetbrains.annotations.NotNull;

public interface IElementoAB<K extends Comparable<K>, T> {

    /**
     * Obtiene el valor de la etiqueta del nodo.
     *
     * @return Etiqueta del nodo.
     */
    public K getEtiqueta();

    /**
     * Obtiene el hijo izquierdo del nodo.
     *
     * @return Hijo Izquierdo del nodo.
     */
    public IElementoAB<K, T> getHijoIzq();

    /**
     * Obtiene el hijo derecho del nodo.
     *
     * @return Hijo derecho del nodo.
     */
    public IElementoAB<K, T> getHijoDer();

    /**
     * Asigna el hijo izquierdo del nodo.
     *
     * @return Elemento a ser asignado como hijo izquierdo.
     */
    public void setHijoIzq(IElementoAB<K, T> elemento);

    /**
     * Asigna el hijo derecho del nodo.
     *
     * @return Elemento a ser asignado como hijo derecho.
     */
    public void setHijoDer(IElementoAB<K, T> elemento);

    /**
     * Busca un elemento dentro del arbol con la etiqueta indicada.
     *
     * @param unaEtiqueta del nodo a buscar
     * @return Elemento encontrado. En caso de no encontrarlo, retorna nulo.
     */
    public IElementoAB<K, T> buscar(K unaEtiqueta);

    /**
     * Inserta un elemento dentro del arbol.
     *
     * @param elemento Elemento a insertar.
     * @return Exito de la operaci�n.
     */
    public boolean insertar(IElementoAB<K, T> elemento);

    /**
     * Imprime en preorden el arbol separado por guiones.
     *
     * @return String conteniendo el PreOrden
     */
    public String preOrden();

    /**
     * Imprime en inorden el arbol separado por guiones.
     *
     * @return String conteniendo el InOrden
     */
    public String inOrden();

    /**
     * Imprime en postorden el arbol separado por guiones.
     *
     * @return String conteniendo el PostOrden
     */
//    public String postOrden();

    /**
     * Retorna los datos contenidos en el elemento.
     *
     * @return
     */
    public T getDatos();
	
	 /**
     * Elimina un elemento dada una etiqueta.
     * @param
     * @return 
     */
    IElementoAB<K, T> eliminar(K etiqueta);
    IElementoAB<K, T> quitarNodo();

//    StringBuilder inOrdenBuilder(String separador);

    StringBuilder inOrden(String separador);
    StringBuilder preOrden(String separador);


    int obtenerNivel(K etiqueta);
    int obtenerNivel(K etiqueta, int nivel);

    int obtenerAltura();
}
