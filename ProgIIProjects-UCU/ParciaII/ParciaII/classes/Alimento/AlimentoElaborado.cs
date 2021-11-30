namespace ParciaII.classes.Alimento
{
    public class AlimentoElaborado : IAlimento
    {
        public string Nombre { get; set; }
        
        public AlimentoElaborado(string nombre)
        { Nombre = nombre; }
    }
}