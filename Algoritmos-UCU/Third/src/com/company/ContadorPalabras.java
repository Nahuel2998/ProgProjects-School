package com.company;

import java.util.regex.Pattern;

public class ContadorPalabras {
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
}
