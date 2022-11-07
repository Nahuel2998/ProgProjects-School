
public class TArista implements IArista
{
    protected Comparable etiquetaOrigen;
    protected Comparable etiquetaDestino;
    protected double costo;

    public TArista(Comparable etiquetaOrigen, Comparable etiquetaDestino, double costo)
    {
        this.etiquetaOrigen = etiquetaOrigen;
        this.etiquetaDestino = etiquetaDestino;
        this.costo = costo;
    }

    @Override
    public Comparable getEtiquetaOrigen()
    { return etiquetaOrigen; }

    @Override
    public void setEtiquetaOrigen(Comparable etiquetaOrigen)
    { this.etiquetaOrigen = etiquetaOrigen; }

    @Override
    public Comparable getEtiquetaDestino()
    { return etiquetaDestino; }

    @Override
    public void setEtiquetaDestino(Comparable etiquetaDestino)
    { this.etiquetaDestino = etiquetaDestino; }

    @Override
    public double getCosto()
    { return costo; }

    @Override
    public void setCosto(double costo)
    { this.costo = costo; }
}
