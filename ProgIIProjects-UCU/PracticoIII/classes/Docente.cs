using System;

namespace PracticoIII
{
    public class Docente : Persona
    {
        public uint IdDocente { get; set; }

        public Docente(string xNombre, string xApellido, string xDocumento, uint xIdDocente) :base (xNombre, xApellido, xDocumento)
        {
            this.IdDocente = xIdDocente;
        }

        public override void Presentarse()
        {
            Console.WriteLine($"Hola! Mi nombre es {this.Nombre} {this.Apellido}. Soy un Docente. Un gusto.");
        }

        public void AgregarTarea(WebAsignatura webasignatura, Tarea tarea)
        {
            webasignatura.ListaTareas.Add(tarea);
        }
    }
}