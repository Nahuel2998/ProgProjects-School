package com.company;

public class Main
{
    public static void main(String[] args)
    {
        Multsuma ms = new Multsuma();

        System.out.println(ms.multsuma(1.0, 2.0, 3.0));

        System.out.println(ms.multsuma(1, Math.sin(Math.PI/4), Math.cos(Math.PI/4) / 2));
        System.out.println(ms.multsuma(1, Math.log(148), Math.log(296)));

        System.out.println("- - - - - -");

        System.out.println("4 = " + sumarParesSiPrimoSumarImparesSiNo(4));
        System.out.println("5 = " + sumarParesSiPrimoSumarImparesSiNo(15));
    }

    public static long sumarParesSiPrimoSumarImparesSiNo(long num)
    {
        long n = num / 2;
        return isPrime(num) ? n * (n + 1) : n * n;
    }

    public static boolean isPrime(long n)
    {
        boolean prime = true;
        long i = 3;
        while (i <= Math.sqrt(n))
        {
            if (n % i == 0)
            {
                prime = false;
                break;
            }
            i += 2;
        }
        return ((n % 2 != 0 && prime && n > 2) || n == 2);
    }
}
