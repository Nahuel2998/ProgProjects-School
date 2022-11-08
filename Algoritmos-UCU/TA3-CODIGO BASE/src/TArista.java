
public class TArista<C extends Comparable<C>> implements IArista<C>
{
    protected C etiquetaOrigen;
    protected C etiquetaDestino;
    protected double costo;

    public TArista(C etiquetaOrigen, C etiquetaDestino, double costo)
    {
        this.etiquetaOrigen = etiquetaOrigen;
        this.etiquetaDestino = etiquetaDestino;
        this.costo = costo;
    }

    @Override
    public C getEtiquetaOrigen()
    { return etiquetaOrigen; }

    @Override
    public void setEtiquetaOrigen(C etiquetaOrigen)
    { this.etiquetaOrigen = etiquetaOrigen; }

    @Override
    public C getEtiquetaDestino()
    { return etiquetaDestino; }

    @Override
    public void setEtiquetaDestino(C etiquetaDestino)
    { this.etiquetaDestino = etiquetaDestino; }

    @Override
    public double getCosto()
    { return costo; }

    @Override
    public void setCosto(double costo)
    { this.costo = costo; }
}
