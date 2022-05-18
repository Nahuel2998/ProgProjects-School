package org.classes;

import org.jetbrains.annotations.NotNull;

public interface ILista<K extends Comparable<K>, T>
{
    void insertar(INodo<K, T> nodo);
    void insertar(@NotNull ILista<K, T> lista);

    INodo<K, T> buscar(K clave);
    INodo<K, T> buscarDato(T dato);
    ILista<K, T> buscarCada(K clave);
//    ListaMejorada<T> buscarCadaMejor(Integer clave);

    boolean eliminar(K clave);

    String imprimir();
    String imprimir(int labels);

    String imprimir(String separador);
    String imprimir(int labels, String separador);
    String imprimir(int labels, String separador, String separadorNodo);

    int cantElementos();

    boolean esVacia();

    INodo<K, T> getPrimero();
    INodo<K, T> getUltimo();

    boolean existe(K clave);
    boolean existeDato(T dato);

//    void ordenar(int tipo);
}
