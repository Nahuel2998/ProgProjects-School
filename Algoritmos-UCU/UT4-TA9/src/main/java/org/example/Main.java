package org.example;

public class Main
{
    public static void main(String[] args)
    {
        Geant.getInstance().agregarProductosDesdeArchivo("altasPrueba.txt");
        System.out.println();
        Geant.getInstance().imprimirProductos();
        System.out.println();
        Geant.getInstance().comprarDesdeArchivo("ventasPrueba.txt");
        System.out.println();
        Geant.getInstance().eliminarProductoDesdeArchivo("elimPrueba.txt");
        System.out.println();
        Geant.getInstance().imprimirProductos();
        System.out.println();
        Geant.getInstance().volcarDescripcionDeProductosConSuPrecioUnitarioEnUnArchivoDeTextoYPosteriormenteImprimirEnConsolaElValorMonetarioDelAlmacen();
        System.out.println();
        Geant.getInstance().existeStock(1000073);
        Geant.getInstance().existeStock(1000087);
        Geant.getInstance().existeStock(1000088);
        Geant.getInstance().existeStock(1000096);
        Geant.getInstance().existeStock(1000097);
        System.out.println();
        Geant.getInstance().existeProducto(1000073);
        Geant.getInstance().existeProducto(1000087);
        Geant.getInstance().existeProducto(1000088);
        Geant.getInstance().existeProducto(1000096);
        Geant.getInstance().existeProducto(1000097);
        System.out.println();
        Geant.getInstance().buscarProducto1("Gelatina");
        System.out.println();
        Geant.getInstance().buscarProducto1("Helado");

        // TODO: Search for desc.

        // cargar los productos desde el archivo "altasprueba.txt"
        // listar los productos ordenados por codigo, junto con su cantidad existente
        // emitir el valor del stock
        // simular las ventas a partir del archivo "ventaspruebas.txt"
        // simular la eliminación de productos a partir del archivo "elimPrueba.txt"
        // listar los productos ordenados por codigo, junto con su cantidad existente
    }
}
