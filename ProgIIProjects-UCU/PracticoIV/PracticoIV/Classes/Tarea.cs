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

        public Tarea()
        {
            Nombre = null;
            Puntaje = 0;
        }

        public override string ToString()
        {
            return $"{Nombre} : {Puntaje}P";
        }
    }
}