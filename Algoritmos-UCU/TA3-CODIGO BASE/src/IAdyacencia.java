/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Ernesto
 */
public interface IAdyacencia<C extends Comparable<C>, T>
{
    double getCosto();

    TVertice<C, T> getDestino();

    C getEtiqueta();
}
