import java.util.LinkedList;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Ernesto
 */
public interface IVertice<C extends Comparable<C>, T>
{
    TAdyacencia<C, T> buscarAdyacencia(TVertice<C, T> verticeDestino);

    TAdyacencia<C, T> buscarAdyacencia(C etiquetaDestino);

    boolean eliminarAdyacencia(C nomVerticeDestino);

    LinkedList<TAdyacencia<C, T>> getAdyacentes();

    boolean insertarAdyacencia(Double costo, TVertice<C, T> verticeDestino);

    Double obtenerCostoAdyacencia(TVertice<C, T> verticeDestino);

    TVertice<C, T> primerAdyacente();

    TVertice<C, T> siguienteAdyacente(TVertice<C, T> w);

    boolean existeAdyacencia(C etiquetaDestino);

    boolean existeAdyacencia(TVertice<C, T> verticeDestino);
}
