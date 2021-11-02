namespace PracticoIV.Classes
{
    public class Tarea
    {
        public string Nombre { get; set; }
        public int Puntaje { get; set; }

        public Tarea(string nombre, int puntaje)
        {
            Nombre = nombre;
            Puntaje = puntaje;
        }
    }
}