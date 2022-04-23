package com.company.classes;

public class Producto extends Nodo implements IProducto
{
    private int stock = 0;

    public Producto(String id, String nombre, float precio)
    { super(id, nombre, precio); }

    public Producto(String id, String nombre, float precio, int cantidad)
    {
        super(id, nombre, precio);
        this.stock = cantidad;
    }

    public int getStock()
    { return this.stock; }

    public void setStock(int cantidad)
    { this.stock = cantidad; }

    // Unsafe
    public void modificarStock(int cantidad)
    { this.setStock(this.stock + cantidad); }

    // Throws IllegalArgumentException if not adding
    public void aumentarStock(int cantidad) throws IllegalArgumentException
    {
        if (cantidad < 0)
        { throw new IllegalArgumentException(String.valueOf(cantidad)); }

        this.modificarStock(cantidad);
    }

    public void aumentarStock()
    { this.aumentarStock(1); }

    // Throws IllegalArgumentException if not subtracting
    // Returns false if stock < cantidad
    public boolean reducirStock(int cantidad) throws IllegalArgumentException
    {
        if (cantidad < 0)
        { throw new IllegalArgumentException(String.valueOf(cantidad)); }

        if (this.stock < cantidad)
        { return false; }

        this.modificarStock(-cantidad);
        return true;
    }

    public boolean reducirStock()
    { return this.reducirStock(1); }
}
