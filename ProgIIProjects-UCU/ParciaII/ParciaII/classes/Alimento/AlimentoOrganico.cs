namespace ParciaII.classes.Alimento
{
    public class AlimentoOrganico : IAlimento
    {
        public string Nombre { get; set; }
        
        public AlimentoOrganico(string nombre)
        { Nombre = nombre; }
    }
}