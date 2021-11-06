using System.Collections.Generic;

namespace PracticoIV.Classes
{
    public class Usuario
    {
        public string Nombre { get; set; } 
        // Null = Usuario comun
        // False = Coordinador
        // True = Admin
        public bool? Permisos { get; set; }
        public List<string> Inventario { get; set; }
        public int Puntaje { get; set; }

        public Usuario(string nombre, bool? permisos = null, int puntaje = 0, List<string> inventario = null)
        {
            Nombre = nombre;
            Permisos = permisos;
            Puntaje = puntaje;
            Inventario = inventario;
        }

        public Usuario()
        {
            Nombre = null;
            Permisos = null;
            Puntaje = 0;
            Inventario = null;
        }
    }
}