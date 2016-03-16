using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsFc
{
    public class Planificador
    {
        /* 1 ocupado, 0 libre */
        private int estado;
        public int GSestado
        {
            set
            {
                this.estado = value;
            }
            get
            {
                return this.estado;
            }
        }
        public int rafagaCpu = 3;

        public void planificarFcFs(Proceso pActual)
        {
            // procesar hasta que el proceso termine 
            while (pActual.faltate >= 3)
            {
                pActual.faltate -= 3;
                pActual.GSestado = 1;
            }

            pActual.GSestado = 4;
        }
    }
}