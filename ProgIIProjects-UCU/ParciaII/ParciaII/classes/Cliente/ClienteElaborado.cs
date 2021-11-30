using System;
using ParciaII.classes.Exception;
using ParciaII.classes.Notificador;

namespace ParciaII.classes.Cliente
{
    public class ClienteElaborado : ICliente
    {
        public string Nombre { get; set; }

        public void NewFoodJustDropped()
        { Console.WriteLine($"{Nombre} esta muy contente."); }

        public void Comprar()
        {
            try
            { CampanitaElaborada.GetInstance().Alimentos.Pop(); }
            catch (InvalidOperationException)
            { throw new NoHayComidaException("No hay mas elaborados :("); }
        }
        
        public ClienteElaborado(string nombre)
        { Nombre = nombre; }
    }
}