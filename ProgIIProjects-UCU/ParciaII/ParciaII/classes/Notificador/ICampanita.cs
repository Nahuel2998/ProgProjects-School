using System.Collections.Generic;
using ParciaII.classes.Alimento;
using ParciaII.classes.Cliente;

namespace ParciaII.classes.Notificador
{
    public interface ICampanita
    {
        // He usado Stack para hacer mas facil la parte de aniadir y quitar comida
        public Stack<IAlimento> Alimentos { get; }
        public IList<ICliente> Subs { get; }
        void NotificarSubs();
    }
}