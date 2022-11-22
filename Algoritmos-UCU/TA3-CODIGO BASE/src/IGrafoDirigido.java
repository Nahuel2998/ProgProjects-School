import java.util.Map;

public interface IGrafoDirigido<C extends Comparable<C>, T>
{
    /**
     * @return Etiqueta del centro del grafo
     */
    C centroDelGrafo();

    /**
     * Metodo encargado de eliminar una arista dada por un origen y destino.
     * En caso de no existir la arista, retorna falso. En caso de que las
     * etiquetas sean invalidas (no existe el vertice origen o el destino), retorna falso.
     * @param nomVerticeOrigen
     * @param nomVerticeDestino
     * @return
     */
    boolean eliminarArista(C nomVerticeOrigen, C nomVerticeDestino);

    /**
     * Metodo encargado de verificar la existencia de una arista. Las
     * etiquetas pasadas por parametro deben ser validas (o sea, los vértices origen y destino deben existir en el grafo).
     *
     * @return True si existe la arista, false en caso contrario
     */
    boolean existeArista(C etiquetaOrigen, C etiquetaDestino);

    /**
     * Metodo encargado de verificar la existencia de un vertice dentro del
     * grafo.-
     * <p>
     * La etiqueta especificada como parametro debe ser valida.
     *
     * @param unaEtiqueta Etiqueta del vertice a buscar.-
     * @return True si existe el vertice con la etiqueta indicada, false en caso
     * contrario
     */
    boolean existeVertice(C unaEtiqueta);

    /**
     *ejecuta el algoritmo de Floyd en el grafo, para hallar los caminos mínimos entre todos los pares de vértices. 
     * @return una matriz de n x n (n = cantidad de vértices del grafo) con los costos de los caminos mínimos.
     */
    Double [][] floyd();

    /**
     * Metododouble encargado de insertar una arista en el grafo (con un cierto
     * costo), dado su vertice origen y destino.- Para que la arista sea
     * valida, se deben cumplir los siguientes casos: 1) Las etiquetas pasadas
     * por parametros son validas.- 2) Los vertices (origen y destino) existen
     * dentro del grafo.- 3) No es posible ingresar una arista ya existente
     * (miso origen y mismo destino, aunque el costo sea diferente).- 4) El
     * costo debe ser mayor que 0.
     *
     * @return True si se pudo insertar la arista, false en caso contrario
     */
    boolean insertarArista(TArista<C> arista);

    /**
     * Metodo encargado de insertar un vertice en el grafo.
     * <p>
     * No pueden ingresarse vertices con la misma etiqueta. La etiqueta
     * especificada como parametro debe ser valida.
     *
     * @return True si se pudo insertar el vertice, false en caso contrario
     */
    boolean insertarVertice(TVertice<C, T> vertice);

    Double obtenerExcentricidad(C etiquetaVertice);

    /**
     *ejecuta el algoritmo de Warshall para halla la cerradura transitiva del grafo. 
     * @return una matriz de n x n (n = cantidad de vértices del grafo) en la que sus celdas indican si hay (TRUE) o no (FALSE) conectividad entre cada par de vértices.
     */
    boolean[][] warshall();

    Map<C, TVertice<C, T>> getVertices();
}
