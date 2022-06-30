package org.example;

import asg.cliche.ShellFactory;
import java.io.IOException;

public class Main
{
    public static void main(String[] args) throws IOException
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
        Geant.getInstance().buscarProducto("Gelatina");
        System.out.println();
        Geant.getInstance().buscarProducto("Helado");

//        ShellFactory.createConsoleShell("-", "Geant", Geant.getInstance()).commandLoop();
    }
}
