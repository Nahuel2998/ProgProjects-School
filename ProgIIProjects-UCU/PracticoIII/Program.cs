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
                Console.WriteLine("\nRegresar -> '-1'");
                Console.WriteLine("Ingrese Indice (de persona) y Opcion");
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(' ');

                if (input[0].ToLower().Equals("-1"))
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
                Console.WriteLine("\nIngresar -> 1\nRevisar -> 2\nSalir -> 0");
                Console.Write("> ");
                string opcion = Console.ReadLine();

                if (opcion[0].Equals('2'))
                { Console.Clear(); SubMenu(); continue; }
                else if (opcion[0].Equals('0'))
                    break;

                Console.WriteLine("Ingrese Tipo (y ID)");
                Console.Write("> ");
                string[] i = Console.ReadLine().Split(' ');
                Console.WriteLine("Ingrese Nombre, Apellido, y Documento: ");
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(' ');

                try {
                    switch(i[0])
                    {
                        case "1":
                            Docente p = new Docente(input[0], input[1], input[2], UInt32.Parse(i[1]));
                            ListaPersonas.Add(p);
                            break;
                        case "2":
                            Alumno p1 = new Alumno(input[0], input[1], input[2], UInt32.Parse(i[1]));
                            ListaPersonas.Add(p1);
                            break;
                        default:
                            Persona p2 = new Persona(input[0], input[1], input[2]);
                            ListaPersonas.Add(p2);
                            break;
                    }
                    Console.WriteLine("\n" + ListaPersonas[ListaPersonas.Count - 1].ToString());
                } catch 
                { Console.WriteLine("Argumentos invalidos."); }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
