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
        public List<ItemTienda> Inventario { get; set; }
        public int Puntaje { get; set; }

        public Usuario(string nombre, bool? permisos = null, int puntaje = 0, List<ItemTienda> inventario = null)
        {
            Nombre = nombre;
            Permisos = permisos;
            Puntaje = puntaje;
            Inventario = inventario ?? new List<ItemTienda>();
        }

        public Usuario()
        {
            Nombre = null;
            Permisos = null;
            Puntaje = 0;
            Inventario = new List<ItemTienda>();
        }

        public static Usuario ToUser(string s)
        {
            string[] param = s.Split(" : ");
            char param1 = param[1].ToLower()[0];
            return new Usuario(param[0], param1.Equals('a') ? true : param1.Equals('c') ? false : null,
                int.Parse(param[2]));
        }
    }
}