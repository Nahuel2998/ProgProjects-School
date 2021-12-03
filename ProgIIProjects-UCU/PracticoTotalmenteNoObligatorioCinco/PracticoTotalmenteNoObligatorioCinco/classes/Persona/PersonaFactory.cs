using static PracticoTotalmenteNoObligatorioCinco.classes.Constantes;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public class PersonaFactory
    {
        public static IPersona PersonaBuilder(ushort tipo, string nombre)
        {
            return tipo switch
            {
                CLIENTE => new Cliente(nombre),
                ADMINISTRADOR => new Administrador(nombre),
                DESARROLLADOR => new Desarrollador(nombre),
                _ => null
            };
        }
    }
}