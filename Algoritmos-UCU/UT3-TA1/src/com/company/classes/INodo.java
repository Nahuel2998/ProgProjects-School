package com.company.classes;

//interface INodo
//{
//    float getPrecio();
//    void setDato(Object dato);
//    void setSiguiente(INodo nodo);
//    INodo getSiguiente();
//    void imprimir();
//    void imprimirEtiqueta();
//    boolean equals(INodo unNodo);
//    Comparable getEtiqueta();
//    int compareTo(Comparable etiqueta);
//}


interface INodo
{
    float getPrecio();
    void setPrecio(float precio);
    String getId();
    String getNombre();
    void setNombre(String nombre);
    void setSiguiente(INodo nodo);
    INodo getSiguiente();
    INodo clonar();
    String imprimir(int labels, String separador);
    String imprimir(int labels);
    String imprimir();

    // @Override
    boolean equals(INodo nodo);

//    // @Override
//    int compareTo(Comparable etiqueta);
}
