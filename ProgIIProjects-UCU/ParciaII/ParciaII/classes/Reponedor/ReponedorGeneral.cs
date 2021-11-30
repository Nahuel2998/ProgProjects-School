using ParciaII.classes.Notificador;
using static ParciaII.classes.Constantes;
using static ParciaII.classes.Alimento.AlimentoFactory;

namespace ParciaII.classes.Reponedor
{
    public class ReponedorGeneral : IReponedorOrganico, IReponedorProcesado, IReponedorElaborado
    {
        public void ReponerOrganico()
        {
            CampanitaOrganica.GetInstance().Alimentos.Push(AlimentoBuilder(Organico, "Placeholder"));
            CampanitaOrganica.GetInstance().NotificarSubs();
        }

        public void ReponerProcesado()
        {
            CampanitaProcesada.GetInstance().Alimentos.Push(AlimentoBuilder(Procesado, "Placeholder"));
            CampanitaProcesada.GetInstance().NotificarSubs();
        }
        
        public void ReponerElaborado()
        {
            CampanitaElaborada.GetInstance().Alimentos.Push(AlimentoBuilder(Elaborado, "Placeholder"));
            CampanitaElaborada.GetInstance().NotificarSubs();
        }
    }
}