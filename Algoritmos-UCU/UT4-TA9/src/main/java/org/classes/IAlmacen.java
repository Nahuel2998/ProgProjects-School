package org.classes;

import javax.naming.NameNotFoundException;

public interface IAlmacen<K extends Comparable<K>, T>
{
    /**
     * Incorporar un nuevo producto al supermercado.
     *
     * @param unProducto
     */
    void insertarProducto(K etiqueta, Producto<K> unProducto);

    /**
     * Eliminar productos que ya no se venden (por no ser comercializados m�s).
     *
     * @param clave
     * @return
     */
    boolean eliminarProducto(K clave);

    /**
     * Imprime la lista de productos
     *
     * @return
     */
    String imprimirProductos();

    String imprimirProductos(String separador);

    Boolean agregarStock(K clave, Integer cantidad);

    /**
     * Simular la venta de un producto (reducir el stock de un producto
     * existente
     *
     * @param clave
     * @param cantidad
     * @return
     */
    Integer restarStock(K clave, Integer cantidad) throws NameNotFoundException;

    /**
     * Dado un código de producto, indicar las existencias del mismo en el
     * almac�n.
     *
     * @param clave
     * @return
     */
    Producto buscarPorCodigo(K clave);

    /**
     * Dado un código de producto, retorna si existe o no (boolean)
     *
     * @param clave
     * @return
     */
    boolean existeProducto(K clave);

    int getValorAgregado();

    boolean vaciar();
}
