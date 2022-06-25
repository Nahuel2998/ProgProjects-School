package org.classes;

public class Almacen<K extends Comparable<K>> implements IAlmacen<K, Producto>
{
    private String nombre;
    private String direccion;
    private String telefono;

    private ArbolBB<K, Producto> productos;

    public Almacen(String nombre)
    {
        this.nombre = nombre;
        this.productos = new ArbolBB<>();
    }

    @Override
    public void insertarProducto(Producto unProducto)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public String imprimirProductos()
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public Boolean agregarStock(K clave, Integer cantidad)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public Integer restarStock(K clave, Integer cantidad)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public Producto buscarPorCodigo(K clave)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public boolean eliminarProducto(K clave)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
}
