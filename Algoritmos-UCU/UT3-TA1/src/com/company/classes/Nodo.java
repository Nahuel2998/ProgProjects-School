package com.company.classes;

public class Nodo implements INodo
{
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
    public boolean equals(INodo nodo)
    { return this.nombre.equals(nodo.getNombre()) && this.precio == nodo.getPrecio(); }

//    // @Override
//    public int compareTo(Comparable etiqueta)
//    { return this.etiqueta.compareTo(etiqueta); }
}
