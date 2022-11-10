import org.jetbrains.annotations.NotNull;

public class TAdyacencia<C extends Comparable<C>, T> implements IAdyacencia<C, T>
{
    private final C etiqueta;
    private final double costo;
    private final TVertice<C, T> destino;

    @Override
    public C getEtiqueta()
    { return etiqueta; }

    @Override
    public double getCosto()
    { return costo; }

    @Override
    public TVertice<C, T> getDestino()
    { return destino; }

    public TAdyacencia(double costo, @NotNull TVertice<C, T> destino)
    {
        this.etiqueta = destino.getEtiqueta();
        this.costo = costo;
        this.destino = destino;
    }
}
