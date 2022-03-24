using System;
using System.Collections.Generic;
using System.Linq; // casi pero no
using PracticoTotalmenteNoObligatorioCinco.classes;
using static PracticoTotalmenteNoObligatorioCinco.classes.Constantes;
using static PracticoTotalmenteNoObligatorioCinco.classes.PersonaFactory;

namespace PracticoTotalmenteNoObligatorioCinco
{
    class Program
    {
        static void Main(string[] args)
        {
            Cliente c = (Cliente) PersonaBuilder(CLIENTE, "Marcos");
            Administrador admin = (Administrador) PersonaBuilder(ADMINISTRADOR, "Macro");
            Desarrollador desu = (Desarrollador) PersonaBuilder(DESARROLLADOR, "Macross");

            Campanita.GetInstance().Subs.Add(c);
            Campanita.GetInstance().Subs.Add(admin);
            Campanita.GetInstance().Subs.Add(desu);
            
            ////////////////////////////////////////
            
            c.CrearTarea("buenas");
            c.CrearTarea("yes");
            c.CrearTarea("dias");
            
            ////////////////////////////////////////

            try
            { admin.RechazarTarea("hooooola"); }
            catch (KeyNotFoundException)
            { Console.WriteLine("La Tarea nunca existio."); }
            catch (TareaAprobadaException)
            { Console.WriteLine("La Tarea ya esta aprobada/terminada, no se arrepienta."); }
            catch (TareaRechazadaException)
            { Console.WriteLine("La Tarea ya estaba rechazada, no hubo cambios."); }
            
            ////////////////////////////////////////

            Random r = new();
            var tareasPendientes = Campanita.GetInstance().GetTareasPendientes();
            
            try
            { admin.AprobarTarea(tareasPendientes[r.Next(tareasPendientes.Length)]); }
            catch (KeyNotFoundException)
            { Console.WriteLine("La Tarea nunca existio."); }
            catch (TareaAprobadaException)
            { Console.WriteLine("La Tarea ya estaba aprobada/terminada."); }
            catch (TareaRechazadaException)
            { Console.WriteLine("La Tarea ya esta rechazada, no se arrepienta."); }

            ////////////////////////////////////////
            
            // var tareasAprobadas = Campanita.GetInstance().GetTareasAprobadas();
            // Una ruleta rusa es mejor 
            
            try
            { desu.TerminarTarea("buenas"); }
            catch (KeyNotFoundException)
            { Console.WriteLine("La Tarea (buenas) nunca existio."); }
            catch (TareaAprobadaException)
            { Console.WriteLine("La Tarea (buenas) ya estaba terminada."); }
            catch (TareaRechazadaException)
            { Console.WriteLine("La Tarea (buenas) no fue aprobada."); }
            
            ////////////////////////////////////////
        }
    }
}