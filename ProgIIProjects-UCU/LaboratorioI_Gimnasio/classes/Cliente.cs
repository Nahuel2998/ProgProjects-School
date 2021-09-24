
public class Cliente 
{
    public int NumeroSocio { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double[] Pagos { get; set; }
    public string EjercicioActual { get; set; }
    // public Tuple<string, TimeSpan> EjercicioActual { get; set; }

    public Cliente (int xNumeroSocio, string xNombre, string xApellido, double xPeso, double xAltura)
    {
        this.NumeroSocio = xNumeroSocio;
        this.Nombre = xNombre;
        this.Apellido = xApellido;
        this.Peso = xPeso;
        this.Altura = xAltura;
        this.Pagos = new double[12];
    }

    public void PagarCuota (int mes, double cantidad)
    {
        this.Pagos[mes - 1] = cantidad;
    }
    
    public bool Deuda(int mes)
    {
        return this.Pagos[mes - 1] == 0;
    }

    public void Ejercitarse (string ejercicio)
    {
        this.EjercicioActual = ejercicio;
    }

    public double getIMC()
    {
        return this.Peso / (this.Altura*this.Altura);
    }

    public string getIMCCategoria()
    {
        string res = "";
        switch (this.getIMC())
        {
            case < 18.5:
                res = "Bajo Peso";
                break;
            case < 25:
                res = "Normal";
                break;
            case < 30:
                res = "Sobrepeso";
                break;
            case < 35:
                res = "Obesidad I";
                break;
            case < 40:
                res = "Obesidad II";
                break;
            case < 50:
                res = "Obesidad III";
                break;
            default:
                res = "Obesidad IV";
                break;
        }
        return res;
    }
    
    public override string ToString()
    {
        return $"Numero de Socio: {this.NumeroSocio}\nNombre: {this.Nombre}\nApellido: {this.Apellido}";
    }
}
