using System;

namespace PracticoIII
{
    public class Persona
    {
        // public string nombre;
        public string Nombre { get; set; }
        // public string apellido;
        public string Apellido { get; set; }
        // public string documento;
        public string Documento { get; set; }

        public Persona(string xNombre, string xApellido, string xDocumento)
        {
            this.Nombre = xNombre;
            this.Apellido = xApellido;
            this.Documento = xDocumento;
        }
        
        public virtual void Presentarse()
        {
            Console.WriteLine($"Hola! Mi nombre es {this.Nombre} {this.Apellido}. Soy una Persona. Un gusto.");
        }

        // No he usado un WriteLine aqui ya que el atributo original solo retorna.
        public override string ToString()
        {
            return $"Nombre: {this.Nombre}\nApellido: {this.Apellido}\nDocumento: {this.Documento}";
        }
    }
}