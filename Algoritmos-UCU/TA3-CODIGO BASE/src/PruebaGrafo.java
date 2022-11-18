import java.util.HashMap;
import java.util.Scanner;

public class PruebaGrafo
{
    public static void main(String[] args)
    {
        TGrafoDirigido<String, Double> gd = UtilGrafos.cargarGrafo("./src/aeropuertos_1.txt","./src/conexiones_1.txt",
                false, TGrafoDirigido.class);

        assert gd != null;
        String[] etiquetasarray = new String[gd.getVertices().size()];
        etiquetasarray = gd.getEtiquetasOrdenado(etiquetasarray);

        int i = 0;
        // Lo optimo seria usar un hashmap en vez de Double[][] para tanto Floyd como Warshall para evadir hacer esto
        // Pero no quise cambiar lo que pedia la signature de la funcion
        HashMap<String, Integer> etiquetaId = new HashMap<>();
        for (String s : gd.getVertices().keySet())
        { etiquetaId.put(s, i++); }

        Double[][] matriz = UtilGrafos.obtenerMatrizCostos(gd.getVertices());
        UtilGrafos.imprimirMatrizMejorado(matriz, gd.getVertices(), "Matriz");

        Integer[][] caminos = new Integer[gd.getVertices().size()][gd.getVertices().size()];

        Double[][] mfloyd = gd.floyd(caminos);
        UtilGrafos.imprimirMatrizMejorado(mfloyd, gd.getVertices(), "Matriz luego de FLOYD");

        boolean[][] warshall = gd.warshall();

        for (String o : etiquetasarray)
        { System.out.printf("Excentricidad de %s : %.1f%n", o, gd.obtenerExcentricidad(o, mfloyd)); }

        System.out.println();
        System.out.println("Centro del grafo: " + gd.centroDelGrafo());

        Scanner s = new Scanner(System.in);

        // Ya que la letra decia "contestar interactivamente"
        while (true)
        {
            System.out.print("> ");
            String[] input = s.nextLine().split(" +");

            try
            {
                switch (input[0].toLowerCase())
                {
                    case "exit":
                    case ":q":
                    case "salir":
                    case "nosvem":
                        return;
                    case "dist":
                    case "distancia":
                        System.out.printf("%s -[%.1f]> %s%n",
                                input[1],
                                mfloyd[ etiquetaId.get(input[1]) ]
                                      [ etiquetaId.get(input[2]) ],
                                input[2]);
                        break;
                    case "posible":
                    case "es_posible":
                        System.out.printf("%s %s %s%n",
                                input[1],
                                warshall[ etiquetaId.get(input[1]) ]
                                        [ etiquetaId.get(input[2]) ]
                                        ? "->"
                                        : "!>",
                                input[2]);
                        break;
                    case "camino":
                    case "camino_mas_corto":
                        System.out.println(gd.caminoString(input[1], input[2], caminos));
                        break;
                }
            }
            catch (ArrayIndexOutOfBoundsException e)
            { System.out.printf("Numero incorrecto de argumentos para %s%n", input[0]); }
            catch (NullPointerException e)
            { System.out.println("Error en el nombre (al menos una) de las ciudades."); }
        }
    }
}
