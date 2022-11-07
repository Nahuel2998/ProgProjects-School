public class PruebaGrafo
{
    public static void main(String[] args)
    {
        TGrafoDirigido gd = (TGrafoDirigido) UtilGrafos.cargarGrafo("./src/aeropuertos_1.txt","./src/conexiones_1.txt",
                false, TGrafoDirigido.class);

        Object[] etiquetasarray = gd.getEtiquetasOrdenado();

        Double[][] matriz = UtilGrafos.obtenerMatrizCostos(gd.getVertices());
        UtilGrafos.imprimirMatrizMejorado(matriz, gd.getVertices(), "Matriz");
        Double[][] mfloyd = gd.floyd();
        UtilGrafos.imprimirMatrizMejorado(mfloyd, gd.getVertices(), "Matriz luego de FLOYD");
        for (Object o : etiquetasarray)
        { System.out.println("excentricidad de " + o + " : " + gd.obtenerExcentricidad((Comparable) o)); }
        System.out.println();
        System.out.println("Centro del grafo: " + gd.centroDelGrafo());
    }
}
