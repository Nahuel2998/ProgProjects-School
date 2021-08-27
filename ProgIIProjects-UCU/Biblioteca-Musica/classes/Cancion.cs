using System;
using System.Collections.Generic;

public class Cancion
{
    public string Nombre { get; set; }
    public int Duracion { get; set; }

    public Cancion(string nombre, int duracion)
    {
        this.Nombre = nombre;
        this.Duracion = duracion;
    }
}