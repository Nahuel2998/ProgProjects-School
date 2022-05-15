package org.classes;

public interface ILista<K extends Comparable<K>, T> {

    /**
     * Metodo encargado de agregar un nodo al final de la lista.
     *
     * @param nodo - Nodo a agregar
     */
    void insertarComienzo(INodo<K, T> nodo);
    void insertarFinal(INodo<K, T> nodo);

    /**
     * Metodo encargado de buscar un nodo cuya clave es la indicada.
     *
     * @param clave - Clave del nodo a buscar.
     * @return El nodo encontrado. En caso de no encontrarlo, retornar null.
     */
    INodo<K, T> buscar(K clave);

    /**
     * Metodo encargado de eliminar un nodo cuya clave es la indicada.
     *
     * @param clave Clave del nodo a eliminar.
     * @return True en caso de que la eliminaci�n haya sido efectuada con �xito.
     */
    boolean eliminar(K clave);

    /**
     * Metodo encargado de imprimir en consola las claves de los nodos
     * contenidos en la lista.
     * @return 
     */
//    String imprimir();

    /**
     * Retorna un String con las claves separadas por el separador pasado por
     * par�metro.
     *
     * @param separador Separa las claves
     * @return
     */
//    String imprimir(String separador);

    /**
     * Retorna la cantidad de elementos de la lista. En caso de que la lista
     * este vac�a, retornar 0.
     *
     * @return Cantidad de elementos de la lista.
     */
    int cantElementos();

    /**
     * Indica si la lista contiene o no elementos.
     *
     * @return Si tiene elementos o no.
     */
    boolean esVacia();

    INodo<K, T> getPrimero();
    INodo<K, T> getUltimo();

    boolean existe(K clave);

    INodo<K, T> getAt(int indice);

    boolean eliminarAt(int indice);
}
