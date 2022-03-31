package com.company;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.regex.Pattern;

public class ContadorPalabras {
    // Too slow
    public static long contarPalabras(String frase)
    { return Pattern.compile("[^ ]+").matcher(frase).results().count(); }

    public static int contarPalabrasJusto(String frase)
    {
        int res = 0;
        boolean palabra = false;
        for (int i = 0; i < frase.length(); i++)
        {
            if (frase.charAt(i) == ' ')
            { palabra = false; }
            else if (!palabra)
            {
                palabra = true;
                res++;
            }
        }
        return res;
    }

    public static int contarPalabrasJusto(String[] frases)
    {
        int res = 0;
        for (String frase : frases)
        { res += contarPalabrasJusto(frase); }
        return res;
    }

    public static int contarPalabrasLargasJusto(String frase, int largoEsperado)
    {
        int res = 0;
        int largoPalabra = 0;
        for (int i = 0; i < frase.length(); i++)
        {
            if (frase.charAt(i) == ' ')
            {
                if (largoPalabra > largoEsperado)
                { res++; }
                largoPalabra = 0;
                continue;
            }
            largoPalabra++;
        }
        return largoPalabra > largoEsperado ? ++res : res;
    }

    public static int contarOcurrenciasDeLetraJusto(String frase, char letra)
    {
        int res = 0;
        for (int i = 0; i < frase.length(); i++)
        {
            if (frase.charAt(i) == letra)
            { res++; }
        }
        return res;
    }

    // Devuelve un array con el formato {cantVocales, cantConsonantes} (Slower but shorter)
    public static int[] contarVocalesYConsonantes(String frase)
    { return new int[]{ frase.replaceAll("[^aáeéiíoóuúAÁEÉIÍOÓUÚ]", "").length(),
            frase.replaceAll("[^b-df-hj-np-tv-zñB-DF-HJ-NP-TV-ZÑ]", "").length() }; }

//    // Devuelve un array con el formato {cantVocales, cantConsonantes}
//    public static int[] contarVocalesYConsonantesJusto(String frase)
//    {
//        int[] res = new int[]{0, 0};
//        for (int i = 0; i < frase.length(); i++)
//        {
//            if (esConsonante(frase.charAt(i)))
//            { res[1]++; }
//            else if (esVocal(frase.charAt(i)))
//            { res[0]++; }
//        }
//        return res;
//    }

    // Devuelve un array con el formato {cantVocales, cantConsonantes} (Even faster!)
    public static int[] contarVocalesYConsonantesJusto2(String frase)
    {
        int[] res = new int[]{0, 0};
        for (int i = 0; i < frase.length(); i++)
        {
            if (Character.isLetter(frase.charAt(i)))
            { res[esVocal(frase.charAt(i)) ? 0 : 1]++; }
        }
        return res;
    }

    public static boolean esVocal(char c)
    {
        return switch (Character.toLowerCase(c)) {
            case 'a', 'e', 'i', 'o', 'u', 'á', 'é', 'í', 'ó', 'ú' -> true;
            default -> false;
        };
    }

    public static boolean esConsonante(char c)
    { return Character.isLetter(c) && !esVocal(c); }

    public static String[] palabrasComunes(String[] palabras1, String[] palabras2)
    {
        HashSet<String> res = new HashSet<>(List.of(palabras2));
//        ArrayList<String> res = new ArrayList<>(List.of(palabras2));
        res.retainAll(new HashSet<>(List.of(palabras1)));
        return res.toArray(new String[0]);
    }

    public static String[] palabrasComunesJusto(String[] palabras1, String[] palabras2)
    {
        HashSet<String> p1 = new HashSet<>(List.of(palabras1));
        ArrayList<String> res = new ArrayList<>();
        for (String s : palabras2)
        {
            if (p1.contains(s))
            { res.add(s); }
        }
        return res.toArray(new String[0]);
    }
}
