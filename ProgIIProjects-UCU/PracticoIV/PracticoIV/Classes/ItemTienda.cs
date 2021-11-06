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
    }
}