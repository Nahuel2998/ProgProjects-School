package org.example;

import junitx.framework.FileAssert;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.PrintStream;

import static org.junit.jupiter.api.Assertions.*;

class GeantTest
{
    private final ByteArrayOutputStream outContent = new ByteArrayOutputStream();
    private final PrintStream originalOut = System.out;

    @BeforeEach
    void setUp()
    {
        System.setOut(new PrintStream(outContent));
        Geant.getInstance().vaciarAlmacen();
        Geant.getInstance().agregarProductosDesdeArchivo("altasPrueba.txt");
    }

    @AfterEach
    void tearDown()
    { System.setOut(originalOut); }

    @Test
    void agregarProductosDesdeArchivo()
    { assertEquals("Monto total aumentado: 165399\n", outContent.toString()); }

    @Test
    void comprarDesdeArchivo()
    {
        outContent.reset();

        Geant.getInstance().comprarDesdeArchivo("ventasPrueba.txt");
        assertEquals("""
        No hay suficiente stock para comprar 550. Ignorando.
        No hay suficiente stock para comprar 150. Ignorando.
        Producto '1000031' no encontrado. Ignorando.
        Ganancias: 22400
        """, outContent.toString());
    }

    @Test
    void eliminarProductoDesdeArchivo()
    {
        outContent.reset();

        Geant.getInstance().eliminarProductoDesdeArchivo("elimPrueba.txt");
        assertEquals("""
        Producto '2000036' no encontrado. Ignorando.
        Monto de perdida: 58332
        """, outContent.toString());
    }

    @Test
    void volcarDescripcionDeProductosConSuPrecioUnitarioEnUnArchivoDeTextoYPosteriormenteImprimirEnConsolaElValorMonetarioDelAlmacen()
    {
        outContent.reset();

        Geant.getInstance().volcarDescripcionDeProductosConSuPrecioUnitarioEnUnArchivoDeTextoYPosteriormenteImprimirEnConsolaElValorMonetarioDelAlmacen();
        FileAssert.assertEquals(new File("productosEsperado.txt"), new File("productos.txt"));
        assertEquals("""
        productos.txt escrito.
        Valor total monetario: 165399
        """, outContent.toString());
    }

    @Test
    void existeStock()
    {
        // Comprar el Stock Entero de Galletita Maria (para la comprobacion de Stock):
        Geant.getInstance().comprar(1000097, 569);
        outContent.reset();

        // Cuando no existe el producto
        Geant.getInstance().existeStock(1000045);

        // Cuando no hay unidades restantes
        Geant.getInstance().existeStock(1000097);

        // Cuando hay stock
        Geant.getInstance().existeStock(1000096);

        assertEquals("""
        El producto '1000045' no existe.
        No hay unidades restantes.
        Hay stock. 280 unidades restantes.
        """, outContent.toString());
    }

    @Test
    void imprimirProductos()
    {
        outContent.reset();
        Geant.getInstance().imprimirProductos();
        assertEquals("""
        Id: 1000073
        Nombre: GALLETAS CEREALITAS CLASICAS
        Precio: 103
        Stock: 332
        - - - - - - -
        Id: 1000087
        Nombre: GELATINA DURAZNO ROYAL 8 PORCIONES
        Precio: 56
        Stock: 431
        - - - - - - -
        Id: 1000088
        Nombre: GELATINA FRUTILLA ROYAL 8 PORCIONES
        Precio: 150
        Stock: 15
        - - - - - - -
        Id: 1000096
        Nombre: HELADO DULCE DE LECHE RECREO
        Precio: 226
        Stock: 280
        - - - - - - -
        Id: 1000097
        Nombre: HELADO FRUTILLA RECREO
        Precio: 73
        Stock: 569
        """, outContent.toString());
    }

    @Test
    void existeProducto()
    {
        outContent.reset();

        // Cuando no existe
        Geant.getInstance().existeProducto(1000045);

        // Cuando existe
        Geant.getInstance().existeProducto(1000096);

        assertEquals("""
        No existe.
        Existe.
        """, outContent.toString());
    }

    @Test
    void buscarProducto()
    {
        outContent.reset();

        Geant.getInstance().buscarProducto("Royal");
        assertEquals("""
        Id: 1000087
        Nombre: GELATINA DURAZNO ROYAL 8 PORCIONES
        Precio: 56
        Stock: 431
        - - - - - - -
        Id: 1000088
        Nombre: GELATINA FRUTILLA ROYAL 8 PORCIONES
        Precio: 150
        Stock: 15
        """, outContent.toString());
    }
}