package com.company.classes;

public interface IProducto extends INodo
{
    int getStock();
    void setStock(int cantidad);
    void modificarStock(int cantidad);
    // Throws IllegalArgumentException if not adding
    void aumentarStock(int cantidad);
    void aumentarStock();
    // Throws IllegalArgumentException if not subtracting
    // Returns false if stock < cantidad
    boolean reducirStock(int cantidad);
    boolean reducirStock();
}
