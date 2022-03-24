using System;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public class Cliente : IPersona
    {
        public string Nombre { get; }

        public Cliente(string nombre)
        { Nombre = nombre; }
        
        // Tirara exception si la key ya existe.
        // Quiero manejar todas las exceptions desde el programa, y no las clases, por eso lo hago de esta manera.
        // No me gusta poner prints en una clase si no es para debug.
        public void CrearTarea(string nombre)
        {
            Campanita.GetInstance().Tareas.Add(nombre, new Tarea(nombre));
            Campanita.GetInstance().NotificarSubs(nombre);
        }

        public void Update(ITarea tarea)
        {
            if (tarea.Estado > 1)
            { Console.WriteLine($"{Nombre}] yay, nueva tarea ({tarea.Nombre}) completada"); }
        }
    }
}