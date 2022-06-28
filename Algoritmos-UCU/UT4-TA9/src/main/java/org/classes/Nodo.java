package org.classes;

import org.jetbrains.annotations.NotNull;

public class Nodo<K extends Comparable<K>, T> implements INodo<K, T>
{
    public static final int DATO = 2;
    public static final int ETIQUETA = 3;

    private final K etiqueta;
    private T dato;
    private INodo<K, T> siguiente = null;

    public Nodo()
    { this.etiqueta = null; }

    public Nodo(K etiqueta, T dato)
    {
        this.etiqueta = etiqueta;
        this.dato = dato;
    }

    public T getDato()
    { return this.dato; }

    public void setDato(T dato)
    { this.dato = dato; }

    @Override
    public K getEtiqueta()
    { return this.etiqueta; }

    @Override
    public String imprimir()
    { return imprimir(ETIQUETA + DATO); }

    @Override
    public String imprimir(String separador)
    { return imprimir(ETIQUETA + DATO, separador); }

    @Override
    public String imprimir(int labels)
    { return this.imprimir(labels, ", "); }

    @Override
    // El parametro labels debera ser una suma de constantes indicando las labels que se quieran.
    // Ejemplo: (ETIQUETA + DATO)
    public String imprimir(int labels, String separador)
    {
        if (labels > ETIQUETA * DATO || labels < 1)
        { throw new IllegalArgumentException("Suma de labels incorrecta."); }

        StringBuilder res = new StringBuilder();

        if (labels % ETIQUETA == 0)
        {
            res.append(this.getLabel(ETIQUETA));
            res.append(separador);
        }

        if (labels % DATO == 0)
        {
            res.append(this.getLabel(DATO));
            res.append(separador);
        }

        if (!res.isEmpty())
        { res.setLength(res.length() - separador.length()); }

        return res.toString();
    }

    @Override
    public String imprimirEtiqueta()
    { return this.getLabel(ETIQUETA); }

    public String imprimirDato()
    { return this.getLabel(DATO); }

    public INodo<K, T> clonar()
    { return new Nodo<>(this.etiqueta, this.dato); }

    public boolean equals(@NotNull Nodo<K, T> unNodo)
    { return this.dato.equals(unNodo.getDato()); }

//    @Override
//    public int compareTo(K etiqueta)
//    { return this.etiqueta.compareTo(etiqueta); }

    @Override
    public INodo<K, T> getSiguiente()
    { return this.siguiente; }

    @Override
    public void setSiguiente(INodo<K, T> nodo)
    { this.siguiente = nodo; }

    @Override
    public String getLabel(int label)
    {
        return switch (label) {
            case ETIQUETA -> this.getEtiqueta().toString();
            case DATO -> this.getDato().toString();
            default -> throw new IllegalArgumentException("Label invalida.");
        };
    }
}
