using System.Collections.Generic;

namespace PracticoIII
{
    public class Curso
    {
        public Docente DocenteTitular { get; set; }
        public WebAsignatura Webasignatura { get; set; }
        public List<Alumno> ListaAlumnos { get; set; }

        public Curso(Docente xDocenteTitular, WebAsignatura xWebasignatura, List<Alumno> xListaAlumnos)
        {
            this.DocenteTitular = xDocenteTitular;
            this.Webasignatura = xWebasignatura;
            this.ListaAlumnos = xListaAlumnos;
        }

        public string ObtenerAlumnosString()
        {
            string res = "";

            foreach (Alumno a in ListaAlumnos)
            {
                res += "\n" + a.Nombre;
            }

            return res.Length > 0 ? res.Remove(0, 1) : "No hay alumnos.";
        }
    }
}
