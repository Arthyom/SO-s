using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows.Forms;

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

        public Proceso procesar( Proceso pActual)
        {
              


            Proceso pSelec = pActual;
                // procesar hasta que el proceso termine 
                while (pSelec.faltate >= 3)
                {
                    pSelec.faltate -= 3;
                    pSelec.GSestado = 1;
                }

                //pSelec.tEspera = espera + 1;
                pSelec.GSestado = 4;
                pSelec.faltate = 0;
                this.estado = 0;
                //espera += pSelec.GSduracion ;

            return pActual;
        }

        public Proceso[] planificarFcFs(Proceso [] pActual)
        {
            Proceso[] atendidos = new Proceso [ pActual.Length ];
            int espera = 0;
            

            // procesar el proceso i-esimo y ponerlo en la cola de atendidos 
            for (int i = 0; i < pActual.Length; i++)
            {
                atendidos[i] =  procesar(pActual[i]);

                atendidos[i].tEspera = espera + 1;
                espera += atendidos[i].GSduracion;
            }
              

            return atendidos;

            
        }

        public Proceso[] planificarSJF(Proceso[] vectorProc)
        {
            // declarar una coleccion generica 
            Proceso[] listaSalida = new Proceso[vectorProc.Length];
            Proceso min = vectorProc[0];
            int espera = 0;

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
             
                listaSalida[k] = procesar(min);
                listaSalida[k].tEspera = espera + 1;
                espera += listaSalida[k].GSduracion;
            }
            return listaSalida;
        }

        public void leerArchivo( Proceso [] vectorProceso)
        {
            // leer procesos en el archivo

            // crear un lector de archivo 
            string rutaArchivo = @"C:\Users\frodo\Desktop\procesos.txt";
            StreamReader lector = new StreamReader(rutaArchivo);

            // leer dato del archivo de text linea a linea 
            string linea;
            int[] indxEspacios = new int [3];

            string nombre;
            int duracion;
            int inicio;
            int j = 0;
            int k = 0;
            // desplazar la primera linea 
            lector.ReadLine();
            while ( k < vectorProceso.Length )
            {
                linea = lector.ReadLine();
                j = 0;
                // buscar espacios 
                for (int i = 0; i < linea.Length; i++)
                {
                    if (linea[i] == ' ')
                    {
                        indxEspacios[j] = i;
                        j += 1;
                    }

                }

                nombre = linea.Substring(0, indxEspacios[0]);
                inicio = Convert.ToInt16( linea.Substring(indxEspacios[0] + 1, ( indxEspacios[1] - indxEspacios[0] ) -1 ) );
                duracion = Convert.ToInt16(linea.Substring(indxEspacios[1]));

                /// meter al vector
                vectorProceso[k].GSnombre = nombre;
                vectorProceso[k].GSduracion = duracion;
                vectorProceso[k].GSTiempoLLegada = inicio;
                k++;
            }

            lector.Close();
        } 
    }
}