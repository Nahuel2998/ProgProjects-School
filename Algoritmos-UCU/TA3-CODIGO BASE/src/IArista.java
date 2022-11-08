/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Ernesto
 */
public interface IArista<C extends Comparable<C>>
{
    double getCosto();

    C getEtiquetaDestino();

    C getEtiquetaOrigen();

    void setCosto(double costo);

    void setEtiquetaDestino(C etiquetaDestino);

    void setEtiquetaOrigen(C etiquetaOrigen);
}
