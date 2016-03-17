using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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

        public Proceso[] planificarSJF(Proceso[] vectorProc)
        {
            // declarar una coleccion generica 
            Proceso[] listaSalida = new Proceso[vectorProc.Length];
            Proceso min = vectorProc[0];

            for ( int k = 0;  k < vectorProc.Length; k ++)
            {
                // buscar el minimo para cada elemtneto del vector 
                for (int i = 0; i < vectorProc.Length; i++)
                {
                    min = vectorProc[i];
                    // verifiar que el elemnto exista
                    if (vectorProc[i].GSestado != 4)
                    {
                        // compararlo con todo el vector buscando el minimo 
                        for (int j = 0; j < vectorProc.Length; j++)
                        {
                            // comparar min con V[j] siempre y cuando V[j] exista 
                            if ((min.GSduracion > vectorProc[j].GSduracion) && vectorProc[j].GSestado != 4)
                                min = vectorProc[j];
                        }

                        break;

                    }
                }

               

                // buscar el elemento minimo en el vector y eliminarlo
                for (int i = 0; i < vectorProc.Length; i++)
                {
                    if (vectorProc[i].GSnombre.CompareTo(min.GSnombre) == 0)
                    {
                        vectorProc[i].GSestado = 4;
                        min.GSestado = 4;
                        break;
                    }
                }

                // procesar el proceso minimo 
                while (min.faltate >= 3)
                {
                    min.faltate -= 3;
                    min.faltate = 1;

                }
                min.GSestado = 4;

                // meter proceso en el vector de listos
                listaSalida[k] = min;
            }
            return listaSalida;
        }

        
    }
}