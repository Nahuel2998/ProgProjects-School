namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public interface ITarea
    {
        string Nombre { get; }
        short Estado { get; }
        void Aprobar();
        void Rechazar();
        void Terminar();
    }
}