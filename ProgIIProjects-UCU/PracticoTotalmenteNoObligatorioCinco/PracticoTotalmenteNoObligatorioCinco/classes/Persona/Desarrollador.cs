using System;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public class Desarrollador : IPersona
    {
        public string Nombre { get; }

        public Desarrollador(string nombre)
        { Nombre = nombre; }
        
        public void Update(ITarea tarea)
        {
            if (tarea.Estado == 1)
            { Console.WriteLine($"{Nombre}] Ah shit here we go again."); }
        }

        public void TerminarTarea(string nombre)
        {
            Campanita.GetInstance().Tareas[nombre].Terminar();
            Campanita.GetInstance().NotificarSubs(nombre); 
        }
    }
}