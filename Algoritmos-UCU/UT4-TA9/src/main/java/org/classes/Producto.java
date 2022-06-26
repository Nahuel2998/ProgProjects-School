package org.classes;

public class Producto<K extends Comparable<K>> implements IProducto<K>
{
    private final K etiqueta;
    private String nombre;
    private Integer precio;
    private Integer stock;

    public Producto(K etiqueta, String nombre)
    {
        this.etiqueta = etiqueta;
        this.nombre = nombre;
        this.precio = 0;
        this.stock = 0;
    }

    public Producto(K etiqueta, String nombre, int precio, int stock)
    {
        this.etiqueta = etiqueta;
        this.nombre = nombre;
        this.precio = precio;
        this.stock = stock;
    }

    @Override
    public String getNombre()
    { return this.nombre; }

    @Override
    public K getEtiqueta()
    { return this.etiqueta; }

    @Override
    public Integer getPrecio()
    { return this.precio; }

    @Override
    public void setPrecio(Integer precio)
    { this.precio = precio; }

    @Override
    public Integer getStock()
    { return this.stock; }

    public void agregarStock(Integer stock)
    { this.stock += stock; }

    // restarStock devolverá -1 si se pretende extraer más de lo que hay...
    // y el campo stock quedará inalterado
    public Integer restarStock(Integer stock)
    {
        if (stock > this.stock)
        { return -1; }

        setStock(this.stock - stock);
        return this.stock;
    }

    @Override
    public void setNombre(String nombre)
    { this.nombre = nombre; }

    @Override
    public void setStock(Integer stock)
    { this.stock = stock; }

//    @Override
//    public Comparable getEtiqueta()
//    { return this.etiqueta; }

    @Override
    public String toString()
    {
        return String.format("Id: %s\nNombre: %s\nPrecio: %d\nStock: %d",
                this.etiqueta.toString(),
                this.nombre,
                this.precio,
                this.stock);
    }

    @Override
    public int getValorAgregado()
    { return this.stock * this.precio; }
}
