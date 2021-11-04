using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NarLib;
using PracticoIV.Classes;

namespace PracticoIV
{
    internal static class Program
    {
        private const string FTareas = "Datos/Tareas.dat";
        private const string FTienda = "Datos/Tienda.dat";
        private const string FUsuarios = "Datos/Usuarios.dat";
        private static Dictionary<uint, Tarea> _listTareas = new();
        private static Dictionary<uint, ItemTienda> _listItemsTienda = new();

        private static void Tareas()
        {
            Menu.BuildMenuGetIndex("[Tareas]", _listTareas.Values
                .Select(tarea => $"{tarea.Nombre} : {tarea.Puntaje}P").ToArray(), true);
        }

        private static void Tienda()
        {
            Menu.BuildMenuGetIndex("[Tienda]", _listItemsTienda.Values
                .Select(item => $"{item.Nombre} : {item.Precio}P").ToArray(), true);
        }

        private static void Main(string[] args)
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
            string[][] lTareas = (from i in File.ReadAllLines(FTareas) select i.Split('|')).ToArray();
            for (uint i = 0; i < lTareas.Length; i++)
                _listTareas.Add(i, new Tarea(lTareas[i][0], int.Parse(lTareas[i][1])));
            
            // Cargar Items de Tienda.
            string[][] lItemsTienda = (from i in File.ReadAllLines(FTienda) select i.Split('|')).ToArray();
            for (uint i = 0; i < lItemsTienda.Length; i++)
                _listItemsTienda.Add(i, new ItemTienda(lItemsTienda[i][0], int.Parse(lItemsTienda[i][1]), lItemsTienda[i][2]));
            
            Option[] options = {
                new("Tareas", Tareas),
                new("Tienda", Tienda)
            };
            
            // "Puntaje: {puntaje}\n- - - -\n"
            Menu.BuildMenu($"[Menu Principal]", options, "Salir");
        }
    }
}