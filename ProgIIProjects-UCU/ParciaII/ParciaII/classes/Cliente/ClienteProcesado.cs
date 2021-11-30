using System;
using ParciaII.classes.Exception;
using ParciaII.classes.Notificador;

namespace ParciaII.classes.Cliente
{
    public class ClienteProcesado : ICliente
    {
        public string Nombre { get; set; }

        public void NewFoodJustDropped()
        { Console.WriteLine($"{Nombre} esta muy contente."); }

        public void Comprar()
        {
            try
            { CampanitaProcesada.GetInstance().Alimentos.Pop(); }
            catch (InvalidOperationException)
            { throw new NoHayComidaException("No hay mas procesados :("); }
        }
        
        public ClienteProcesado(string nombre)
        { Nombre = nombre; }
    }
}