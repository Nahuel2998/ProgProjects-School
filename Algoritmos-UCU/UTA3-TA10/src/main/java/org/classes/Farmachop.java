package org.classes;

import asg.cliche.Command;
import org.jetbrains.annotations.NotNull;

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

    @Command
    public boolean preparadoViable(Integer suero, Integer @NotNull [] farmacos)
    {
        // Si el suero no es valido, no
        if (!this.sueros.existe(suero))
        { return false; }

        for (Integer farmaco : farmacos)
        {
            // Si el farmaco no es valido, no
            // (Realmente no seria necesario ya que si no se encuentra en la lista blanca no nos interesa
            // y en dicha lista no deberia haber un preparado invalido, pero por las dudas)
            if (!this.farmacos.existe(farmaco))
            { return false; }

            // Obtener lista de incompatibilidades
            ILista<Integer, Integer> listaCompatibilidad = this.lista.buscarCada(farmaco);

            // El primer elemento de la lista representa que esta en la lista blanca
            // Si la lista es vacia, no esta en la lista blanca, y no se puede usar
            if (listaCompatibilidad.esVacia())
            { return false; }

            // Si el suero se encuentra en la lista de incompatibles, no es viable
            if (listaCompatibilidad.existeDato(suero))
            { return false; }
        }
        // Si nada ha fallado hasta ahora, todo esta bien
        return true;
    }

    @Command
    public String imprimirSuero(int etiqueta) throws IllegalArgumentException
    { return this.imprimirSuero(etiqueta, IDENTIFICADOR + DESCRIPCION); }

    public String imprimirSuero(int etiqueta, int labels) throws IllegalArgumentException
    {
        INodo<Integer, String> nodo = this.sueros.buscar(etiqueta);
        if (nodo == null)
        { throw new IllegalArgumentException("Etiqueta invalida."); }
        return nodo.imprimir(labels, " : ");
    }

    @Command
    public String imprimirFarmaco(int etiqueta) throws IllegalArgumentException
    { return this.imprimirFarmaco(etiqueta, IDENTIFICADOR + DESCRIPCION); }

    public String imprimirFarmaco(int etiqueta, int labels) throws IllegalArgumentException
    {
        INodo<Integer, String> nodo = this.farmacos.buscar(etiqueta);
        if (nodo == null)
        { throw new IllegalArgumentException("Etiqueta invalida."); }
        return nodo.imprimir(labels, " : ");
    }

    @Command
    public String imprimirSueros()
    { return this.imprimirSueros(IDENTIFICADOR + DESCRIPCION); }

    @Command
    public String imprimirFarmacos()
    { return this.imprimirFarmacos(IDENTIFICADOR + DESCRIPCION); }

    public String imprimirSueros(int labels)
    { return this.sueros.imprimir(labels, "\n", " : "); }

    public String imprimirFarmacos(int labels)
    { return this.farmacos.imprimir(labels, "\n", " : "); }
}
