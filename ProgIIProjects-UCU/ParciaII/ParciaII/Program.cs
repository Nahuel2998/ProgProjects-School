using ParciaII.classes.Cliente;
using ParciaII.classes.Notificador;
using ParciaII.classes.Reponedor;
using static ParciaII.classes.Constantes;
using static ParciaII.classes.Cliente.ClienteFactory;

namespace ParciaII
{
    class Program
    {
        static void Main(string[] args)
        {
            var organico = (ClienteOrganico) ClienteBuilder(Organico, "Ori");
            var procesado = (ClienteProcesado) ClienteBuilder(Procesado, "Procilandro");
            // Para aniadir elaborado, solo cree las nuevas clases y modifique las factories
            var elaborado = (ClienteElaborado) ClienteBuilder(Elaborado, "Elaborrero");
            
            CampanitaOrganica.GetInstance().Subs.Add(organico);
            CampanitaProcesada.GetInstance().Subs.Add(procesado);
            CampanitaElaborada.GetInstance().Subs.Add(elaborado);

            ReponedorGeneral reponedor = new();
            reponedor.ReponerOrganico();
            reponedor.ReponerProcesado();
            
            organico.Comprar();
            organico.Comprar(); // Dara una exception.
        }
    }
}