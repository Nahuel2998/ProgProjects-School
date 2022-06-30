package org.example;

import asg.cliche.ShellFactory;
import java.io.IOException;
import java.util.Scanner;

public class Main
{
    public static void main(String[] args) throws IOException
    {
        System.out.println("Correr en modo interactivo? [y/N]");
        if (new Scanner(System.in).nextLine().toLowerCase().startsWith("y"))
        { ShellFactory.createConsoleShell("-", "Geant", Geant.getInstance()).commandLoop(); }
        else
        { modoEstatico(); }
    }

    public static void modoEstatico()
    {
        System.out.println("\nAgregar desde Archivo:");
        Geant.getInstance().agregarProductosDesdeArchivo("altas.txt");
        System.out.println("\nImprimir Productos:");
        Geant.getInstance().imprimirProductos();
        System.out.println("\nComprar desde Archivo:");
        Geant.getInstance().comprarDesdeArchivo("ventas.txt");
        System.out.println("\nEliminar desde Archivo:");
        Geant.getInstance().eliminarProductoDesdeArchivo("elim.txt");
        System.out.println("\nImprimir Productos:");
        Geant.getInstance().imprimirProductos();
        System.out.println("\nVolcar Descripcion de Productos con su Precio Unitario en un Archivo de Texto y Posteriormente Imprimir en Consola el Valor Monetario del Almacen:");
        Geant.getInstance().volcarDescripcionDeProductosConSuPrecioUnitarioEnUnArchivoDeTextoYPosteriormenteImprimirEnConsolaElValorMonetarioDelAlmacen();
        System.out.println("\nComprar el Stock Entero de Galletita Maria (para la comprobacion de Stock):");
        Geant.getInstance().comprarDesdeArchivo("ventasPrueba.txt");
        System.out.println("\nComprobar Existencia de Stock:");
        Geant.getInstance().existeStock(1000045);
        Geant.getInstance().existeStock(1010084);
        Geant.getInstance().existeStock(1000079);
        Geant.getInstance().existeStock(1000096);
        Geant.getInstance().existeStock(1000097);
        System.out.println("\nComprobar Existencia de Productos:");
        Geant.getInstance().existeProducto(1000045);
        Geant.getInstance().existeProducto(1010084);
        Geant.getInstance().existeProducto(1000088);
        Geant.getInstance().existeProducto(1000096);
        Geant.getInstance().existeProducto(1000097);
        System.out.println("\nBuscar Productos por Descripcion [Gelatina]:");
        Geant.getInstance().buscarProducto("Gelatina");
        System.out.println("\nBuscar Productos por Descripcion [Helado]:");
        Geant.getInstance().buscarProducto("Helado");
    }
}
