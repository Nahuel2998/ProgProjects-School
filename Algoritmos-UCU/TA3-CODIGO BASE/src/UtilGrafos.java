import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.*;
import java.util.logging.Level;
import java.util.logging.Logger;

public class UtilGrafos
{
    public static <C extends Comparable<C>, T> Double[] @NotNull [] obtenerMatrizCostos(@NotNull Map<C, TVertice<C, T>> vertices)
    {
        int cantidadVertices = vertices.size();
        Double[][] matrizCostos = new Double[cantidadVertices][cantidadVertices];

        for (int i = 0; i < matrizCostos.length; i++)
        {
            for (int j = 0; j < matrizCostos.length; j++)
            {
                if (i == j)
                { matrizCostos[i][j] = 0.0; }
                else
                { matrizCostos[i][j] = Double.MAX_VALUE; }
            }
        }

        int i = 0;

        Set<C> etiquetasVertices = vertices.keySet();
        Object[] VerticesIArr = etiquetasVertices.toArray();
        Object[] VerticesJArr = etiquetasVertices.toArray();

        while (i < cantidadVertices)
        {
            int j = 0;
            while (j < cantidadVertices)
            {
                TVertice<C, T> elemVerticeI = vertices.get((C)VerticesIArr[i]);
                TVertice<C, T> elemVerticeJ = vertices.get((C)VerticesJArr[j]);

                if (!elemVerticeI.getEtiqueta().equals(elemVerticeJ.getEtiqueta()))
                {
                    Double costoAdyacencia = (elemVerticeI).obtenerCostoAdyacencia(elemVerticeJ);
                    matrizCostos[i][j] = costoAdyacencia;
                }
                j++;
            }
            i++;
        }
        return matrizCostos;
    }

    public static <C extends Comparable<C>, T> void imprimirMatriz(C[][] matriz, Map<C, TVertice<C, T>> vertices)
    {
        Object[] etiquetas = vertices.keySet().toArray();
        System.out.print("  ");
        for (int i = 0; i < matriz.length; i++)
        { System.out.print(etiquetas[i] + "  "); }
        System.out.println();
        for (int i = 0; i < matriz.length; i++)
        {
            System.out.print(etiquetas[i] + " ");
            for (int j = 0; j < matriz.length; j++)
            {
                if (((Double)matriz[i][j]).compareTo(Double.MAX_VALUE) == 0)
                { System.out.print(" INF "); }
                else
                { System.out.print(matriz[i][j] + " "); }
            }
            System.out.println();
        }
    }

    public static <C extends Comparable<C>, T> void imprimirMatrizCsv(C[][] matriz, Map<C, TVertice<C, T>> vertices)
    {
        Object[] etiquetas = vertices.keySet().toArray();
        System.out.print("Vertice/Vertice,");
        for (int i = 0; i < matriz.length; i++)
        {
            System.out.print(etiquetas[i]);
            if (i != matriz.length - 1)
            { System.out.print(","); }
        }
        System.out.println();
        for (int i = 0; i < matriz.length; i++)
        {
            System.out.print(etiquetas[i] + ", ");
            for (int j = 0; j < matriz.length; j++)
            {
                if (((Double)matriz[i][j]).compareTo(Double.MAX_VALUE) == 0)
                { System.out.print(" INF "); }
                else
                { System.out.print(matriz[i][j]); }
                if (j != matriz.length - 1)
                { System.out.print(","); }
            }
            System.out.println();
        }
    }

