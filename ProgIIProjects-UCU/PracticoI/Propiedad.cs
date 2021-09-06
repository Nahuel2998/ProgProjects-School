using System;
using System.Collections.Generic;

class Propiedad
{
    #region Funciones Estaticas
    public static List<Propiedad> FiltrarDisponibilidad(List<Propiedad> propiedades, bool status = true)
    {
        List<Propiedad> res = new List<Propiedad>();
        foreach (Propiedad p in propiedades)
        {
            if (p.Disponible == status)
            {
                res.Add(p);
            }
        }
        return res;
    }

    // TODO: Filtrar habitaciones.

    public static double SumarAreas(List<Dimensiones> dimensiones)
    {
        double res = 0;
        foreach (Dimensiones d in dimensiones)
        {
            res += d.getArea();
        }
        return res;
    }
    #endregion

    #region Atributos
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    private bool _disponible;
    public bool Disponible
    {
        get => _disponible;
        set => _disponible = value;
    }
    private List<Dimensiones> _habitaciones;
    public List<Dimensiones> Habitaciones
    {
        get => _habitaciones;
        set => _habitaciones = value;
    }

    // TODO: Crear clase Pisos que almacene habitaciones?

    #endregion

    #region Constructores
    public Propiedad(int xId = -1, bool xDisponible = true)
    {
        this._id = xId; 
        this._disponible = xDisponible;
        this._habitaciones = new List<Dimensiones>();
    }
    #endregion
    
    #region Metodos

    // FIXME: Return only a few digits after a comma.
    public double getArea()
    {
        return SumarAreas(Habitaciones);
    }

    public int getCantidadHabitaciones()
    {
        return Habitaciones.Count;
    }

    public string obtenerHabitaciones(bool indices = false)
    {
        string res = "";
        for (int i = 0; i < Habitaciones.Count; i++)
        {
            res += $"\n{(indices ? $"{i} : " : "")}{Habitaciones[i].getString()}";
        }
        return (res.Length > 0) ? res.Remove(0, 1) : "No hay habitaciones.";
    }

    public string obtenerDatos()
    {
        return $"ID: {this.Id}\nDisponible: {this.Disponible}\nArea: {this.getArea()}m^3\nHabitaciones: {this.getCantidadHabitaciones()}\n{this.obtenerHabitaciones()}";
    }
    #endregion        
}

class Dimensiones
{
    double x;
    double y;
    double z;

    public Dimensiones(double _x = 0, double _y = 0, double _z = 0)
    {
        this.x = _x;
        this.y = _y;
        this.z = _z;
    }

    // FIXME: Return only a few digits after a comma.
    public double getArea()
    {
        return x*y*z;
    }

    public string getString()
    {
        return $"{x}x{y}x{z} - {this.getArea()}m^3";
    }
}
