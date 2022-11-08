public class PruebaGrafo
{
    public static void main(String[] args)
    {
        TGrafoDirigido<Double, String> gd = UtilGrafos.cargarGrafo("./src/aeropuertos_1.txt","./src/conexiones_1.txt",
                false, TGrafoDirigido.class);

        assert gd != null;
        Object[] etiquetasarray = gd.getEtiquetasOrdenado();

        Double[][] matriz = UtilGrafos.obtenerMatrizCostos(gd.getVertices());
        UtilGrafos.imprimirMatrizMejorado(matriz, gd.getVertices(), "Matriz");
        Double[][] mfloyd = gd.floyd();
        UtilGrafos.imprimirMatrizMejorado(mfloyd, gd.getVertices(), "Matriz luego de FLOYD");
        for (Object o : etiquetasarray)
        { System.out.println("excentricidad de " + o + " : " + gd.obtenerExcentricidad((Double) o)); }
        System.out.println();
        System.out.println("Centro del grafo: " + gd.centroDelGrafo());
    }
}
