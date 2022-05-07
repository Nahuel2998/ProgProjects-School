package org.classes;

import org.jetbrains.annotations.NotNull;

public class Nodo implements INodo
{
    public static final int ID = 2;
    public static final int NOMBRE = 3;
    public static final int PRECIO = 5;

    private final String id; // a-z 0-9 [20]
    private String nombre; // a-z 0-9 [30]
    private float precio;
    private INodo siguiente = null;

    public Nodo(String id)
    { this.id = id; }

    public Nodo(String id, String nombre, float precio)
    {
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
    }

    public float getPrecio()
    { return this.precio; }

    public void setPrecio(float precio)
    { this.precio = precio; }

    public String getId()
    { return this.id; }

    public String getNombre()
    { return nombre; }

    public void setNombre(String nombre)
    { this.nombre = nombre; }

    public void setSiguiente(INodo nodo)
    { this.siguiente = nodo; }

    public INodo getSiguiente()
    { return this.siguiente; }

    public INodo clonar()
    { return new Nodo(this.id, this.nombre, this.precio); }

    @Override
    public boolean equals(@NotNull INodo nodo)
    { return this.nombre.equals(nodo.getNombre()) && this.precio == nodo.getPrecio(); }

    // labels indicara los labels que se quieren imprimir, usando las constantes
    // Para indicar mas de un label, multiplicar sus constantes
    // Ejemplo: imprimir(ID*PRECIO, " : ") -> id : precio
    public String imprimir(int labels, String separador)
    {
        StringBuilder res = new StringBuilder();

        if (labels % ID == 0)
        {
            res.append(this.getId());
            res.append(separador);
        }
        if (labels % NOMBRE == 0)
        {
            res.append(this.getNombre());
            res.append(separador);
        }
        if (labels % PRECIO == 0)
        {
            res.append(this.getPrecio());
            res.append(separador);
        }

        if (!res.isEmpty())
        { res.setLength(res.length() - separador.length()); }

        return res.toString();
    }

    public String imprimir(int labels)
    { return this.imprimir(labels, " : "); }

    public String imprimir()
    { return this.imprimir(ID*NOMBRE*PRECIO); }

//    // @Override
//    public int compareTo(Comparable etiqueta)
//    { return this.etiqueta.compareTo(etiqueta); }
}
