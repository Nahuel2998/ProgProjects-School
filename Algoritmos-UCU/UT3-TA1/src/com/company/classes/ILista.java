package com.company.classes;

interface ILista
{
    /**
     * M�todo encargado de agregar un nodo al final de la lista.
     *
     * @param nodo - Nodo a agregar
     */
    void insertarFinal(INodo nodo);

    /**
     * M�todo encargado de agregar un nodo al comienzo de la lista.
     *
     * @param nodo - Nodo a agregar
     */
    void insertarComienzo(INodo nodo);

    /**
     * M�todo encargado de buscar un nodo cuya clave es la indicada.
     *
     * @param clave - Clave del nodo a buscar.
     * @return El nodo encontrado. En caso de no encontrarlo, retornar null.
     */
    INodo buscar(String clave);
    boolean existe(String clave);

    INodo getAt(int indice);

    /**
     * M�todo encargado de eliminar un nodo cuya clave es la indicada.
     *
     * @param clave Clave del nodo a eliminar.
     * @return True en caso de que la eliminaci�n haya sido efectuada con �xito.
     */
    boolean eliminar(String clave);

    boolean eliminarAt(int indice);

    /**
     * M�todo encargado de imprimir en consola las claves de los nodos
     * contenidos en la lista.
     */
    String imprimir();

    /**
     * Retorna un String con las claves separadas por el separador pasado por
     * par�metro.
     *
     * @param separador Separa las claves
     * @return
     */
    String imprimir(String separador);


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

    /**
     * Retorna el primer nodo de la lista.
     *
     * @return Primer nodo de la lista.
     */
    INodo getPrimero();

    /**
     * Retorna el ultimo nodo de la lista.
     *
     * @return Ultimo nodo de la lista.
     */
    INodo getUltimo();

}
