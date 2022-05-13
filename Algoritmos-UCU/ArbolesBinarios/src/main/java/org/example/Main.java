package org.example;
import org.classes.ElementoAB;
import org.util.*;

import java.util.Arrays;

public class Main
{
    public static void main(String[] args)
    {
        System.out.println(Arrays.toString(ManejadorArchivosGenerico.leerArchivo("target/files/claves1.txt")));
        String[] claves = ManejadorArchivosGenerico.leerArchivo("target/files/claves1.txt");

        ElementoAB<Object> arbol = new ElementoAB<>(Integer.parseInt(claves[0]));

        for (int i = 1; i < claves.length; i++)
        { arbol.insertar(new ElementoAB<>(Integer.parseInt(claves[i]))); }

        System.out.println(arbol.inOrden());
    }
}