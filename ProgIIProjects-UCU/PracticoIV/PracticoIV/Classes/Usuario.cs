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

        public Usuario(string nombre, bool? permisos = null, List<string> inventario = null)
        {
            Nombre = nombre;
            Permisos = permisos;
            Inventario = inventario;
        }
    }
}