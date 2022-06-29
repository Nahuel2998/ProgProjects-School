package org.classes;

import javax.naming.NameNotFoundException;
import java.util.Locale;
import java.util.StringJoiner;

public class Almacen<K extends Comparable<K>> implements IAlmacen<K, Producto<K>>
{
    private final String nombre;
    private String direccion;
    private String telefono;
    private final ArbolBB<K, Producto<K>> productos;

    /**
     * Separador utilizado entre elemento y elemento al imprimir la lista
     */
    public static final String SEPARADOR_ELEMENTOS_IMPRESOS = "\n- - - - - - -\n";

    public Almacen(String nombre)
    {
        this.nombre = nombre;
        this.productos = new ArbolBB<>();
    }

    @Override
    public void insertarProducto(K etiqueta, Producto<K> unProducto)
    {
        if (!this.productos.insertarBalanceado(new ElementoAB<>(etiqueta, unProducto)))
        { throw new IllegalArgumentException(String.format("Producto %s ya existe.", etiqueta.toString())); }
    }

    @Override
    public String imprimirProductos()
    { return imprimirProductos(SEPARADOR_ELEMENTOS_IMPRESOS); }

    @Override
    public String imprimirProductos(String separador)
    {
        StringJoiner res = new StringJoiner(separador);

        for (Producto<K> producto : this.productos.inOrden())
        { res.add(producto.toString()); }

        return res.toString();
    }

    @Override
    public Boolean agregarStock(K clave, Integer cantidad)
    {
        IElementoAB<K, Producto<K>> elementoProducto = this.productos.buscar(clave);
        if (elementoProducto == null)
        { return false; }

        elementoProducto.getDatos().agregarStock(cantidad);
        return true;
    }

    @Override
    public Integer restarStock(K clave, Integer cantidad) throws NameNotFoundException
    {
        IElementoAB<K, Producto<K>> elementoProducto = this.productos.buscar(clave);
        if (elementoProducto == null)
        { throw new NameNotFoundException(String.format("Producto '%s' no encontrado", clave)); }

        return elementoProducto.getDatos().restarStock(cantidad);
    }

    @Override
    public Producto<K> buscarPorCodigo(K clave)
    {
        IElementoAB<K, Producto<K>> elementoProducto = this.productos.buscar(clave);
        if (elementoProducto == null)
        { return null; }

        return elementoProducto.getDatos();
    }

    @Override
    public boolean existeProducto(K clave)
    { return this.productos.existe(clave); }

    @Override
    public boolean eliminarProducto(K clave)
    {
        if (!existeProducto(clave))
        { return false; }

        this.productos.eliminarBalanceado(clave);
        return true;
    }

    @Override
    public int getValorAgregado()
    {
        int res = 0;

        Lista<K, Producto<K>> listaProductos = productos.inOrden();
        if (listaProductos != null)
        {
            for (Producto<K> producto : listaProductos)
            { res += producto.getValorAgregado(); }
        }

        return res;
    }

    public ILista<String, Integer> obtenerListaDeProductosOrdenadosPorDescripcion()
    {
        ArbolBB<String, Integer> res = new ArbolBB<>();

        for (Producto<K> producto : this.productos.inOrden())
        { res.insertar(new ElementoAB<>(producto.getNombre(), producto.getPrecio())); }

        return res.inOrden();
    }

    public ILista<K, Producto<K>> obtenerDescripcionesSimilares(String busqueda)
    {
        ILista<K, Producto<K>> res = new Lista<>();

        for (Producto<K> producto : this.productos.inOrden())
        {
            if (producto.getNombre().toLowerCase().contains(busqueda.toLowerCase()))
            { res.insertar(new Nodo<>(producto.getEtiqueta(), producto)); }
        }

        return res;
    }
}
