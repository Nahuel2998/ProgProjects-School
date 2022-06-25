package org.classes;

public interface IArbolBB<K extends Comparable<K>, T> {

    /**
     * Inserta un elemento en el arbol. En caso de ya existir un elemento con la
     * clave indicada en "unElemento", retorna falso.
     *
     * @param unElemento Elemento a insertar
     * @return Exito de la operacián
     */

    boolean insertar(IElementoAB<K, T> unElemento);

    /**
     * Busca un elemento dentro del árbol.
     *
     *
     * @param unaEtiqueta Etiqueta identificadora del elemento a buscar.
     * .
     * @return Elemento encontrado. En caso de no encontrarlo, retorna nulo.
     */
    IElementoAB<K, T> buscar(K unaEtiqueta);

    /**
     * Imprime en PreOrden los elementos del árbol, separados por guiones.
     *
     * @return String conteniendo el preorden separado por guiones.
     */
//    String preOrden();

    /**
     * Imprime en InOrden los elementos del árbol, separados por guiones.
     *
     * @return String conteniendo el preorden separado por guiones.
     */
    String inOrden();

    /**
     * @return una Lista con los elementos del recorrido.
     *
     */
    ILista<K, T> inorden();

    /**
     * Imprime en PostOrden los elementos del árbol, separados por guiones.
     *
     * @return String conteniendo el preorden separado por guiones.
     */
//    String postOrden();

    int obtenerAltura();

    /**
     * Retorna el tama�o del arbol.
     *
     * @return Tama�o del arbol.
     */
//    int obtenerTamanio();

    /**
     * Retorna el nivel del arbol a partir de la etiqueta indicada
     *
     * @param unaEtiqueta
     * @return Nivel
     */
    int obtenerNivel(K unaEtiqueta);

    /**
     * Retorna la cantidad de hojas del arbol.
     *
     * @return Cantidad de hojas del arbol.
     */
//    int obtenerCantidadHojas();

    /**
     * Elimina un elemento dada una etiqueta.
     * @param unaEtiqueta
     */
    void eliminar(K unaEtiqueta);
}

