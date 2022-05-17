package org.classes;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.main.Main;

import static org.junit.jupiter.api.Assertions.*;

class FarmachopTest
{
    @BeforeAll
    static void cargarDatos()
    { Main.cargarDatos(); }

    @Test
    void esPreparadoViable()
    {
        // Todos los elementos estan en lista blanca y son compatibles
        assertTrue(Farmachop.getInstance().esPreparadoViable(13, 4, 6, 10));

        // 13 no es compatible con 1
        assertFalse(Farmachop.getInstance().esPreparadoViable(13, 4, 6, 1));

        // 19 no es un suero valido
        assertThrowsExactly(IllegalArgumentException.class,
                () -> Farmachop.getInstance().esPreparadoViable(19, 4, 6),
                "Suero '19' no encontrado.");

        // 670 no es un farmaco valido
        assertThrowsExactly(IllegalArgumentException.class,
                () -> Farmachop.getInstance().esPreparadoViable(13, 4, 670, 10),
                "Farmaco '670' no encontrado.");
    }

    @Test
    void imprimirSuero()
    {
        // 4 existe
        assertEquals("4\t : RINGER CON LACTATO SOL. X 500ML", Farmachop.getInstance().imprimirSuero(4));

        // 50 no es un suero valido
        assertThrowsExactly(IllegalArgumentException.class,
                () -> Farmachop.getInstance().imprimirSuero(50),
                "Suero '50' no encontrado.");
    }

    @Test
    void imprimirFarmaco()
    {
        // 221 existe
        assertEquals("221\t : MELFALANO 550 MG", Farmachop.getInstance().imprimirFarmaco(221));

        // 921 no es un suero valido
        assertThrowsExactly(IllegalArgumentException.class,
                () -> Farmachop.getInstance().imprimirFarmaco(921),
                "Farmaco '921' no encontrado.");
    }
}