    public static <C extends Comparable<C>, T, C1 extends Comparable<C1>> void imprimirMatrizMejorado(C[][] matriz, Map<C1, TVertice<C1, T>> vertices, String titulo)
    {
        if (vertices != null && matriz.length == vertices.keySet().size())
        {
            Object[] etiquetas = Arrays.stream(vertices.keySet().toArray()).map(Object::toString).toArray();
            int etiquetaMasLarga = stringMasLargo(etiquetas);
            int datoMasLargo = 0;
            String infinito = "Inf";
            String nulo = "Nulo";
            int separacionEntreColumnas = 3;

            String[] datos = new String[matriz.length];

            for (C[] cs : matriz)
            {
                for (int j = 0; j < matriz.length; j++)
                {
                    if (cs[j] == null)
                    { datos[j] = nulo; }
                    else if (((Double)cs[j]).compareTo(Double.MAX_VALUE) == 0)
                    { datos[j] = infinito; }
                    else
                    { datos[j] = String.valueOf(cs[j]); }
                }
                if (stringMasLargo(datos) > datoMasLargo)
                { datoMasLargo = stringMasLargo(datos); }
            }

            int largo = Math.max(etiquetaMasLarga, datoMasLargo) + separacionEntreColumnas;

            for (int i = 0; i < etiquetas.length; i++)
            { etiquetas[i] = rellenar(etiquetas[i].toString(), largo, ' '); }

            int tope = (largo) * (etiquetas.length + 1);

            String linea = rellenar("", tope, '-');
            String separador = rellenar("", largo, ' ');
            String sepTitulo = rellenar("", tope, '*');

            System.out.println(sepTitulo);
            System.out.println(devolverCentrado(titulo, tope));
            System.out.println(sepTitulo);
            System.out.println(linea);

            System.out.print(separador);
            for (int i = 0; i < matriz.length; i++)
            { System.out.print(etiquetas[i]); }

            System.out.println();
            System.out.println(linea);

            for (int i = 0; i < matriz.length; i++)
            {
                System.out.print(etiquetas[i]);
                for (int j = 0; j < matriz.length; j++)
                {
                    if (matriz[i][j] == null)
                    { System.out.print(rellenar(nulo, largo, ' ')); }
                    else if (((Double)matriz[i][j]).compareTo(Double.MAX_VALUE) == 0)
                    { System.out.print(rellenar(infinito, largo, ' ')); }
                    else
                    { System.out.print(rellenar(matriz[i][j].toString(), largo, ' ')); }
                }
                System.out.println();
                System.out.println(linea);
            }
        }
        System.out.println();
    }

    public static @NotNull String rellenar(String textoARellenar, int largoTotal, char relleno)
    {
        StringBuilder textoARellenarBuilder = new StringBuilder(textoARellenar);
        while (textoARellenarBuilder.length() < largoTotal)
        { textoARellenarBuilder.append(relleno); }
        textoARellenar = textoARellenarBuilder.toString();
        return textoARellenar;
    }

    public static <C> int stringMasLargo(C[] etiquetas)
    {
        int mayor = etiquetas[0].toString().length();
        for (int i = 1; i < etiquetas.length; i++)
        {
            if (etiquetas[i].toString().length() > mayor)
            { mayor = etiquetas[i].toString().length(); }
        }
        return mayor;
    }

    public static @NotNull String devolverCentrado(String texto, int largo)
    {
        boolean pos = false;
        StringBuilder textoBuilder = new StringBuilder(texto);
        while (textoBuilder.length() < largo)
        {
            if (!pos)
            {
                textoBuilder.append(" ");
                pos = true;
            }
            else
            {
                textoBuilder.insert(0, " ");
                pos = false;
            }
        }
        texto = textoBuilder.toString();
        return texto;
    }

    public static <C extends Comparable<C>, T, G extends IGrafoDirigido<C, T>> @Nullable G cargarGrafo(String nomArchVert, String nomArchAdy, boolean ignoreHeader, Class<G> t)
    {
        String[] vertices = ManejadorArchivosGenerico.leerArchivo(nomArchVert, ignoreHeader);
        String[] aristas = ManejadorArchivosGenerico.leerArchivo(nomArchAdy, ignoreHeader);

        List<TVertice<C, T>> verticesList = new ArrayList<>(vertices.length);
        List<TArista<C>> aristasList = new ArrayList<>(aristas.length);

        for (String verticeString : vertices)
        {
            if ((verticeString != null) && (!verticeString.trim().equals("")))
            {
                verticeString = verticeString.split(",")[0];
                verticesList.add(new TVertice<>((C) verticeString));
            }
        }
        for (String aristaString : aristas)
        {
            if ((aristaString != null) && (!aristaString.trim().equals("")))
            {
                String[] datos = aristaString.split(",");
                aristasList.add(new TArista<>((C) datos[0], (C) datos[1], Double.parseDouble(datos[2])));
            }
        }
        try
        {
            t.getConstructor(Collection.class, Collection.class);
            return t.getConstructor(Collection.class, Collection.class).newInstance(verticesList, aristasList);
        }
        catch (Exception ex)
        { Logger.getLogger(UtilGrafos.class.getName()).log(Level.SEVERE, null, ex); }
        return null;
    }
}
