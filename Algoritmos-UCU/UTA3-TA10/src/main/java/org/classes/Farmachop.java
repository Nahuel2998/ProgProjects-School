package org.classes;

import asg.cliche.Command;
import asg.cliche.Param;

public class Farmachop
{
    private static final int IDENTIFICADOR = Nodo.ETIQUETA;
    private static final int DESCRIPCION = Nodo.DATO;
    private static final Farmachop instancia = new Farmachop();

    public ListaMejorada<String> sueros = new ListaMejorada<>(16);
    public ListaMejorada<String> farmacos = new ListaMejorada<>(256);
    public ListaMejorada<Integer> lista = new ListaMejorada<>(128);

    public static Farmachop getInstance()
    { return instancia; }

    public boolean esPreparadoViable(Integer suero, Integer... farmacos) throws IllegalArgumentException
    {
        // Si el suero no es valido, no
        if (!this.sueros.existe(suero))
        { throw new IllegalArgumentException(String.format("Suero '%d' no encontrado.", suero)); }

        for (Integer farmaco : farmacos)
        {
            // Si el farmaco no es valido, no
            // (Realmente no seria necesario ya que si no se encuentra en la lista blanca no nos interesa
            // y en dicha lista no deberia haber un preparado invalido, pero por las dudas)
            if (!this.farmacos.existe(farmaco))
            { throw new IllegalArgumentException(String.format("Farmaco '%d' no encontrado.", farmaco)); }

            // Obtener lista de incompatibilidades
            ILista<Integer, Integer> listaCompatibilidad = this.lista.buscarCada(farmaco);

            // El primer elemento de la lista representa que este se encuentra en la lista blanca
            // Si la lista es vacia, no se encuentra en la lista blanca, y no se puede usar
            if (listaCompatibilidad == null || listaCompatibilidad.esVacia())
            { return false; }

            // Si el suero se encuentra en la lista de incompatibles, no es viable
            if (listaCompatibilidad.existeDato(suero))
            { return false; }
        }
        // Si nada ha fallado hasta ahora, todo esta bien
        return true;
    }

    public String imprimirSuero(int identificador) throws IllegalArgumentException
    { return this.imprimirSuero(identificador, IDENTIFICADOR + DESCRIPCION); }

    public String imprimirSuero(int identificador, int labels) throws IllegalArgumentException
    {
        INodo<Integer, String> nodo = this.sueros.buscar(identificador);
        if (nodo == null)
        { throw new IllegalArgumentException(String.format("Suero '%d' no encontrado.", identificador)); }
        return nodo.imprimir(labels, "\t : ");
    }

    public String imprimirFarmaco(int identificador) throws IllegalArgumentException
    { return this.imprimirFarmaco(identificador, IDENTIFICADOR + DESCRIPCION); }

    public String imprimirFarmaco(int identificador, int labels) throws IllegalArgumentException
    {
        INodo<Integer, String> nodo = this.farmacos.buscar(identificador);
        if (nodo == null)
        { throw new IllegalArgumentException(String.format("Farmaco '%d' no encontrado.", identificador)); }
        return nodo.imprimir(labels, "\t : ");
    }

    public String imprimirSueros()
    { return this.imprimirSueros(IDENTIFICADOR + DESCRIPCION); }

    public String imprimirFarmacos()
    { return this.imprimirFarmacos(IDENTIFICADOR + DESCRIPCION); }

    public String imprimirSueros(int labels)
    { return this.sueros.imprimir(labels, "\n", "\t : "); }

    public String imprimirFarmacos(int labels)
    { return this.farmacos.imprimir(labels, "\n", "\t : "); }

    // <editor-fold desc="[Comandos]">
    @Command(description = "Checkear si un preparado es viable o no viable.")
    public void preparadoViable(@Param(name = "Suero", description = "Suero a usar.") Integer suero,
                                @Param(name = "Farmacos", description = "Farmacos a usar.") Integer... farmacos)
    {
        try
        { System.out.println(esPreparadoViable(suero, farmacos) ? "VIABLE" : "NO VIABLE"); }
        catch (IllegalArgumentException e)
        { System.out.println(e.getMessage()); }
    }

    @Command(description = "Obtener descripcion de un suero.")
    public void suero(@Param(name = "Identificador", description = "Identificador del suero.") int identificador)
    {
        try
        { System.out.println(imprimirSuero(identificador)); }
        catch (IllegalArgumentException e)
        { System.out.println(e.getMessage()); }
    }

    @Command(description = "Obtener descripcion de un farmaco.")
    public void farmaco(@Param(name = "Identificador", description = "Identificador del farmaco.") int identificador)
    {
        try
        { System.out.println(imprimirFarmaco(identificador)); }
        catch (IllegalArgumentException e)
        { System.out.println(e.getMessage()); }
    }

    @Command(description = "Imprime todos los sueros.")
    public void sueros()
    { System.out.println(this.imprimirSueros()); }

    @Command(description = "Imprime todos los farmacos.")
    public void farmacos()
    { System.out.println(this.imprimirFarmacos()); }
    // </editor-fold>
}