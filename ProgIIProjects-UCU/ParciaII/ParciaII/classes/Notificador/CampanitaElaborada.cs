using System.Collections.Generic;
using ParciaII.classes.Alimento;
using ParciaII.classes.Cliente;

namespace ParciaII.classes.Notificador
{
    public class CampanitaElaborada : ICampanita
    {
        public Stack<IAlimento> Alimentos { get; } = new();
        public IList<ICliente> Subs { get; } = new List<ICliente>();

        private static readonly CampanitaElaborada Instance = new();

        private CampanitaElaborada()
        { }

        public static CampanitaElaborada GetInstance()
        { return Instance; }
        
        public void NotificarSubs()
        {
            foreach (var cliente in Subs)
            { cliente.NewFoodJustDropped(); }
        }
    }
}