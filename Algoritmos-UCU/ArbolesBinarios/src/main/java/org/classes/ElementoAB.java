package org.classes;

public class ElementoAB<T>
        implements IElementoAB<T>
{
    private final int etiqueta;
    private T datos;
    private IElementoAB<T> hijoIzquierdo;
    private IElementoAB<T> hijoDerecho;

    public ElementoAB(int etiqueta)
    { this.etiqueta = etiqueta; }

    public ElementoAB(int etiqueta, T datos)
    {
        this.etiqueta = etiqueta;
        this.datos = datos;
    }

    @Override
    public int getEtiqueta()
    { return this.etiqueta; }

    @Override
    public IElementoAB<T> getHijoIzq()
    { return hijoIzquierdo; }

    @Override
    public void setHijoIzq(IElementoAB<T> hijoIzquierdo)
    { this.hijoIzquierdo = hijoIzquierdo; }

    @Override
    public IElementoAB<T> getHijoDer()
    { return hijoDerecho; }

    @Override
    public void setHijoDer(IElementoAB<T> hijoDerecho)
    { this.hijoDerecho = hijoDerecho; }

    @Override
    public T getDatos()
    { return datos; }

    @Override
    public boolean insertar(IElementoAB<T> elemento)
    {
//        if (this.etiqueta.compareTo(elemento.getEtiqueta()) == 0)
        if (etiqueta == elemento.getEtiqueta())
        { return false; }

//        if (this.etiqueta.compareTo(elemento.getEtiqueta()) > 0)
        if (etiqueta > elemento.getEtiqueta())
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
        return this.hijoDerecho.insertar(elemento);
    }

    public String inOrden()
    {
        String separador = ", ";
//        StringBuilder res = new StringBuilder();
//
//        if (this.hijoIzquierdo != null)
//        { res = new StringBuilder(this.hijoIzquierdo.inOrden()); }
//
//        res.append(getEtiqueta());
//        res.append(separador);
//
//        if (this.hijoDerecho != null)
//        {
//            res.append(this.hijoDerecho.inOrden());
//        }
//
//        return res.toString();
        StringBuilder res = this.inOrden(separador);
        res.setLength(res.length() - separador.length());
        return res.toString();
    }

    public StringBuilder inOrden(String separador)
    {
        StringBuilder res = new StringBuilder();

        if (this.hijoIzquierdo != null)
        { res = this.hijoIzquierdo.inOrden(separador); }

        res.append(getEtiqueta());
        res.append(separador);

        if (this.hijoDerecho != null)
        { res.append(this.hijoDerecho.inOrden(separador)); }

        return res;
    }
}
