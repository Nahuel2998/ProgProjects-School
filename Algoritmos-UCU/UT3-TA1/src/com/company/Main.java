package com.company;

import com.company.classes.Lista;
import com.company.classes.Nodo;

public class Main {
    public static void main(String[] args)
    {
        Lista lista = new Lista();

        lista.insertarFinal(new Nodo("xx14"));
        lista.insertarFinal(new Nodo("at2"));
        lista.insertarFinal(new Nodo("093"));
        lista.insertarFinal(new Nodo("n"));
        lista.insertarFinal(new Nodo("nu"));
        lista.insertarFinal(new Nodo("na"));

        System.out.println(lista.imprimir());
        System.out.println(lista.cantElementos());

        lista.ordenar();
        System.out.println(lista.imprimir());
        System.out.println(lista.cantElementos());
    }
}
