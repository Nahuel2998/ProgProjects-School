using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NarLib;
using PracticoIV.Classes;

namespace PracticoIV
{
    class Program
    {
        private static readonly string _fTareas = "../../../Datos/Tareas.dat";
        private static readonly string _fTienda = "../../../Datos/Tienda.dat";
        private static readonly string _fUsuarios = "../../../Datos/Usuarios.dat";
        private static Dictionary<uint, Tarea> ListTareas = new();
        private static Dictionary<uint, ItemTienda> ListItemsTienda = new();
        
        static void Tareas()
        {
            Menu.BuildMenuGetIndex("[Tareas]", (from tarea in ListTareas.Values select $"{tarea.Nombre} : {tarea.Puntaje}P").ToArray());
        }

        static void Tienda()
        {
            Menu.BuildMenuGetIndex("[Tienda]", (from item in ListItemsTienda.Values select $"{item.Nombre} : {item.Precio}P").ToArray());
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Usuario: ");
            string user = Console.ReadLine();
            
            // TODO: Check whether it's admin or an existing user.
            /* [Usuarios Pedidos]
             * Admin        | Agregar/Quitar/Modificar Tareas
             * Coordinador  | Agregar/Quitar Objetos (Tienda)
             * Usuario
             */

            // TODO: Cargar Puntaje. 
            // int puntaje = 0;
            
            // Cargar Tareas.
            string[][] lTareas = (from i in File.ReadAllLines(_fTareas) select i.Split('|')).ToArray();
            for (uint i = 0; i < lTareas.Length; i++)
                ListTareas.Add(i, new Tarea(lTareas[i][0], Int32.Parse(lTareas[i][1])));
            
            // Cargar Items de Tienda.
            string[][] lItemsTienda = (from i in File.ReadAllLines(_fTienda) select i.Split('|')).ToArray();
            for (uint i = 0; i < lItemsTienda.Length; i++)
                ListItemsTienda.Add(i, new ItemTienda(lItemsTienda[i][0], Int32.Parse(lItemsTienda[i][1]), lItemsTienda[i][2]));
            
            Option[] options = {
                new("Tareas", Tareas),
                new("Tienda", Tienda)
            };
            
            // "Puntaje: {puntaje}\n- - - -\n"
            Menu.BuildMenu($"[Menu Principal]", options, "Salir");
        }
    }
}