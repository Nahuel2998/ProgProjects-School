package org.classes;

public interface IListaIndexada<K extends Comparable<K>, T> extends ILista<K, T>
{
    void insertarComienzo(INodo<K, T> nodo);
    void insertarFinal(INodo<K, T> nodo);

    INodo<K, T> getAt(int indice);
    boolean eliminarAt(int indice);

    int length();
}
