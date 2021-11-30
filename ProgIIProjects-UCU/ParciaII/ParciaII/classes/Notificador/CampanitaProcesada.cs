using System.Collections.Generic;
using ParciaII.classes.Alimento;
using ParciaII.classes.Cliente;

namespace ParciaII.classes.Notificador
{
    public sealed class CampanitaProcesada : ICampanita
    {
        public Stack<IAlimento> Alimentos { get; } = new();
        public IList<ICliente> Subs { get; } = new List<ICliente>();

        private static readonly CampanitaProcesada Instance = new();

        private CampanitaProcesada()
        { }

        public static CampanitaProcesada GetInstance()
        { return Instance; }

        public void NotificarSubs()
        {
            foreach (var cliente in Subs)
            { cliente.NewFoodJustDropped(); }
        }
    }
}