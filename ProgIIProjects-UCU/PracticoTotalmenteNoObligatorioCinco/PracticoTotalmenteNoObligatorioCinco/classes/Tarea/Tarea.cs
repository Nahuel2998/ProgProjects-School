using System;

namespace PracticoTotalmenteNoObligatorioCinco.classes
{
    public class Tarea : ITarea
    {
        public string Nombre { get; }
        // -1: Rechazada
        // 0: No Aprobada
        // 1: Aprobada
        // 2: Terminada
        public short Estado { get; private set; }

        public Tarea(string nombre)
        {
            Nombre = nombre;
            Estado = 0;
        }

        public void Terminar()
        {
            if (Estado != 1)
            {
                throw Estado switch
                {
                    2 => new TareaAprobadaException(),
                    < 1 => new TareaRechazadaException(),
                    _ => new whatException()
                };
            }

            Estado = 2;
        }

        public void Aprobar() => CambiarEstado(1);
        public void Rechazar() => CambiarEstado(-1);

        // Move this to Admin?
        // yes-- actually no, I tried
        private void CambiarEstado(short nuevoEstado)
        {
            if (Estado != 0)
            {
                throw Estado switch
                {
                    > 0 => new TareaAprobadaException(),
                    -1 => new TareaRechazadaException(),
                    _ => new whatException()
                };
            }

            Estado = nuevoEstado;
        }
    }

    public class TareaRechazadaException : Exception
    { }
    
    public class TareaAprobadaException : Exception
    { }
    
    public class whatException : Exception
    {
        public whatException() : base("what")
        { }
    }
}