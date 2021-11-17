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
        private static List<Tarea> _listTareas = new();
        private static List<ItemTienda> _listItemsTienda = new();
        private static Dictionary<string, Usuario> _listUsuarios = new();
        private static Usuario _usuarioSeleccionado = new();

        private static void GuardarDatos()
        {
            _listUsuarios[_usuarioSeleccionado.Nombre.ToLower()] = _usuarioSeleccionado;
            File.WriteAllText(FTareas, new SerializerBuilder().Build().Serialize(_listTareas));
            File.WriteAllText(FTienda, new SerializerBuilder().Build().Serialize(_listItemsTienda));
            File.WriteAllText(FUsuarios, new SerializerBuilder().Build().Serialize(_listUsuarios));
        }

        private static void AdminMenu()
        {
            Option[] adminOptions = {
                new("Agregar Usuario", AgregarUsuario),
                new("Listar Usuarios", ListarUsuarios)
            };
            
            Menu.BuildMenu("[Admin Menu]", adminOptions, "Regresar",
                bottomTextFunc: () =>
                    $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
        }

        private static void AgregarItem()
        {
            string prompt = "  [Agregar Item]\n- - - - - - - - -\nIngresar: <Nombre : Precio>\n- - - - - - - - -";
            // string input = Console.ReadLine();
            // Console.WriteLine(input);
            
            // FIXME: This.
            _listItemsTienda.Add(ItemTienda.ToItem(
                $"{GetValidInput.GetValidStringInput(prompt, splitArg: "", conditionRegex: "[^<][^( : )]+ : [\\d]+")} : {_usuarioSeleccionado.Nombre}"));
            Console.WriteLine("Item aniadido satisfactoriamente.");
            Console.ReadKey();
        }

        private static void AgregarUsuario()
        {
            
        }

        private static void ListarUsuarios()
        {
            string usuariosString = File.ReadAllText(FUsuarios);
            Console.Clear();
            Console.WriteLine("  [Usuarios]");
            Console.WriteLine("- - - - - - - -");
            Console.WriteLine(usuariosString.Remove(usuariosString.Length - 1));
            Console.WriteLine("- - - - - - - -");
            Console.ReadKey();
        }

        private static void Inventario()
        {
            int selected = Menu.BuildMenuGetIndex("[Inventario]", _usuarioSeleccionado.Inventario
                .Select(item => item.ToString()).ToArray(), cancellable: true,
                bottomText: $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");

            if (selected == -1)
            {
                return;
            }

            _usuarioSeleccionado.Inventario.RemoveAt(selected);
            GuardarDatos();
            
            Console.WriteLine("Item usado.");
            Console.ReadKey();
        }

        private static void Tareas()
        {
            int selected = Menu.BuildMenuGetIndex("[Tareas]", _listTareas
                .Select(tarea => tarea.ToString()).ToArray(), cancellable: true,
                bottomText: $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
            
            if (selected == -1)
                return;

            _usuarioSeleccionado.Puntaje += _listTareas[selected].Puntaje;
            GuardarDatos();
            
            Console.WriteLine("Tarea realizada correctamente.");
            Console.ReadKey();
        }

        private static void Tienda()
        {
            int selected = Menu.BuildMenuGetIndex("[Tienda]", _listItemsTienda
                .Select(item => item.ToString()).ToArray(), cancellable: true,
                bottomText: $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
            
            if (selected == -1)
                return;

            if (_usuarioSeleccionado.Puntaje < _listItemsTienda[selected].Precio)
            {
                Console.WriteLine("No tienes puntos suficiente para comprar este item.");
                Console.ReadKey();
                return;
            }
            
            _usuarioSeleccionado.Puntaje -= _listItemsTienda[selected].Precio;
            _usuarioSeleccionado.Inventario.Add(_listItemsTienda[selected]);
            _listItemsTienda.RemoveAt(selected);
            GuardarDatos();
            
            Console.WriteLine("Item aniadido a inventario.");
            Console.ReadKey();
        }

        private static void Main(string[] args)
        {
            _listUsuarios = new DeserializerBuilder().Build()
                .Deserialize<Dictionary<string, Usuario>>(File.ReadAllText(FUsuarios));
            
            Console.WriteLine("Usuario: ");
            Console.Write("> ");
            if (!_listUsuarios.TryGetValue(Console.ReadLine()?.ToLower() ?? string.Empty, out _usuarioSeleccionado))
            {
                Console.WriteLine("El usuario no existe.");
                Console.ReadKey();
                return;
            }
            
            // Cargar datos.
            _listTareas = new DeserializerBuilder().Build()
                .Deserialize<List<Tarea>>(File.ReadAllText(FTareas));
            _listItemsTienda = new DeserializerBuilder().Build()
                .Deserialize<List<ItemTienda>>(File.ReadAllText(FTienda));
            
            /* [Usuarios Pedidos]
             * Admin        | Agregar/Quitar/Modificar Tareas
             * Coordinador  | Agregar/Quitar Objetos (Tienda)
             * Usuario
             */

            List<Option> options = new List<Option>
            {
                new("Tareas", Tareas),
                new("Tienda", Tienda),
                new("Inventario", Inventario)
            };

            // Aniadir opciones opcionales
            if (_usuarioSeleccionado.Permisos != null)
            {
                options.Add(new Option("Agregar Item", AgregarItem));
                if ((bool) _usuarioSeleccionado.Permisos)
                    options.Add(new Option("Administrador", AdminMenu));
            }

            // "Puntaje: {puntaje}\n- - - -\n"
            Menu.BuildMenu($"[Menu Principal]", options.ToArray(), "Salir",
                bottomTextFunc: () =>
                    $"Usuario: {_usuarioSeleccionado.Nombre}\nPuntaje: {_usuarioSeleccionado.Puntaje}P");
            
            // Guardar datos.
            GuardarDatos();
        }
    }
}