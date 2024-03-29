package org.util;

import org.jetbrains.annotations.NotNull;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

public class ManejadorArchivosGenerico
{

    /**
     * @param nombreCompletoArchivo
     * @param listaLineasArchivo lista con las lineas del archivo
     * @throws IOException
     */
    public static void escribirArchivo(String nombreCompletoArchivo,
            String @NotNull ... listaLineasArchivo)
    {
        FileWriter fw;
        try
        {
            fw = new FileWriter(nombreCompletoArchivo, true);
            BufferedWriter bw = new BufferedWriter(fw);
            for (String lineaActual : listaLineasArchivo) {
                bw.write(lineaActual);
                bw.newLine();
            }
            bw.close();
            fw.close();
        }
        catch (IOException e)
        {
            System.out.println("Error al escribir el archivo: "
                    + nombreCompletoArchivo);
//            e.printStackTrace();
        }
    }

    public static String @NotNull [] leerArchivo(String nombreCompletoArchivo)
    {
        FileReader fr;
        ArrayList<String> listaLineasArchivo = new ArrayList<>();
        try
        {
            fr = new FileReader(nombreCompletoArchivo);
            BufferedReader br = new BufferedReader(fr);
            String lineaActual = br.readLine();
            while (lineaActual != null)
            {
                listaLineasArchivo.add(lineaActual);
                lineaActual = br.readLine();
            }
            br.close();
            fr.close();
        } catch (IOException e)
        {
            System.out.println("Error al leer el archivo "
                    + nombreCompletoArchivo);
            e.printStackTrace();
        }
//        System.out.println("Archivo leido satisfactoriamente.");

        return listaLineasArchivo.toArray(new String[0]);
    }
}
