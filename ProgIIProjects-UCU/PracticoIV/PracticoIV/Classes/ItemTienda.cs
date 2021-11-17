using System.Text.RegularExpressions;

namespace PracticoIV.Classes
{
    public class ItemTienda : Item
    {
        public string Agregador { get; set; }

        public ItemTienda(string nombre, int precio, string agregador) : base(nombre, precio)
        {
            Agregador = agregador;
        }

        public ItemTienda() : base()
        {
            Agregador = null;
        }

        public override string ToString()
        {
            return $"{base.ToString()} : {Agregador}";
        }

        public static ItemTienda ToItem(string s)
        {
            string[] param = s.Split(" : ");
            return new ItemTienda(param[0], int.Parse(param[1]), param[2]);
        }
    }
}