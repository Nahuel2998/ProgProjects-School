using static ParciaII.classes.Constantes;

namespace ParciaII.classes.Cliente
{
    public static class ClienteFactory
    {
        public static ICliente ClienteBuilder(uint tipo, string nombre)
        {
            return tipo switch
            {
                Organico => new ClienteOrganico(nombre),
                Procesado => new ClienteProcesado(nombre),
                Elaborado => new ClienteElaborado(nombre),
                _ => null
            };
        }
    }
}