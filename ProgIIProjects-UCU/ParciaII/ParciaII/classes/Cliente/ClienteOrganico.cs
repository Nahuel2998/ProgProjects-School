using System;
using ParciaII.classes.Exception;
using ParciaII.classes.Notificador;

namespace ParciaII.classes.Cliente
{
    public class ClienteOrganico : ICliente
    {
        public string Nombre { get; set; }

        public void NewFoodJustDropped()
        { Console.WriteLine($"{Nombre} esta muy contente."); }

        public void Comprar()
        {
            try
            { CampanitaOrganica.GetInstance().Alimentos.Pop(); }
            catch (InvalidOperationException)
            { throw new NoHayComidaException("No hay mas organicos :("); }
        }
        
        public ClienteOrganico(string nombre)
        { Nombre = nombre; }
    }
}