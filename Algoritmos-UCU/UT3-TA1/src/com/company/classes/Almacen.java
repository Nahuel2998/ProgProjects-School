package com.company.classes;

import javax.naming.NameNotFoundException;

public class Almacen
{
    private final ILista almacen;

    public Almacen()
    { this.almacen = new Lista(); }

    // Returns false if element was already on the list
    public boolean agregarProducto(IProducto producto)
    {
        if (this.existe(producto.getId()))
        { return false; }

        this.almacen.insertarFinal(producto);
        return true;
    }

    public boolean eliminarProducto(String id)
    { return this.almacen.eliminar(id); }

    // Throws NameNotFoundException if element was not found
    public void aumentarStock(String id, int cantidad) throws NameNotFoundException, IllegalArgumentException
    {
        if (!this.existe(id))
        { throw new NameNotFoundException(id); }

        ((IProducto) this.almacen.buscar(id)).aumentarStock(cantidad);
    }

    public void aumentarStock(String id) throws NameNotFoundException, IllegalArgumentException
    { this.aumentarStock(id, 1); }

    // Throws NameNotFoundException if element was not found
    public boolean venderProducto(String id, int cantidad) throws NameNotFoundException, IllegalArgumentException
    {
        if (!this.existe(id))
        { throw new NameNotFoundException(id); }

        return ((IProducto) this.almacen.buscar(id)).reducirStock(cantidad);
    }

    public boolean venderProducto(String id) throws NameNotFoundException, IllegalArgumentException
    { return this.venderProducto(id, 1); }

    // Throws NameNotFoundException if element was not found
    public int getStock(String id) throws NameNotFoundException
    {
        if (!this.existe(id))
        { throw new NameNotFoundException(id); }

        return ((IProducto) this.almacen.buscar(id)).getStock();
    }

    public boolean existe(String id)
    { return this.almacen.existe(id); }
}
