package org.classes;

public interface IProducto <K extends Comparable<K>>
{
    /**
     * Retorna el codigo del Producto.
     *
     * @return codigo del Producto.
     */
    K getEtiqueta();

    /**
     * Retorna el precio unitario del Producto.
     *
     * @return precio del Producto.
     */
    Integer getPrecio();

    void setPrecio(Integer precio);

    /**
     * Retorna el stock del Producto.
     *
     * @return stock del Producto.
     */
    Integer getStock();

    void setStock(Integer stock);

    /**
     * Retorna la descripcion/nombre del Producto.
     *
     * @return descripci�n del Producto.
     */
    String getNombre();

    void setNombre(String nombre);

    int getValorAgregado();
}
