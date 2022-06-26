package org.classes;

import org.jetbrains.annotations.NotNull;

public interface IElementoAB<K extends Comparable<K>, T> {

    /**
     * Obtiene el valor de la etiqueta del nodo.
     *
     * @return Etiqueta del nodo.
     */
    K getEtiqueta();
    void setEtiqueta(K unaEtiqueta);

    /**
     * Obtiene el hijo izquierdo del nodo.
     *
     * @return Hijo Izquierdo del nodo.
     */
    IElementoAB<K, T> getHijoIzq();

    /**
     * Obtiene el hijo derecho del nodo.
     *
     * @return Hijo derecho del nodo.
     */
    IElementoAB<K, T> getHijoDer();

    /**
     * Asigna el hijo izquierdo del nodo.
     *
     * @return Elemento a ser asignado como hijo izquierdo.
     */
    void setHijoIzq(IElementoAB<K, T> elemento);

    /**
     * Asigna el hijo derecho del nodo.
     *
     * @return Elemento a ser asignado como hijo derecho.
     */
    void setHijoDer(IElementoAB<K, T> elemento);

    /**
     * Busca un elemento dentro del arbol con la etiqueta indicada.
     *
     * @param unaEtiqueta del nodo a buscar
     * @return Elemento encontrado. En caso de no encontrarlo, retorna nulo.
     */
    IElementoAB<K, T> buscar(K unaEtiqueta);

    void setDatos(T datos);

    /**
     * Inserta un elemento dentro del arbol.
     *
     * @param elemento Elemento a insertar.
     * @return Exito de la operaci�n.
     */
    boolean insertar(IElementoAB<K, T> elemento);

    /**
     * Imprime en preorden el arbol separado por guiones.
     *
     * @return String conteniendo el PreOrden
     */
    String preOrden();

    /**
     * Imprime en inorden el arbol separado por guiones.
     *
     * @return String conteniendo el InOrden
     */
    String inOrden();

    /**
     * Pone las etiquetas del recorrido en inorden en una TLista.
     *
     * @param unaLista
     */
    void inOrden(ILista<K, T> unaLista);

    /**
     * Imprime en postorden el arbol separado por guiones.
     *
     * @return String conteniendo el PostOrden
     */
//    String postOrden();

    /**
     * Retorna los datos contenidos en el elemento.
     *
     * @return
     */
    T getDatos();

    int obtenerAlturaHijoDer();

    int obtenerAlturaHijoIzq();

    int obtenerBalance();

    /**
     * Elimina un elemento dada una etiqueta.
     * @param
     * @return 
     */
    IElementoAB<K, T> eliminar(K etiqueta);
    IElementoAB<K, T> quitarNodo();

    String inOrden(String separador);
    StringBuilder preOrden(String separador);


    int obtenerNivel(K etiqueta);
    int obtenerNivel(K etiqueta, int nivel);

    int obtenerAltura();

    boolean esHoja();
    IElementoAB<K, T> obtenerHojaIzquierda();

    void actualizarAltura();

    /**
     * Retorna la cantidad de hojas del arbol cuya raiz es la del nodo actual.
     * @return Cantidad de hojas del subarbol.
     */
//    int obtenerCantidadHojas();
}
