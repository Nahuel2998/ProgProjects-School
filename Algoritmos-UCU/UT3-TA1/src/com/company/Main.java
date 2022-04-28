package com.company;

import com.company.classes.Lista;
import com.company.classes.Nodo;

public class Main {
    public static void main(String[] args)
    {
        Lista lista = new Lista();

        lista.insertarFinal(new Nodo("xx14", "5aw", 0));
        lista.insertarFinal(new Nodo("at2", "1abbw", 0));
        lista.insertarFinal(new Nodo("093", "Zrauf", 0));
        lista.insertarFinal(new Nodo("n", "carT", 0));
        lista.insertarFinal(new Nodo("nu", "00000", 0));
        lista.insertarFinal(new Nodo("na", "00", 0));

        System.out.println(lista.imprimir());
        System.out.println(lista.cantElementos());

        lista.ordenar(Lista.NOMBRE);
        System.out.println(lista.imprimir(Nodo.NOMBRE));
        System.out.println(lista.cantElementos());

        lista.ordenar(Lista.ID);
        System.out.println(lista.imprimir());
        System.out.println(lista.cantElementos());
    }
}
