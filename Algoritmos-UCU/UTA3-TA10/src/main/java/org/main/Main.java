package org.main;

import org.classes.*;
import org.util.ManejadorArchivosGenerico;

import asg.cliche.ShellFactory;

import java.io.IOException;

public class Main
{
    public static void cargarDatos()
    {
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
    }

    public static void main(String[] args) throws IOException
    {
        cargarDatos();
        ShellFactory.createConsoleShell("-", """
                        [Farmachop]
                        - - - - - - - - - - - - - - - - - - - -
                        ?l(ist)           : Lista de comandos.
                        ?h(elp) <comando> : Ayuda de comando.
                        - - - - - - - - - - - - - - - - - - - -""", Farmachop.getInstance()).commandLoop();
    }
}
