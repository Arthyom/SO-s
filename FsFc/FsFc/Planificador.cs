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

        public Proceso[] planificarFcFs(Proceso [] pActual)
        {
            Proceso[] atendidos = new Proceso [ pActual.Length ];

            // procesar el proceso i-esimo y ponerlo en la cola de atendidos 
            for ( int i = 0; i < pActual.Length; i ++)
            {
                Proceso pSelec = pActual[i];
                // procesar hasta que el proceso termine 
                while (pSelec.faltate >= 3)
                {
                    pSelec.faltate -= 3;
                    pSelec.GSestado = 1;
                }

                pSelec.GSestado = 4;
                pSelec.faltate = 0;
                this.estado = 0;
            }

            return atendidos;

            
        }

        public Proceso[] planificarSjf(Proceso[] colaProcesos)
        {
            // buscar el proceso mas corto y procesarlo hasta que termine totalmente
            // poner su duracion en 0 y su estado en 4, listo
            // volver a buscar entre todos los procesos quue no tengan estado = 4
            Proceso[] nuevoOrden = new Proceso[colaProcesos.Length];
            int cont = 0;

            while ( colaProcesos.Length > cont)
            {
                //int faltantes = 0;

                Proceso Aux = colaProcesos[0];

                /* buscar procesos sin terminar
                foreach (Proceso p in colaProcesos)
                    if (p.GSestado != 4)
                        faltantes++;

                if (faltantes == 0)
                    return nuevoOrden;
                    */

                // buscar al mas corto de todos 
                int i;
                for (i = 0; i < colaProcesos.Length; i++)
                {
                    if (Aux.GSduracion > colaProcesos[i].GSduracion && colaProcesos[i].GSestado!= 4)
                        Aux = colaProcesos[i];
                }
                // buscar proceso mas corto y ponerlo a 4
                foreach (Proceso p in colaProcesos)
                    if (p.GSnombre == Aux.GSnombre)
                        p.GSestado = 4;



                // procesar al proceso mas corto
                while (Aux.faltate >= 3)
                {
                    Aux.faltate -= 3;
                    Aux.GSestado = 1;

                }
                Aux.GSestado = 4;
                nuevoOrden[cont] = Aux;   
                 
                cont++;


            }

            return nuevoOrden;
        }
    }
}