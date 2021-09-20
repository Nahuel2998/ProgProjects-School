namespace PracticoIII
{
    public class Tarea
    {
        public uint IdTarea { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public string Respuestas { get; set; }

        public Tarea(uint xIdTarea, string xNombre, string xContenido, string xRespuestas)
        {
            this.IdTarea = xIdTarea;
            this.Nombre = xNombre; 
            this.Contenido = xContenido;
            this.Respuestas = xRespuestas;
        }
    }
}
