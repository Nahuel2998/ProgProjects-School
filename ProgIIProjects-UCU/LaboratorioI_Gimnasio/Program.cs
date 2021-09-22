using System;
using System.Collections.Generic;

namespace LaboratorioI_Gimnasio
{
    class Program
    {
        static Dictionary<int, Cliente> ListaClientes = new Dictionary<int, Cliente>();

        static void IngresarCliente()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ingrese: [Numero de Socio / Nombre / Apellido / Peso / Altura] ('Volver' para volver)");
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(' ');

                int nSocio;
                if (!Int32.TryParse(input[0], out nSocio))
                {
                    if (input[0].ToLower().Equals("volver"))
                        return;

                    Console.WriteLine("Formato incorrecto. Intentelo nuevamente.");
                    Console.ReadLine();
                    continue;
                }

                Cliente cliente;
                if (ListaClientes.TryGetValue(nSocio, out cliente))
                {
                    Console.WriteLine("El cliente ya existe. Intentelo nuevamente.");
                    continue;
                }

                cliente = new Cliente(nSocio, input[1], input[2], Double.Parse(input[3]), Double.Parse(input[4]));
                ListaClientes.Add(nSocio, cliente);

                Console.WriteLine("El cliente ha sido ingresado correctamente. Gracias.");
                Console.ReadLine();
                return;
            }
        }

        static void EliminarCliente()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("A quien matamo: ");
                Console.Write("> ");
                int victima;
                if (!Int32.TryParse(Console.ReadLine().Split(' ')[0], out victima))
                {
                    Console.WriteLine("Formato incorrecto. Intentelo nuevamente.");
                    Console.ReadLine();
                    continue;
                }

                if (ListaClientes.Remove(victima))
                {
                    Console.WriteLine("El cliente ha sido eliminado correctamente. Gracias.");
                    Console.ReadLine();
                    return;
                }
                
                Console.WriteLine("Cliente no encontrado. Intentelo nuevamente.");
                Console.Read();
            }
        }

        static void Main(string[] args)
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("[Menu Principal]\n- - - -\n1) Ingresar Cliente\n2) Eliminar Cliente\n3) Pagar Cuota\n4) Listar Clientes\n5) Lista de Deudas\n6) Ver Cliente\n0) Salir\n- - - -");
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input[0].Equals('0'))
                    break;
                
                switch (input[0])
                {
                    case '1':
                        IngresarCliente();
                        break;
                    case '2':
                        EliminarCliente();
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '5':
                        break;
                    case '6':
                        break;
                }
            }
        }
    }
}
