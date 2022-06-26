package org.classes;

import org.jetbrains.annotations.NotNull;

public class ElementoAB<K extends Comparable<K>, T> implements IElementoAB<K, T>
{
    private K etiqueta;
    private T datos;
    private int altura = 1;
    private IElementoAB<K, T> hijoIzquierdo;
    private IElementoAB<K, T> hijoDerecho;

    public ElementoAB(K etiqueta)
    { this.etiqueta = etiqueta; }

    public ElementoAB(K etiqueta, T datos)
    {
        this.etiqueta = etiqueta;
        this.datos = datos;
    }

    @Override
    public K getEtiqueta()
    { return this.etiqueta; }

    public void setEtiqueta(K etiqueta)
    { this.etiqueta = etiqueta; }

    @Override
    public IElementoAB<K, T> getHijoIzq()
    { return hijoIzquierdo; }

    @Override
    public void setHijoIzq(IElementoAB<K, T> hijoIzquierdo)
    { this.hijoIzquierdo = hijoIzquierdo; }

    @Override
    public IElementoAB<K, T> getHijoDer()
    { return hijoDerecho; }

    @Override
    public void setHijoDer(IElementoAB<K, T> hijoDerecho)
    { this.hijoDerecho = hijoDerecho; }

    @Override
    public T getDatos()
    { return datos; }

    @Override
    public void setDatos(T datos)
    { this.datos = datos; }

    @Override
    public boolean insertar(@NotNull IElementoAB<K, T> elemento)
    {
        if (this.etiqueta.compareTo(elemento.getEtiqueta()) == 0)
//        if (etiqueta == elemento.getEtiqueta())
        { return false; }

        if (this.etiqueta.compareTo(elemento.getEtiqueta()) > 0)
//        if (etiqueta > elemento.getEtiqueta())
        {
            if (this.hijoIzquierdo == null)
            {
                this.hijoIzquierdo = elemento;
                return true;
            }
            return this.hijoIzquierdo.insertar(elemento);
        }

        if (this.hijoDerecho == null)
        {
            this.hijoDerecho = elemento;
            return true;
        }

        this.actualizarAltura();
        return this.hijoDerecho.insertar(elemento);
    }

    @Override
    public IElementoAB<K, T> buscar(@NotNull K etiqueta)
    {
//        if (etiqueta.equals(this.etiqueta))
//        { return this; }
//        if (etiqueta.compareTo(this.etiqueta) < 0)
//        { return this.getHijoIzq() != null ? this.getHijoIzq().buscar(etiqueta) : null; }
//        return this.getHijoDer() != null ? this.getHijoDer().buscar(etiqueta) : null;

        return etiqueta.equals(this.etiqueta) ?
                this :
                etiqueta.compareTo(this.etiqueta) < 0 ?
                        this.getHijoIzq() != null ?
                                this.getHijoIzq().buscar(etiqueta) :
                                null :
                        this.getHijoDer() != null ?
                                this.getHijoDer().buscar(etiqueta) :
                                null;
    }

    @Override
    public int obtenerNivel(@NotNull K etiqueta, int nivel)
    {
        return etiqueta.equals(this.etiqueta) ?
                nivel :
                etiqueta.compareTo(this.etiqueta) < 0 ?
                        this.getHijoIzq() != null ?
                                this.getHijoIzq().obtenerNivel(etiqueta, nivel + 1) :
                                -1 :
                        this.getHijoDer() != null ?
                                this.getHijoDer().obtenerNivel(etiqueta, nivel + 1) :
                                -1;
    }

    @Override
    public int obtenerNivel(@NotNull K etiqueta)
    { return obtenerNivel(etiqueta, 1); }

//    @Override
//    public int obtenerAltura()
//    {
//        return this.getHijoIzq() == null && this.getHijoDer() == null ?
//                1 :
//                this.getHijoIzq() == null ?
//                        this.getHijoDer().obtenerAltura() + 1 :
//                        this.getHijoDer() == null ?
//                                this.getHijoIzq().obtenerAltura() + 1 :
//                                Math.max(this.getHijoIzq().obtenerAltura(), this.getHijoDer().obtenerAltura()) + 1;
//    }

