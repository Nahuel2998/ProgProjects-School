import org.jetbrains.annotations.NotNull;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;

public class TGrafoDirigido<C extends Comparable<C>, T> implements IGrafoDirigido<C, T>
{
    private final Map<C, TVertice<C, T>> vertices; // vertices del grafo.-

    public TGrafoDirigido(@NotNull Collection<TVertice<C, T>> vertices, Collection<TArista<C>> aristas)
    {
        this.vertices = new HashMap<>();
        for (TVertice<C, T> vertice : vertices)
        { insertarVertice(vertice.getEtiqueta()); }
        for (TArista<C> arista : aristas)
        { insertarArista(arista); }
    }

    /**
     * Metodo encargado de eliminar una arista dada por un origen y destino. En
     * caso de no existir la adyacencia, retorna falso. En caso de que las
     * etiquetas sean invalidas, retorna falso.
     *
     */
    public boolean eliminarArista(C nomVerticeOrigen, C nomVerticeDestino)
    {
        if ((nomVerticeOrigen != null) && (nomVerticeDestino != null))
        {
            TVertice<C, T> vertOrigen = buscarVertice(nomVerticeOrigen);
            if (vertOrigen != null)
            { return vertOrigen.eliminarAdyacencia(nomVerticeDestino); }
        }
        return false;
    }

    /**
     * Metodo encargado de verificar la existencia de una arista. Las etiquetas
     * pasadas por par�metro deben ser v�lidas.
     *
     * @return True si existe la adyacencia, false en caso contrario
     */
    public boolean existeArista(C etiquetaOrigen, C etiquetaDestino)
    {
        TVertice<C, T> vertOrigen = buscarVertice(etiquetaOrigen);
        TVertice<C, T> vertDestino = buscarVertice(etiquetaDestino);
        if ((vertOrigen != null) && (vertDestino != null))
        { return vertOrigen.buscarAdyacencia(vertDestino) != null; }
        return false;
    }

    /**
     * Metodo encargado de verificar la existencia de un vertice dentro del
     * grafo.-
     * <p>
     * La etiqueta especificada como par�metro debe ser v�lida.
     *
     * @param unaEtiqueta Etiqueta del vertice a buscar.-
     * @return True si existe el vertice con la etiqueta indicada, false en caso
     * contrario
     */
    public boolean existeVertice(C unaEtiqueta)
    {
        return getVertices().get(unaEtiqueta) != null;
    }

    /**
     * Metodo encargado de verificar buscar un vertice dentro del grafo.-
     * <p>
     * La etiqueta especificada como parametro debe ser valida.
     *
     * @param unaEtiqueta Etiqueta del vertice a buscar.-
     * @return El vertice encontrado. En caso de no existir, retorna nulo.
     */
    private TVertice<C, T> buscarVertice(C unaEtiqueta)
    {
        return getVertices().get(unaEtiqueta);
    }

    /**
     * Metodo encargado de insertar una arista en el grafo (con un cierto
     * costo), dado su vertice origen y destino.- Para que la arista sea valida,
     * se deben cumplir los siguientes casos: 1) Las etiquetas pasadas por
     * parametros son v�lidas.- 2) Los vertices (origen y destino) existen
     * dentro del grafo.- 3) No es posible ingresar una arista ya existente
     * (miso origen y mismo destino, aunque el costo sea diferente).- 4) El
     * costo debe ser mayor que 0.
     *
     * @return True si se pudo insertar la adyacencia, false en caso contrario
     */
    public boolean insertarArista(@NotNull TArista<C> arista)
    {
        if ((arista.getEtiquetaOrigen() != null) && (arista.getEtiquetaDestino() != null))
        {
            TVertice<C, T> vertOrigen = buscarVertice(arista.getEtiquetaOrigen());
            TVertice<C, T> vertDestino = buscarVertice(arista.getEtiquetaDestino());
            if ((vertOrigen != null) && (vertDestino != null))
            { return vertOrigen.insertarAdyacencia(arista.getCosto(), vertDestino); }
        }
        return false;
    }

    /**
     * Metodo encargado de insertar un vertice en el grafo.
     * <p>
     * No pueden ingresarse vertices con la misma etiqueta. La etiqueta
     * especificada como par�metro debe ser v�lida.
     *
     * @param unaEtiqueta Etiqueta del vertice a ingresar.
     * @return True si se pudo insertar el vertice, false en caso contrario
     */
    public boolean insertarVertice(C unaEtiqueta)
    {
        if ((unaEtiqueta != null) && (!existeVertice(unaEtiqueta)))
        {
            TVertice<C, T> vert = new TVertice<>(unaEtiqueta);
            getVertices().put(unaEtiqueta, vert);
            return getVertices().containsKey(unaEtiqueta);
        }
        return false;
    }

    @Override
    public boolean insertarVertice(TVertice<C, T> vertice)
    {
        C unaEtiqueta = vertice.getEtiqueta();
        if ((unaEtiqueta != null) && (!existeVertice(unaEtiqueta)))
        {
            getVertices().put(unaEtiqueta, vertice);
            return getVertices().containsKey(unaEtiqueta);
        }
        return false;
    }

    public Object[] getEtiquetasOrdenado()
    {
        TreeMap<C, TVertice<C, T>> mapOrdenado = new TreeMap<>(this.getVertices());
        return mapOrdenado.keySet().toArray();
    }

    /**
     * @return the vertices
     */
    public Map<C, TVertice<C, T>> getVertices()
    {
        return vertices;
    }

    @Override
    public C centroDelGrafo()
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public Double[][] floyd()
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public C obtenerExcentricidad(C etiquetaVertice)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public boolean[][] warshall()
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public boolean eliminarVertice(C nombreVertice)
    {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
}
