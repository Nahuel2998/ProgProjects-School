namespace PracticoIV.Classes
{
    public class Item : IItem
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }

        public Item(string nombre, int precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
    }
}