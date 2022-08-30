package org.example;

import org.example.inutil.GeneradorDatosGenericos;
import org.example.inutil.TClasificador;

import static org.example.inutil.TClasificador.METODO_CLASIFICACION_INSERCION;

public class Main
{
    public static void main(String args[])
    {
        TClasificador clasif = new TClasificador();
        GeneradorDatosGenericos gdg = new GeneradorDatosGenericos();
        int[] vectorAleatorio = gdg.generarDatosAleatorios();
        int[] vectorAscendente = gdg.generarDatosAscendentes();
        int[] vectorDescendente = gdg.generarDatosDescendentes();

        int[] resAleatorio = clasif.clasificar(vectorAleatorio, METODO_CLASIFICACION_INSERCION);
        for (int i = 0; i < resAleatorio.length; i++)
        { System.out.print(resAleatorio[i] + " "); }

        int[] resAscendente = clasif.clasificar(vectorAscendente, METODO_CLASIFICACION_INSERCION);
        for (int i = 0; i < resAscendente.length; i++)
        { System.out.print(resAscendente[i] + " "); }

        int[] resDescendente = clasif.clasificar(vectorDescendente, METODO_CLASIFICACION_INSERCION);
        for (int i = 0; i < resDescendente.length; i++)
        { System.out.print(resDescendente[i] + " "); }
    }
}