using System;
using System.Collections.Generic;

namespace PracticoI
{
    class Program
    {
        static List<Propiedad> listaPropiedades = new List<Propiedad>();

        static void EditarHabitaciones(Propiedad p)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[Editar Habitaciones]\n- - - - -");
                Console.WriteLine($"Habitaciones:\n{p.obtenerHabitaciones(true)}\n- - - - -\nn <X>x<Y>x<Z>\tAñadir habitacion de dimensiones XxYxZ\nd <n>\t\tEliminar Habitacion n\n0) Guardar\n- - - - -");
                Console.Write("> ");
                string input = Console.ReadLine();
                switch (input.Length > 0 ? input[0] : ';')
                {
                    case 'n':
                        string[] i_xyz = input.Substring(2).Split("x", 3);
                        double[] xyz = new double[] {0, 0, 0};
                        for (int i = 0; i < i_xyz.Length; i++)
                        {
                            xyz[i] = Double.Parse(i_xyz[i]);
                        }
                        p.Habitaciones.Add(new Dimensiones(xyz[0], xyz[1], xyz[2]));
                        break;
                    case 'd':
                        p.Habitaciones.RemoveAt(Int32.Parse(input.Substring(2)));
                        break;
                    case '0':
                        return;
                }
            }
        }

        static void BuscarPropiedad()
        {
            while (true)
            {
                Console.WriteLine("[Buscar Propiedad]\n- - - - -");
                Console.WriteLine("*\t\t\tMostrar Todo\nid <ID>\t\t\tBuscar por ID\nch <+/-><Cantidad>\tBuscar por Cantidad de Habitaciones\na <+/-><Area>\t\tBuscar por Area\nd <+/->\t\t\tBuscar por Disponibilidad\n0) Regresar\n- - - - -");
                Console.Write("> ");
                string input = Console.ReadLine();
                Console.Clear();
                switch (input.Length > 0 ? input.Substring(0, Math.Max(input.IndexOf(' '), 1)) : ";")
                {
                    case "*":
                        Console.WriteLine($"[Resultados: {input}]\n- - - - -");
                        foreach (Propiedad p in listaPropiedades)
                        {
                            Console.WriteLine($"{p.obtenerDatos()}\n- - - - -");
                        }
                        break;
                    case "id":
                        Console.WriteLine($"[Resultados: {input}]\n- - - - -");
                        int id = Int32.Parse(input.Substring(3));
                        foreach (Propiedad p in listaPropiedades)
                        {
                            if (p.Id == id)
                                Console.WriteLine($"{p.obtenerDatos()}\n- - - - -");
                        }
                        break;
                    case "ch":
                        Console.WriteLine($"[Resultados: {input}]\n- - - - -");
                        bool mayormenor = input[3] == '-';
                        int ch = Int32.Parse(input.Substring(4));
                        foreach (Propiedad p in listaPropiedades)
                        {
                            bool cond = p.getCantidadHabitaciones() >= ch;
                            if ((cond || mayormenor) && (!cond || !mayormenor))
                                Console.WriteLine($"{p.obtenerDatos()}\n- - - - -");
                        }
                        break;
                    case "a":
                        Console.WriteLine($"[Resultados: {input}]\n- - - - -");
                        bool mayormenor1 = input[2] == '-';
                        int a = Int32.Parse(input.Substring(3));
                        foreach (Propiedad p in listaPropiedades)
                        {
                            bool cond1 = p.getArea() >= a;
                            if ((cond1 || mayormenor1) && (!cond1 || !mayormenor1))
                                Console.WriteLine($"{p.obtenerDatos()}\n- - - - -");
                        }
                        break;
                    case "d":
                        Console.WriteLine($"[Resultados: {input}]\n- - - - -");
                        bool estado = input[2] == '+';
                        foreach (Propiedad p in listaPropiedades)
                        {
                            if (p.Disponible == estado)
                                Console.WriteLine($"{p.obtenerDatos()}\n- - - - -");
                        }
                        break;
                    case "0":
                        return;
                }
                Console.Write("> ");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void Main(string[] args)
        {
            Propiedad objPropiedad = new Propiedad();
            listaPropiedades.Add(objPropiedad);

            // Console.WriteLine(objPropiedad.obtenerDatos());

            while (true)
            {
                Console.WriteLine("[Menu Principal]\n- - - - -");
                Console.WriteLine("1) Ingresar Propiedad\n2) Buscar Propiedad\n3) Editar Propiedad\n0) Salir\n- - - - -");
                Console.Write("> ");
                string input = Console.ReadLine();
                Console.Clear();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("[Ingresar Propiedad]\n- - - - -");
                        Console.WriteLine("ID de propiedad:");
                        Console.Write("> ");
                        int id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Disponible? [S/n]");
                        Console.Write("> ");
                        string i_disp = Console.ReadLine();
                        bool disp = i_disp.Length > 0 ? Char.ToLower(i_disp[0]).Equals('n') ? false : true : true;
                        Propiedad nPropiedad = new Propiedad(id, disp);
                        EditarHabitaciones(nPropiedad);
                        listaPropiedades.Add(nPropiedad);
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        BuscarPropiedad();
                        break;
                    case "0":
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
