using static ParciaII.classes.Constantes;

namespace ParciaII.classes.Alimento
{
    public static class AlimentoFactory
    {
        public static IAlimento AlimentoBuilder(uint tipo, string nombre)
        {
            return tipo switch
            {
                Organico => new AlimentoOrganico(nombre),
                Procesado => new AlimentoProcesado(nombre),
                Elaborado => new AlimentoElaborado(nombre),
                _ => null
            };
        }
    }
}