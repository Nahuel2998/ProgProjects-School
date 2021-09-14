using System;
using System.Collections.Generic;

namespace PracticoIII
{
    class Program
    {
        static List<Persona> ListaPersonas = new List<Persona>();

        static void SubMenu()
        {
            while (true)
            {
                Console.WriteLine("\nRegresar -> 'Regresar'");
                Console.WriteLine("Ingrese Indice (de persona) y Opcion");
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(' ');

                if (input[0].ToLower().Equals("regresar"))
                { Console.Clear(); break; }

                try {
                    switch (input[1])
                    {
                        case "1":
                            ListaPersonas[Int32.Parse(input[0])].Presentarse();
                        break;
                    }
                } catch 
                { Console.WriteLine("Argumentos invalidos."); }

                Console.ReadLine();
                Console.Clear();
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nExtra -> 1\nSalir -> 0");
                Console.WriteLine("Ingrese Nombre, Apellido, y Documento: ");
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(' ');

                if (input[0].Equals("1"))
                { Console.Clear(); SubMenu(); continue; }
                else if (input[0].Equals("0"))
                    break;

                try {
                    Persona p = new Persona(input[0], input[1], input[2]);
                    ListaPersonas.Add(p);

                    Console.WriteLine("\n" + p.ToString());
                } catch 
                { Console.WriteLine("Argumentos invalidos."); }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
