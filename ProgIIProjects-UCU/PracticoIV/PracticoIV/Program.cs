using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NarLib;
using PracticoIV.Classes;
using YamlDotNet.Serialization;

namespace PracticoIV
{
    internal static class Program
    {
        private const string FTareas = "Datos/Tareas.yaml";
        private const string FTienda = "Datos/Tienda.yaml";
        private const string FUsuarios = "Datos/Usuarios.yaml";
        private static Dictionary<uint, Tarea> _listTareas = new();
        private static Dictionary<uint, ItemTienda> _listItemsTienda = new();
        private static Dictionary<string, Usuario> _listUsuarios = new();
        private static Usuario _usuarioSeleccionado = new();

        private static void Tareas()
        {
            int selected = Menu.BuildMenuGetIndex("[Tareas]", _listTareas.Values
                .Select(tarea => $"{tarea.Nombre} : {tarea.Puntaje}P").ToArray(), true,
                $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
            
            if (selected == -1)
                return;
            
            // TODO: The rest.
        }

        private static void Tienda()
        {
            int selected = Menu.BuildMenuGetIndex("[Tienda]", _listItemsTienda.Values
                .Select(item => $"{item.Nombre} : {item.Precio}P").ToArray(), true,
                $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
        }

        private static void Main(string[] args)
        {
            _listUsuarios = new DeserializerBuilder().Build().Deserialize<Dictionary<string, Usuario>>(File.ReadAllText(FUsuarios));
            
            Console.WriteLine("Usuario: ");
            Console.Write("> ");
            if (!_listUsuarios.TryGetValue(Console.ReadLine()!.ToLower(), out _usuarioSeleccionado))
            {
                Console.WriteLine("El usuario no existe.");
                Console.ReadLine();
                return;
            }
            
            // Cargar datos.
            _listTareas = new DeserializerBuilder().Build().Deserialize<Dictionary<uint, Tarea>>(File.ReadAllText(FTareas));
            _listItemsTienda = new DeserializerBuilder().Build().Deserialize<Dictionary<uint, ItemTienda>>(File.ReadAllText(FTienda));
            
            // TODO: Check whether it's admin or an existing user.
            /* [Usuarios Pedidos]
             * Admin        | Agregar/Quitar/Modificar Tareas
             * Coordinador  | Agregar/Quitar Objetos (Tienda)
             * Usuario
             */
            
            Option[] options = {
                new("Tareas", Tareas),
                new("Tienda", Tienda)
            };
            
            // "Puntaje: {puntaje}\n- - - -\n"
            Menu.BuildMenu($"[Menu Principal]", options, "Salir", $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
            
            // Guardar datos.
            File.WriteAllText(FTareas, new SerializerBuilder().Build().Serialize(_listTareas));
            File.WriteAllText(FTienda, new SerializerBuilder().Build().Serialize(_listItemsTienda));
            File.WriteAllText(FUsuarios, new SerializerBuilder().Build().Serialize(_listUsuarios));
        }
    }
}