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

        // conseguir las estadisticas de la cola 
        public float GetStdstcs ( Queue ColaProceso, int NumProc )
        {
            int TProm = 0;
            foreach (Proceso p in ColaProceso)
                TProm += p.tEspera;

            return TProm / NumProc;
        }

        // buscar al proceso mas corto en una cola de procesos
        public Proceso BuscarMenor ( Queue ColaProcesos )
        {

            if ( ColaProcesos.Count > 0)
            {
                Proceso procesoMenor = (Proceso)ColaProcesos.Peek();

                while (procesoMenor.GSestado == 4)
                {
                    procesoMenor =(Proceso) ColaProcesos.Peek();
                    if (procesoMenor.GSestado == 4)
                        ColaProcesos.Dequeue();

                }
                   


                if ( ColaProcesos.Count > 0)
                    procesoMenor = (Proceso)ColaProcesos.Peek();

                // iterar para cada elemento de la cola 
                foreach (Proceso p in ColaProcesos)
                {
                    if (p.GSduracion < procesoMenor.GSduracion && p.GSestado != 4)
                        procesoMenor = p;
                }

                return procesoMenor;
            }
            return null;
            
        }

        // encolar un intervalo 
        public void EncolarIntervalor ( Queue ColaProc, int Tinicio,int Tfin, Proceso [] VectorProc)
        {
            // encolar un intervalo de procesos y regresarlo por referencia
            foreach( Proceso p in VectorProc)
            {
                if (ColaProc.Contains(p)) continue;

                if ( p.GSTiempoLLegada >= Tinicio && p.GSTiempoLLegada <= Tfin && p.GSestado != 4)
                    ColaProc.Enqueue(p);
            }

        }
        
        // procesar un proceso hasta que se acabe
        public Proceso procesar( Proceso pActual, int espera )
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

            pActual.Tinicio = espera ;
            pActual.Tfinal = (pActual.GSduracion + pActual.Tinicio);

            pActual.tEspera = (pActual.Tinicio - pActual.GSTiempoLLegada)  ;
            pActual.Tfinal -= 1;
            
            espera += (pActual.GSduracion)-1;

            return pActual;
        }

        public Proceso[] planificarFcFs(Proceso [] pActual)
        {
            Proceso[] atendidos = new Proceso [ pActual.Length ];
            int espera = 1;
            

            // procesar el proceso i-esimo y ponerlo en la cola de atendidos 
            for (int i = 0; i < pActual.Length; i++)
            {
                atendidos[i] =  procesar(pActual[i],espera);
                espera += atendidos[i].GSduracion ;
            }
              

            return atendidos;

            
        }

        public Proceso[] PlanificarSJF(Proceso[] vectorProc)
        {
            // declarar una coleccion generica 
            Proceso[] listaSalida = new Proceso[vectorProc.Length];
            Proceso min = vectorProc[0];
            int espera = 1;

            for ( int k = 0; k < vectorProc.Length; k++)
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
             
                listaSalida[k] = procesar(min,espera);
                espera += listaSalida[k].GSduracion;
            }
            return listaSalida;
        }

        public void leerArchivo( Proceso [] vectorProceso,string ruta)
        {
            // leer procesos en el archivo

            // crear un lector de archivo 
            string rutaArchivo = ruta;
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

        public Queue EliminarProc ( Proceso ProcesoDelete, Queue ColaProcesos)
        {
            // buscar en cada proceso en la cola y eliminarlo
            Queue ColaCopia = new Queue(ColaProcesos.Count);
            foreach ( Proceso p in ColaProcesos)
            {
                if (p.GSnombre.CompareTo(ProcesoDelete.GSnombre) != 0)
                    ColaCopia.Enqueue(p);
            }
            return ColaCopia;
        }

        public Proceso PlanificarSJF ( Queue ColaProc, Proceso [] VectorProc, int TInicio, int TFinal, int TEspera)
        {

            // planificar con sjf 
            while (true)
            {
                // buscar procesos en un intervalo indicado  y encolarlos 
               this.EncolarIntervalor(ColaProc, TInicio , TFinal, VectorProc);

                // buscar al proceso menor en la cola 
                Proceso Menor = this.BuscarMenor(ColaProc);
                if (Menor != null)
                {
                    // procesar al menor y aumentar el rango de duracion
                    this.procesar(Menor, TEspera);

                    // eliminar el proceso de la cola 
                    ColaProc = this.EliminarProc(Menor, ColaProc);


                    return Menor;
                }
                return null;

                
                
            }
        }

        // planificar con algoritmo FsFc
        public Proceso PlanificarFcFs ( Queue ColaProc, int TEspera  )
        {
            Proceso elegido = (Proceso)ColaProc.Peek();
            ColaProc.Dequeue();
            Proceso Procesado = this.procesar(elegido, TEspera );
            return Procesado;
        }

    }

    
}