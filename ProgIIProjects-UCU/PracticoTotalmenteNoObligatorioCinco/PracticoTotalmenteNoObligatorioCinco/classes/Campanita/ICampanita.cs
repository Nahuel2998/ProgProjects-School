using System.Collections.Generic;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public interface ICampanita
    {
        public Dictionary<string, Tarea> Tareas { get; }
        public IList<IPersona> Subs { get; }
        void NotificarSubs();
    }
}