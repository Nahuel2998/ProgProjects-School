package org.classes;

public interface INodo<K extends Comparable<K>, T>
{
    /**
     * devuelve el dato del nodo
     * @return 
     */
    public T getDato();

    /**
     * devuelve el siguiente del nodo
     * @return 
     */
    public INodo<K, T> getSiguiente();
    
    /**
     * "engancha" otro nodo a continuacion
     * 
     */
    public void setSiguiente(INodo<K, T> nodo);
    
    /**
     * Imprime los datos del nodo
     */
    String imprimir();
    String imprimir(String separador);
    String imprimir(int labels);
    String imprimir(int labels, String separador);

    /**
     * Retorna la etiqueta del nodo como String
     */
    String imprimirEtiqueta();
    public String getLabel(int label);

    /**
     * Retorna la etiqueta del nodo
     *
     * @return etiqueta del nodo
     */
    K getEtiqueta();

//	/**
//	 *
//	 * @param unNodo
//	 * @return devueve -1 si this tiene una clave menor, 0 si son iguales, 1 si es mayor
//	 */
//	public int compareTo(INodo<E> unNodo);

    /**
     *
     * @param etiqueta
     * @return devueve -1 si this tiene una etiqueta menor, 0 si son iguales, 1
     * si es mayor
     */
//    int compareTo(K etiqueta);
}
