package org.classestemp;

public interface IPila<T>
{

    /**
     * Pone un dato de tipo T en el tope de la pila.
     * @param dato
     * @return
     */
    public boolean apilar(T dato);
    
    /**
     * Quita del tope de la pila el dato y lo devuelve.
     * @return
     */
    public T desapilar();
    
    /**
     * Devuelve el dato del tope de la pila sin quitarlo de la misma.
     * @return
     */
    public T tope();
    
    /**
     * Devuelve verdadero si la pila no contiene datos.
     * @return
     */
    public boolean esVacia();
    
    /**
     * Quita todos los elementos de la pila.
     */
    public void vaciar();
    
    /**
     * De acuerdo al tamaño inicial de la pila, devuelve verdadero si la cantidad de elementos en la pila es la misma que la capacidad (tamaño).
     * @return
     */
    public boolean estaLlena();
}
