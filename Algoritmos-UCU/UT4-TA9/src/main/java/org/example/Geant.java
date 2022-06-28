package org.example;

import asg.cliche.Command;
import org.classes.*;
import org.jetbrains.annotations.NotNull;
import org.util.ManejadorArchivosGenerico;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintStream;
import java.util.AbstractMap;
import java.util.Arrays;
import java.util.Map;

public final class Geant
{
    private static final Geant instancia = new Geant();
    private final Almacen<Integer> almacen;

    private Geant()
    { this.almacen = new Almacen<>("GeantMain"); }

    public static Geant getInstance()
    { return instancia; }

//    public boolean comprar(int codigoProducto, int cantidad)
//    {
//        try
//        {
//             Si no hay suficiente stock
//            if (this.almacen.restarStock(codigoProducto, cantidad) == -1)
//            {
//                System.out.println("No hay suficiente stock.");
//                return false;
//            }
//
//             Si no hubo problemas al comprar
//            System.out.println("Comprado correctamente.");
//            return true;
//        }
//        catch (NameNotFoundException e)
//        {
//             Si el producto no existe
//            System.out.println(e.getMessage());
//            return false;
//        }
//    }

    public int comprar(int codigoProducto, int cantidad) throws IllegalArgumentException
    {
        Producto<Integer> producto = this.almacen.buscarPorCodigo(codigoProducto);

        // Si el producto no existe
        if (producto == null)
        { throw new IllegalArgumentException(String.format("Producto '%d' no encontrado.", codigoProducto)); }

        // Si no hay suficiente stock
        if (producto.restarStock(cantidad) == -1)
        { throw new IllegalArgumentException(String.format("No hay suficiente stock para comprar %d.", cantidad)); }

        // Si no hubo problemas al comprar
        return producto.getPrecio() * cantidad;
    }

//    @SafeVarargs
//    public final int comprar(Map.Entry<Integer, Integer> @NotNull ... ventas)
//    {
//        int valorInicial = this.almacen.getValorAgregado();
//
//        for (Map.Entry<Integer, Integer> venta : ventas)
//        { this.comprar(venta.getKey(), venta.getValue()); }
//
//        return this.almacen.getValorAgregado() - valorInicial;
//    }

    public int comprar(boolean ignorarErrores, @NotNull ILista<Integer, Integer> ventas) throws IllegalArgumentException
    {
        int ganancias = 0;

        INodo<Integer, Integer> venta = ventas.getPrimero();

        while (venta != null)
        {
            try
            { ganancias += this.comprar(venta.getEtiqueta(), venta.getDato()); }
            catch (Exception e)
            {
                if (!ignorarErrores)
                { throw e; }

                System.out.println(e.getMessage() + " Ignorando.");
            }
            venta = venta.getSiguiente();
        }

        return ganancias;
    }

    public int agregarProducto(Producto<Integer> producto) throws IllegalArgumentException
    {
        this.almacen.insertarProducto(producto.getEtiqueta(), producto);
        return producto.getValorAgregado();
    }

    public int agregarProducto(String[] @NotNull ... datosProductos)
    {
        int valorAgregado = this.almacen.getValorAgregado();

        for (String[] datosProducto : datosProductos)
        {
            int idProducto = Integer.parseInt(datosProducto[0]);
            int stockProducto = Integer.parseInt(datosProducto[3]);

            Producto<Integer> producto = this.almacen.buscarPorCodigo(idProducto);

            if (producto != null)
            {
                producto.agregarStock(stockProducto);
                valorAgregado += producto.getPrecio() * stockProducto;
            }
            else { this.almacen.insertarProducto(idProducto, new Producto<>(
                    idProducto,
                    datosProducto[1],
                    Integer.parseInt(datosProducto[2]),
                    stockProducto)); }
        }

        return valorAgregado;
    }

    public int eliminarProducto(boolean ignorarNoEncontrados, int @NotNull ... codigoProducto) throws IllegalArgumentException
    {
        int valorAgregadoPrevio = this.almacen.getValorAgregado();

        for (Integer producto : codigoProducto)
        {
            if (!this.almacen.eliminarProducto(producto))
            {
                if (!ignorarNoEncontrados)
                { throw new IllegalArgumentException(String.format("Producto '%d' no encontrado.", producto)); }

                System.out.printf("Producto '%d' no encontrado. Ignorando.%n", producto);
            }
        }

        return valorAgregadoPrevio - this.almacen.getValorAgregado();
    }

    @Command
    public void agregarProductosDesdeArchivo(String nombreDeArchivo)
    {
        System.out.println("Monto total aumentado: " + agregarProducto(Arrays.stream(ManejadorArchivosGenerico.leerArchivo(nombreDeArchivo))
                .map(s -> s.split(","))
                .toArray(String[][]::new)));
    }

    @Command
    public void comprarDesdeArchivo(String nombreDeArchivo)
    {
        String[] datos = ManejadorArchivosGenerico.leerArchivo(nombreDeArchivo);
        ILista<Integer, Integer> listaProductosCantidades = new Lista<>();

        for (String linea : datos)
        {
            String[] productoCantidad = linea.split(",");

            listaProductosCantidades.insertar(new Nodo<>(Integer.parseInt(productoCantidad[0]), Integer.parseInt(productoCantidad[1])));
        }

        System.out.println("Ganancias: " + comprar(true, listaProductosCantidades));
    }

    @Command
    public void eliminarProductoDesdeArchivo(String nombreDeArchivo)
    { System.out.printf("Monto de perdida: %d%n", eliminarProducto(true, Arrays.stream(ManejadorArchivosGenerico.leerArchivo(nombreDeArchivo)).mapToInt(Integer::parseInt).toArray())); }

    @Command
    public void volcarDescripcionDeProductosConSuPrecioUnitarioEnUnArchivoDeTextoYPosteriormenteImprimirEnConsolaElValorMonetarioDelAlmacen()
    {
        try (PrintStream escribidor = new PrintStream("productos.txt"))
        {
            INodo<String, Integer> aux = this.almacen.obtenerListaDeProductosOrdenadosPorDescripcion().getPrimero();

            while (aux != null)
            {
                escribidor.printf("%s,%d%n", aux.getEtiqueta(), aux.getDato());
                aux = aux.getSiguiente();
            }

            escribidor.flush();
            System.out.println("productos.txt escrito.");
        }
        catch (IOException e)
        { System.out.println("No pude escribir :("); }

        System.out.println("Valor total monetario: " + almacen.getValorAgregado());
    }

    @Command
    public void existeStock(Integer codigoProducto)
    {
        Producto<Integer> producto = this.almacen.buscarPorCodigo(codigoProducto);
        System.out.println
                ((producto == null) ?
                        String.format("El producto '%d' no existe.", codigoProducto) :
                        ((producto.getStock() > 0) ?
                                String.format("Hay stock. %d unidades restantes.", producto.getStock()) :
                                "No hay unidades restantes."));
    }

    @Command
    public void imprimirProductos()
    { System.out.println(this.almacen.imprimirProductos()); }

    @Command
    public void existeProducto(int codigo)
    { System.out.println((this.almacen.existeProducto(codigo) ? "E" : "No e") + "xiste."); }

    @Command
    public void buscarProducto(String busqueda)
    { System.out.println(this.almacen.obtenerDescripcionesSimilares(busqueda).imprimir(Nodo.DATO, Almacen.SEPARADOR_ELEMENTOS_IMPRESOS)); }

    @Command
    public void buscarProducto1(String busqueda)
    { System.out.println(this.almacen.obtenerDescripcionesSimilares1(busqueda)); }
}
