package org.classes;

import org.jetbrains.annotations.NotNull;

public class Farmachop
{
    private static final Farmachop instancia = new Farmachop();

    public ListaMejorada<String> sueros = new ListaMejorada<>(16);
    public ListaMejorada<String> farmacos = new ListaMejorada<>(256);
    public ListaMejorada<Integer> lista = new ListaMejorada<>(128);

    public static Farmachop getInstance()
    { return instancia; }

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
}
