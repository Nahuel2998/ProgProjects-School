package com.company;

import java.util.Scanner;

public class Main {
    
    public static void main(String[] args) {
        Scanner s = new Scanner(System.in);
        System.out.println("Ingresar Frase:");
        String frase = s.nextLine();

        long start, end;

        start = System.nanoTime();
        System.out.println(ContadorPalabras.contarPalabras(frase));
        end = System.nanoTime();
        System.out.println("end - start = " + (end - start));

        start = System.nanoTime();
        System.out.println(ContadorPalabras.contarPalabrasJusto(frase));
        end = System.nanoTime();
        System.out.println("end - start = " + (end - start));
    }
}
