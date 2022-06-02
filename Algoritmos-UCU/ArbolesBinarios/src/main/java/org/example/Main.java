package org.example;
import org.classes.ElementoAB;
import org.util.*;

import java.util.Arrays;

public class Main
{
    public static void main(String[] args)
    {
//        System.out.println(Arrays.toString(ManejadorArchivosGenerico.leerArchivo("target/files/claves1.txt")));
//        String[] claves = ManejadorArchivosGenerico.leerArchivo("target/files/claves1.txt");

//        ElementoAB<Integer, Object> arbol = new ElementoAB<>(Integer.parseInt(claves[0]));

//        for (int i = 1; i < claves.length; i++)
//        { arbol.insertar(new ElementoAB<>(Integer.parseInt(claves[i]))); }

//        System.out.println(arbol.inOrden());
//        System.out.println(arbol.preOrden());

        ElementoAB<Integer, Object> arbol = new ElementoAB<>(8);

        for (int i : new int[]{5, 2, 1, 20, 15, 16, 17, 7})
        { arbol.insertar(new ElementoAB<>(i)); }

        System.out.println(arbol.inOrden());
        System.out.println(arbol.preOrden());

        arbol.eliminar(15);
        arbol.eliminar(7);

        System.out.println(arbol.inOrden());
        System.out.println(arbol.preOrden());
    }
}