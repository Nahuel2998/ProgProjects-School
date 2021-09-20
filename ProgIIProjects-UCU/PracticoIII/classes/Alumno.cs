using System;
using System.Collections.Generic;

namespace PracticoIII
{
    public class Alumno : Persona
    {
        public uint IdAlumno { get; set; } 
        public List<Tarea> ListaTareasRealizadas { get; set; }

        public Alumno(string xNombre, string xApellido, string xDocumento, uint xIdAlumno) :base (xNombre, xApellido, xDocumento)
        {
            this.IdAlumno = xIdAlumno;
        }

        public void ResponderTarea(Tarea tarea, string respuestas)
        {
            Tarea resTarea = tarea;    
            resTarea.Respuestas = respuestas;

            this.ListaTareasRealizadas.Add(resTarea);
        }

        public override void Presentarse()
        {
            Console.WriteLine($"Hola! Mi nombre es {this.Nombre} {this.Apellido}. Soy un Alumno. Un gusto.");
        }
        
        public override string ToString()
        {
            return $"Nombre: {this.Nombre}\nApellido: {this.Apellido}\nDocumento: {this.Documento}\nOcupacion: Alumno";
        }
    }
}
