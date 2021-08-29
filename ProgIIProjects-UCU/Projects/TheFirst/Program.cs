using System;

namespace TheFirst
{
    class Persona
    {
        public string nombre;
        public string apellido;
        public int edad;
        public string ubicacion;

        public Persona()
        {
            this.nombre = "John";
            this.apellido = "Don";
            this.edad = 19;
            this.ubicacion = "Somewhere";
        }
        public Persona(string xNombre, string xApellido, int xEdad, string xUbicacion)
        {
            this.nombre = xNombre;
            this.apellido = xApellido;
            this.edad = xEdad;
            this.ubicacion = xUbicacion;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a la aplicacion!");
            // Ingresar Nombre
            Console.WriteLine("Ingrese su nombre: "); 
            String nombre = Console.ReadLine();
            // Ingresar Apellido
            Console.WriteLine("Ingrese su apellido: ");
            String apellido = Console.ReadLine();
            // Ingresar Edad
            Console.WriteLine("Ingrese su edad: ");
            int edad = Int32.Parse(Console.ReadLine());
            // Ingresar Direccion
            Console.WriteLine("Ingrese su direccion: ");
            String direccion = Console.ReadLine();

            Persona pers = new Persona(nombre, apellido, edad, direccion); 
            Persona pers2 = new Persona(); 
            Console.WriteLine(String.Format("Bienvenido {0} {1} de edad {2} viviendo en {3}.", pers.nombre, pers.apellido, pers.edad, pers.ubicacion));
            Console.WriteLine(String.Format("Bienvenido {0} {1} de edad {2} viviendo en {3}.", pers2.nombre, pers2.apellido, pers2.edad, pers2.ubicacion));
        }
    }
}
