using System;
using System.Collections.Generic;

public class Banda
{
    public string Nombre { get; set; }
    public string Genero { get; set; }
    public List<Persona> Integrantes { get; set; }
    public List<Album> Albumes { get; set; }

    public Banda(string nombre, string genero, List<Persona> integrantes, List<Album> albumes)
    {
        this.Nombre = nombre;
        this.Genero = genero;
        this.Integrantes = integrantes;
        this.Albumes = albumes;
    }

    public string obtenerIntegrantes()
    {
        string res = "";
        int nIntegrante = 1;
        foreach (Persona integrante in this.Integrantes)
        {
            res += $"\n{nIntegrante++}: {integrante.getNombreCompleto()}";
        }
        return (res.Length > 0) ? res.Remove(0, 1) : "No hay integrantes.";
    }

    public string obtenerAlbumes()
    {
        string res = "";
        int nAlbum = 1;
        foreach (Album album in this.Albumes)
        {
            res += $"\n{nAlbum++}: {album.Nombre} | ({album.getCantidadCanciones()} Canciones) | [{album.getDuracion()}s]";
        }
        return (res.Length > 0) ? res.Remove(0, 1) : "No hay albumes.";
    }

    // TODO: Metodo para obtener todos los datos en un string.
}