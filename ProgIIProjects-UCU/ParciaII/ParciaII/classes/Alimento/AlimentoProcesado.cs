namespace ParciaII.classes.Alimento
{
    public class AlimentoProcesado : IAlimento
    {
        public string Nombre { get; set; }
        
        public AlimentoProcesado(string nombre)
        { Nombre = nombre; }
    }
}