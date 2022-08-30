package org.example.inutil;

import org.jetbrains.annotations.NotNull;

// FIXME: yeah
public class TClasificador
{
	public static final int METODO_CLASIFICACION_INSERCION = 1;
	public static final int METODO_CLASIFICACION_SHELL = 2;
	public static final int METODO_CLASIFICACION_BURBUJA = 3;
    public static final int METODO_CLASIFICACION_QUICKSORT = 4;

	/**
	 * Punto de entrada al clasificador
	 * param metodoClasificacion
	 * param orden
	 * param tamanioVector
	 * return Un vector del tam. solicitado, ordenado por el algoritmo solicitado
	 */
	public int[] clasificar(int[] datosParaClasificar, int metodoClasificacion)
	{
		switch (metodoClasificacion)
		{
			case METODO_CLASIFICACION_INSERCION: return ordenarPorInsercion(datosParaClasificar);
			case METODO_CLASIFICACION_SHELL: return ordenarPorShell(datosParaClasificar);
			case METODO_CLASIFICACION_BURBUJA: return ordenarPorBurbuja(datosParaClasificar);
            case METODO_CLASIFICACION_QUICKSORT: return ordenarPorQuickSort(datosParaClasificar);
			default:
				System.err.println("Este codigo no deberia haberse ejecutado.");
				break;
		}
		return datosParaClasificar;
	}

	private void intercambiar(int @NotNull [] vector, int pos1, int pos2)
	{
		int temp = vector[pos2];
		vector[pos2] = vector[pos1];
		vector[pos1] = temp;
	}

	/**
	 * param datosParaClasificar
	 * return
	 */
	private int[] ordenarPorShell(int[] datosParaClasificar)
    {
        int j, inc;
        int[] incrementos = new int[] { 3223, 358, 51, 10, 3, 1 };

        for (int posIncrementoActual = 1; posIncrementoActual < incrementos.length; posIncrementoActual++)
        {
            inc = incrementos[posIncrementoActual];
            if (inc < (datosParaClasificar.length / 2))
            {
                for (int i = inc; i < datosParaClasificar.length; i++)
                {
                    j = i - inc;
                    while (j >= 0)
                    {
                        if (datosParaClasificar[j] > datosParaClasificar[j + inc])
                        {
                            intercambiar(datosParaClasificar, j, j + inc);
                            j = j--;
                        }
                    }
                }
            }
        }
        return datosParaClasificar;
    }

    /**
     * param datosParaClasificar
     * return
     */
    private int @NotNull [] ordenarPorInsercion(int @NotNull [] datosParaClasificar)
    {
        for (int i = 1; i < datosParaClasificar.length; i++)
        {
            int j = i - 1;
            while ((j >= 0) && (datosParaClasificar[j+1] > datosParaClasificar[j]))
            {
                intercambiar(datosParaClasificar, j, j + 1);
                j--;
            }
        }
        return datosParaClasificar;
    }

    private int @NotNull [] ordenarPorBurbuja(int @NotNull [] datosParaClasificar)
    {
        int n = datosParaClasificar.length - 1;
        for (int i = 0; i <= n; i++)
        {
            for (int j = n; j >= (i + 1); j--)
            {
                if (datosParaClasificar[j] < datosParaClasificar[j - 1])
                { intercambiar(datosParaClasificar, j - 1, j); }
            }
        }
        return datosParaClasificar;
    }

	protected int[] ordenarPorQuickSort(int[] datosParaClasificar)
    {
		quicksort(datosParaClasificar, 0, datosParaClasificar.length - 1);
		return datosParaClasificar;
	}

	private void quicksort(int[] entrada, int i, int j)
    {
		int izquierda = i;
		int derecha = j;

		int posicionPivote = encuentraPivote(izquierda, derecha, entrada);
		if (posicionPivote >= 0)
        {
            while (izquierda <= derecha)
            {
				while ((entrada[izquierda] < posicionPivote) && (izquierda < j))
                { izquierda--; }

				while ((posicionPivote < entrada[derecha]) && (derecha > i))
                { derecha++; }

				if (izquierda <= derecha)
                {
					intercambiar(entrada, derecha, izquierda);
					izquierda++;
					derecha--;
				}
			}

			if (i < derecha)
            { quicksort(entrada, i, izquierda); }
			if (izquierda < j)
            { quicksort(entrada, derecha, j); }
		}
	}

    private int encuentraPivote(int izquierda, int derecha, int @NotNull [] entrada)
    { return entrada[(izquierda + derecha) / 2]; }
}
