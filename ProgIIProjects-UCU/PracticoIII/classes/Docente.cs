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

        public Docente(string xNombre, string xApellido, uint xIdDocente) :base (xNombre, xApellido)
        {
            this.IdDocente = xIdDocente;
        }

        public void AgregarTarea(WebAsignatura webasignatura, Tarea tarea)
        {
            webasignatura.ListaTareas.Add(tarea);
        }

        public string ObtenerTareasRealizadasString(Curso curso)
        {
            string res = "";

            foreach (Alumno a in curso.ListaAlumnos)
            {
                res += $"\n-{a.Nombre} {a.Apellido}: \n- - -";
                foreach (Tarea t in a.ListaTareasRealizadas)
                    res += "\n" + t.ToString() + "\n- - -";
            }
            
            return res.Length > 0 ? res.Remove(0, 1) : "Nadie ha hecho tareas.";
        }

        public override void Presentarse()
        {
            Console.WriteLine($"Hola! Mi nombre es {this.Nombre} {this.Apellido}. Soy un Docente. Un gusto.");
        }

        public override string ToString()
        {
            return $"Nombre: {this.Nombre}\nApellido: {this.Apellido}\nDocumento: {(this.Documento.Length > 0 ? this.Documento : "No especificado.")}\nOcupacion: Docente";
        }
    }
}