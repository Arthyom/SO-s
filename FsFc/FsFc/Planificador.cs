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
            pActual.faltate = 0;
            this.estado = 4;
        }
        public void planificarSjf(Proceso[] colaProcesos)
        {
            // buscar el proceso mas corto y procesarlo hasta que termine totalmente
            // poner su duracion en 0 y su estado en 4, listo
            // volver a buscar entre todos los procesos quue no tengan estado = 4
            while (true)
            {
                int faltantes = 0;

                Proceso Aux = colaProcesos[0];

                // buscar procesos sin terminar
                foreach (Proceso p in colaProcesos)
                    if (p.GSestado != 4)
                        faltantes++;

                if (faltantes == 0)
                    return;

                // buscar al mas corto de todos 
                for (int i = 0; i < colaProcesos.Length; i++)
                {
                    if (Aux.GSduracion > colaProcesos[i].GSduracion)
                        Aux = colaProcesos[i];
                }

                // procesar al proceso mas corto
                while (Aux.GSduracion >= 3)
                {
                    Aux.faltate -= 3;
                    Aux.GSestado = 1;

                }
                Aux.GSestado = 4;

            }

            

        }
    }
}