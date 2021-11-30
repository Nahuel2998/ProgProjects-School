using System.Collections.Generic;
using ParciaII.classes.Alimento;
using ParciaII.classes.Cliente;

namespace ParciaII.classes.Notificador
{
    public sealed class CampanitaOrganica : ICampanita
    {
        public Stack<IAlimento> Alimentos { get; } = new();
        public IList<ICliente> Subs { get; } = new List<ICliente>();

        private static readonly CampanitaOrganica Instance = new();

        private CampanitaOrganica()
        { }

        public static CampanitaOrganica GetInstance()
        { return Instance; }
        
        public void NotificarSubs()
        {
            foreach (var cliente in Subs)
            { cliente.NewFoodJustDropped(); }
        }
    }
}