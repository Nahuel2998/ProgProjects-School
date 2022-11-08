import java.util.LinkedList;

public class TVertice<C extends Comparable<C>, T> implements IVertice<C, T>
{
    private final C etiqueta;
    private final LinkedList<TAdyacencia<C, T>> adyacentes;
    private boolean visitado;
    private T datos;

    public C getEtiqueta()
    { return etiqueta; }

    @Override
    public LinkedList<TAdyacencia<C, T>> getAdyacentes()
    { return adyacentes; }

    public TVertice(C unaEtiqueta)
    {
        this.etiqueta = unaEtiqueta;
        adyacentes = new LinkedList<>();
        visitado = false;
    }

    public void setVisitado(boolean valor)
    { this.visitado = valor; }

    public boolean getVisitado()
    { return this.visitado; }

    @Override
    public TAdyacencia<C, T> buscarAdyacencia(TVertice<C, T> verticeDestino)
    {
        if (verticeDestino != null)
        { return buscarAdyacencia(verticeDestino.getEtiqueta()); }
        return null;
    }

    @Override
    public Double obtenerCostoAdyacencia(TVertice<C, T> verticeDestino)
    {
        TAdyacencia<C, T> ady = buscarAdyacencia(verticeDestino);
        if (ady != null)
        { return ady.getCosto(); }
        return Double.MAX_VALUE;
    }

    @Override
    public boolean insertarAdyacencia(Double costo, TVertice<C, T> verticeDestino)
    {
        if (buscarAdyacencia(verticeDestino) == null)
        {
            TAdyacencia<C, T> ady = new TAdyacencia<C, T>(costo, verticeDestino);
            return adyacentes.add(ady);
        }
        return false;
    }

    @Override
    public boolean eliminarAdyacencia(C nomVerticeDestino)
    {
        TAdyacencia<C, T> ady = buscarAdyacencia(nomVerticeDestino);
        if (ady != null)
        {
            adyacentes.remove(ady);
            return true;
        }
        return false;
    }

    @Override
    public TVertice<C, T> primerAdyacente()
    {
        if (this.adyacentes.getFirst() != null)
        { return this.adyacentes.getFirst().getDestino(); }
        return null;
    }

    @Override
    public TVertice<C, T> siguienteAdyacente(TVertice<C, T> w)
    {
        TAdyacencia<C, T> adyacente = buscarAdyacencia(w.getEtiqueta());
        int index = adyacentes.indexOf(adyacente);
        if (index + 1 < adyacentes.size())
        { return adyacentes.get(index + 1).getDestino(); }
        return null;
    }

    @Override
    public TAdyacencia<C, T> buscarAdyacencia(C etiquetaDestino)
    {
        for (TAdyacencia<C, T> adyacencia : adyacentes)
        {
            if (adyacencia.getDestino().getEtiqueta().compareTo(etiquetaDestino) == 0)
            { return adyacencia; }
        }
        return null;
    }

    public T getDatos()
    { return datos; }
}
