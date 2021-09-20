using System.Collections.Generic;

namespace PracticoIII
{
    public class WebAsignatura 
    {
        public List<Tarea> ListaTareas { get; set; }

        public WebAsignatura(List<Tarea> xListaTareas)
        {
            this.ListaTareas = xListaTareas;
        }
    }
}
