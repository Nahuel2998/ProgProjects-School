package org.main;

import org.classes.*;
import org.util.ManejadorArchivosGenerico;

import asg.cliche.Command;
import asg.cliche.ShellFactory;

import java.io.IOException;

public class Main
{
    public static void main(String[] args) throws IOException
    {
//        int cantBuckets = 16;

//        ListaMejorada<String> sueros = new ListaMejorada<>(cantBuckets);
//        ListaMejorada<String> farmacos = new ListaMejorada<>(cantBuckets * 2^4);
//        ListaMejorada<Integer> lista = new ListaMejorada<>(cantBuckets * 2^3);

        // <editor-fold desc="Cargar Datos">
        String[] suerosData = ManejadorArchivosGenerico.leerArchivo("target/files/sueros.txt");
        String[] farmacosData = ManejadorArchivosGenerico.leerArchivo("target/files/farmacos.txt");
        String[] listaNegraData = ManejadorArchivosGenerico.leerArchivo("target/files/listanegra.txt");
        String[] listaBlancaData = ManejadorArchivosGenerico.leerArchivo("target/files/listablanca.txt");

        for (String s : suerosData)
        {
            String[] nodoData = s.split(",");
            Farmachop.getInstance().sueros.insertar(new Nodo<>(Integer.parseInt(nodoData[0]), nodoData[1]));
        }

        for (String s : farmacosData)
        {
            String[] nodoData = s.split(",");
            Farmachop.getInstance().farmacos.insertar(new Nodo<>(Integer.parseInt(nodoData[0]), nodoData[1]));
        }

        for (String s : listaBlancaData)
        {
            String[] nodoData = s.split(",");
            Farmachop.getInstance().lista.insertar(new Nodo<>(Integer.parseInt(nodoData[0]), -1));
        }

        for (String s : listaNegraData)
        {
            String[] nodoData = s.split(",");
            Farmachop.getInstance().lista.insertar(new Nodo<>(Integer.parseInt(nodoData[1]), Integer.parseInt(nodoData[0])));
        }
        // </editor-fold>

        System.out.println(Farmachop.getInstance().sueros.buscar(17).getDato());

        System.out.println(Farmachop.getInstance().preparadoViable(13, new Integer[]{1, 4})); // false
//        System.out.println(Farmachop.getInstance().preparadoViable(13, new Integer[]{1, 3}));
        System.out.println(Farmachop.getInstance().preparadoViable(13, new Integer[]{4, 6})); // true

        ShellFactory.createConsoleShell("-", "", Farmachop.getInstance()).commandLoop();
    }
}
