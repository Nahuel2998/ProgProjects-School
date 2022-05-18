package org.classes;

public interface INodo<K extends Comparable<K>, T>
{
    public T getDato();

    public INodo<K, T> getSiguiente();
    
    public void setSiguiente(INodo<K, T> nodo);
    
    String imprimir();
    String imprimir(String separador);
    String imprimir(int labels);
    String imprimir(int labels, String separador);

    String imprimirEtiqueta();
    public String getLabel(int label);

    K getEtiqueta();
}
