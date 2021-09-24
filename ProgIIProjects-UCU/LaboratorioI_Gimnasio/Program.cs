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
                Console.WriteLine("[Ingresar Cliente] ('Volver' para volver)\n- - - -\nIngrese: [Numero de Socio / Nombre / Apellido / Peso / Altura]\n- - - -");
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(' ');

                if (input[0].ToLower().Equals("volver"))
                    return;

                if (input.Length < 5)
                {
                    Console.WriteLine("Datos insuficientes.");
                    Console.ReadLine();
                    continue;
                }

                int nSocio;
                if (!Int32.TryParse(input[0], out nSocio))
                {
                    Console.WriteLine("Formato incorrecto. Intentelo nuevamente.");
                    Console.ReadLine();
                    continue;
                }

                Cliente cliente;
                if (ListaClientes.TryGetValue(nSocio, out cliente))
                {
                    Console.WriteLine("El cliente ya existe. Intentelo nuevamente.");
                    Console.ReadLine();
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
                Console.WriteLine("[Eliminar Cliente] ('Volver' para volver)\n- - - -\nA quien matamo: [Numero de Socio]\n- - - -");
                Console.Write("> ");
                string input = Console.ReadLine().Split(' ')[0];

                if (input.ToLower().Equals("volver"))
                    return;

                int victima;
                if (!Int32.TryParse(input, out victima))
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
                Console.ReadLine();
            }
        }

        public static void ListarClientes()
        {
            Console.Clear();
            string res = "";
            foreach (Cliente cliente in ListaClientes.Values)
            {
                res += "\n- - -\n" + cliente.ToString();
            }
            Console.WriteLine($"[Lista de Clientes]\n- - - -{(res.Length > 0 ? res.Remove(0, 6) : "\nNo hay clientes.")}\n- - - -");
            Console.ReadLine();
        }

        public static void ListarDeudas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ingrese un Mes:");
                Console.Write("> ");
                string input = Console.ReadLine().Split(' ')[0];

                int mes;
                if (!Int32.TryParse(input, out mes))
                {
                    Console.WriteLine("Formato incorrecto. Intentelo nuevamente.");
                    Console.ReadLine();
                    continue;
                }

                string res = "";
                foreach (Cliente cliente in ListaClientes.Values)
                {
                    if (cliente.Deuda(mes))
                    {
                        res += "\n- - -\n" + cliente.ToString();
                    }
                }
                Console.Clear();
                Console.WriteLine($"[Lista de Deudas (Mes {mes})]\n- - - -{(res.Length > 0 ? res.Remove(0, 6) : "\nNo hay deuda.")}\n- - - -");
                Console.ReadLine();
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
                        ListarClientes();
                        break;
                    case '5':
                        ListarDeudas();
                        break;
                    case '6':
                        break;
                }
            }
        }
    }
}
