using System;
using System.Collections.Generic;

public class Persona
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }

    public Persona(string nombre, string apellido)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
    }

    public string getNombreCompleto()
    {
        return $"{this.Nombre} {this.Apellido}";
    }
}