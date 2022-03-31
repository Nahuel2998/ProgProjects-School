package com.company;

import java.util.Arrays;
import java.util.Scanner;

public class Main
{
    public static void main(String[] args) {
        Scanner s = new Scanner(System.in);
        System.out.println("Ingresar Frase:");
        String frase = s.nextLine();

        long start, end;

//        start = System.nanoTime();
        System.out.println(ContadorPalabras.contarPalabras(frase));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start) / 1e6);

//        start = System.nanoTime();
        System.out.println(ContadorPalabras.contarPalabrasJusto(frase));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start) / 1e6);

        int largoEsperado = 4;
//        start = System.nanoTime();
        System.out.println(ContadorPalabras.contarPalabrasLargasJusto(frase, largoEsperado));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start) / 1e6);

        char letra = 'a';
//        start = System.nanoTime();
        System.out.println(ContadorPalabras.contarOcurrenciasDeLetraJusto(frase, letra));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start) / 1e6);

//        start = System.nanoTime();
        System.out.println(Arrays.toString(ContadorPalabras.contarVocalesYConsonantes(frase)));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start) / 1e6);

//        start = System.nanoTime();
        System.out.println(Arrays.toString(ContadorPalabras.contarVocalesYConsonantesJusto2(frase)));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start) / 1e6);

        String[] p1 = {"This", "is", "a", "test"};
        String[] p2 = {"This", "was", "a", "test", "java.util.Arrays;"};

//        start = System.nanoTime();
        System.out.println(Arrays.toString(ContadorPalabras.palabrasComunes(p1, p2)));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start));

//        start = System.nanoTime();
        System.out.println(Arrays.toString(ContadorPalabras.palabrasComunesJusto(p1, p2)));
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start));

//        start = System.nanoTime();
        String[] frases = ManejadorArchivosGenerico.leerArchivo("C:\\Users\\nahue\\Documents\\Git\\ProgProjects-School\\Algoritmos-UCU\\Third\\src\\com\\company\\demo.txt");
//        end = System.nanoTime();
//        System.out.println("Tiempo: " + (end - start));

        System.out.println(ContadorPalabras.contarPalabrasJusto(frases));
    }
}
