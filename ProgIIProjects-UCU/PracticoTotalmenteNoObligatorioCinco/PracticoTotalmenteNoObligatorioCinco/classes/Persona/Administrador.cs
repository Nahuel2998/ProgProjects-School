using System;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    // Gerente de Proyectos, since it's only mentioned once so yeah
    public class Administrador : IPersona
    {
        public string Nombre { get; }

        public Administrador(string nombre)
        { Nombre = nombre; }
        
        public void Update(ITarea tarea)
        {
            switch (tarea.Estado)
            {
                case 1:
                    Console.WriteLine
                        ($"{Nombre}] Una nueva tarea ({tarea.Nombre}) ha sido detectada. Mis habilidades estan siendo llamadas.");
                    break;
                case > 1:
                    Console.WriteLine($"{Nombre}] yay, nueva tarea ({tarea.Nombre}) completada");
                    break;
            }
        }

        public void RechazarTarea(string nombre)
        {
            Campanita.GetInstance().Tareas[nombre].Rechazar();
            Campanita.GetInstance().NotificarSubs(nombre);
        }

        public void AprobarTarea(string nombre)
        {
            Campanita.GetInstance().Tareas[nombre].Aprobar();
            Campanita.GetInstance().NotificarSubs(nombre);
        }
    }
}