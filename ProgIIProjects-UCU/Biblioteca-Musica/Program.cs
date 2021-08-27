using System;
using System.Collections.Generic;

namespace Biblioteca_Musica
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] listaPersonas = 
            {
                "Marco Polo",
                "Elin Stancia",
                "Josuerta Adoquines",
                "Isfisl Iruslfo"
            };
            List<Persona> integrantes = new List<Persona>();
            foreach (string p in listaPersonas)
            {
                string[] datosPersona = p.Split(" ");
                integrantes.Add(new Persona(datosPersona[0], datosPersona[1]));
            }

            string[] listaCanciones = 
            {
                "Tostada en Fuego:370",
                "Menta Granizada Casera:214",
                "Extraccion de Azucar:100",
                "Pasto:900"
            };
            List<Cancion> canciones = new List<Cancion>();
            foreach (string c in listaCanciones)
            {
                string[] datosCancion = c.Split(":");
                canciones.Add(new Cancion(datosCancion[0], Int32.Parse(datosCancion[1])));
            }
            Album album = new Album("Preparacion", canciones);
            List<Album> albumes = new List<Album>() {album};

            Banda banda = new Banda("ExpiredBread", "Classical", integrantes, albumes);

            Console.WriteLine(banda.obtenerIntegrantes());
            Console.WriteLine(banda.obtenerAlbumes());
            Console.WriteLine(banda.Albumes[0].obtenerCanciones());

            // Menus
            // while (true)
            // {
            //     Console.Clear();
            //     Console.WriteLine("[Menu Principal]");
            //     Console.WriteLine("- - - - -");
            //     Console.WriteLine("1) Agregar Banda");
            //     Console.WriteLine("2) Buscar Banda");
            //     Console.WriteLine("0) Salir");
            //     Console.WriteLine("- - - - -");
            //     Console.Write("> ");
            // }
        }
    }
}
