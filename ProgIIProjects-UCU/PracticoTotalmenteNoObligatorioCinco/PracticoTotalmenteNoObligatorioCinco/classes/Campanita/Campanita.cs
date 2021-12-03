using System.Collections.Generic;
using System.Linq;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public sealed class Campanita
    {
        public Dictionary<string, Tarea> Tareas { get; } = new();

        private static readonly Campanita Instance = new();
        public readonly IList<IPersona> Subs = new List<IPersona>();

        private Campanita()
        { }

        public static Campanita GetInstance()
        { return Instance; }

        public void NotificarSubs(string tarea)
        {
            foreach (var sub in Subs)
            { sub.Update(Instance.Tareas[tarea]); }
        }

        private string[] GetTareasByEstado(short estado)
        {
            return Tareas
                .Where(x => x.Value.Estado == estado)
                .Select(x => x.Key)
                .ToArray();
        }

        public string[] GetTareasRechazadas() => GetTareasByEstado(-1);
        public string[] GetTareasPendientes() => GetTareasByEstado(0);
        public string[] GetTareasAprobadas() => GetTareasByEstado(1);
        public string[] GetTareasTerminadas() => GetTareasByEstado(2);
    }
}