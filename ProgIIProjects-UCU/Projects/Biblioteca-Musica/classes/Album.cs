using System;
using System.Collections.Generic;

public class Album
{
    public string Nombre { get; set; }
    public List<Cancion> Canciones { get; set; }

    public Album(string nombre, List<Cancion> canciones)
    {
        this.Nombre = nombre;
        this.Canciones = canciones;
    }
        
    public int getDuracion()
    {
        int res = 0;
        foreach (Cancion c in this.Canciones)
        {
            res += c.Duracion;
        }
        return res;
    }

    public string obtenerCanciones()
    {
        string res = "";
        int nCancion = 1;
        foreach (Cancion cancion in this.Canciones)
        {
            res += $"\n{nCancion++}: {cancion.Nombre} | [{cancion.Duracion}s]";
        }
        return (res.Length > 0) ? res.Remove(0, 1) : "No hay canciones.";
    }
        
    public int getCantidadCanciones()
    {
        return this.Canciones.Count;
    }
}