    @Override
    public int obtenerAltura()
    { return this.altura; }

    public void actualizarAltura()
    { this.altura = 1 + Math.max(obtenerAlturaHijoIzq(), obtenerAlturaHijoDer()); }

    @Override
    public int obtenerAlturaHijoDer()
    { return this.getHijoDer() == null ? 0 : this.getHijoDer().obtenerAltura(); }

    @Override
    public int obtenerAlturaHijoIzq()
    { return this.getHijoIzq() == null ? 0 : this.getHijoIzq().obtenerAltura(); }

    @Override
    public int obtenerBalance()
    { return this.obtenerAlturaHijoDer() - this.obtenerAlturaHijoIzq(); }

    @Override
    public IElementoAB<K, T> eliminar(@NotNull K etiqueta)
    {
        byte compareRes = (byte) etiqueta.compareTo(this.getEtiqueta());
        if (compareRes < 0)
        {
            if (this.getHijoIzq() != null)
            { this.setHijoIzq(this.getHijoIzq().eliminar(etiqueta)); }
            return this;
        }
        if (compareRes > 0)
        {
            if (this.getHijoDer() != null)
            { this.setHijoDer(this.getHijoDer().eliminar(etiqueta)); }
            return this;
        }
        this.actualizarAltura();
        return this.quitarNodo();
    }

    @Override
    public IElementoAB<K, T> quitarNodo()
    {
        if (this.getHijoIzq() == null)
        { return this.getHijoDer(); }

        if (this.getHijoDer() == null)
        { return this.getHijoIzq(); }

        IElementoAB<K, T> elPadre = this;
        IElementoAB<K, T> elHijo = this.getHijoIzq();

        while (elHijo.getHijoDer() != null)
        {
            elPadre = elHijo;
            elHijo = elHijo.getHijoDer();
        }

        if (elPadre != this)
        {
            if (elHijo.getHijoIzq() != null)
            { elPadre.setHijoDer(elHijo.getHijoIzq()); }

            elHijo.setHijoIzq(this.getHijoIzq());
        }

        elHijo.setHijoDer(this.getHijoDer());
        this.setHijoDer(null);
        this.setHijoIzq(null);

        return elHijo;
    }

    @Override
    public IElementoAB<K, T> obtenerHojaIzquierda()
    {
        IElementoAB<K, T> aux = this;

        while (!aux.esHoja())
        { aux = aux.getHijoIzq(); }

        return aux;
    }

    @Override
    public String inOrden()
    { return inOrden("-"); }

    @Override
    public void inOrden(ILista<K, T> unaLista)
    {
        if (this.hijoIzquierdo != null)
        { this.hijoIzquierdo.inOrden(unaLista); }

        unaLista.insertar(new Nodo<>(this.etiqueta, this.datos));

        if (this.hijoDerecho != null)
        { this.hijoDerecho.inOrden(unaLista); }
    }

    @Override
    public String inOrden(String separador)
    {
        String res = "";

        if (this.hijoIzquierdo != null)
        { res = this.hijoIzquierdo.inOrden(separador); }

        res += this.getEtiqueta();
        res += separador;

        if (this.hijoDerecho != null)
        { res += this.hijoDerecho.inOrden(separador); }

        return res.substring(0, res.length() - separador.length());
    }

    @Override
    public String preOrden()
    {
        String separador = ", ";
        StringBuilder res = this.preOrden(separador);
        res.setLength(res.length() - separador.length());
        return res.toString();
    }

    @Override
    public StringBuilder preOrden(String separador)
    {
        StringBuilder res = new StringBuilder();

        res.append(this.getEtiqueta());
        res.append(separador);

        if (this.hijoIzquierdo != null)
        { res.append(this.hijoIzquierdo.preOrden(separador)); }

        if (this.hijoDerecho != null)
        { res.append(this.hijoDerecho.preOrden(separador)); }

        return res;
    }

    @Override
    public boolean esHoja()
    { return this.getHijoIzq() == null && this.getHijoDer() == null; }
}
