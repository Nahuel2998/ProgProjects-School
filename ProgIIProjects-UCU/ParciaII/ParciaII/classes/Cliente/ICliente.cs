namespace ParciaII.classes.Cliente
{
    public interface ICliente
    {
        public string Nombre { get; set; }
        void NewFoodJustDropped();
        void Comprar();
    }
}