using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;
using System.Collections;

namespace FsFc
{


   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            gant.Titles.Add("Diagrama de Gant de Procesos");
            gant.ChartAreas["ChartArea1"].AxisY.Title = "Procesos";
            gant.ChartAreas["ChartArea1"].AxisX.Title = "Tiempo";
            

            this.button2.Text = "Procesar";
           
            

          
            button4.BackColor = Color.Red;
            button4.Text = "Borrar";
           

            
            // agregar elementos a la lista
            
            listView1.Columns.Add("1" ,"Nombre", listView1.Width / 7);
            listView1.Columns.Add("2", "To", listView1.Width / 7);
            listView1.Columns.Add("3", "Duracion", listView1.Width /7);            
            listView1.Columns.Add("4","Ti", listView1.Width / 7);
            listView1.Columns.Add("5", "Tf", listView1.Width / 7);
            listView1.Columns.Add("6", "Tr", listView1.Width / 7);
            listView1.Columns.Add("7", "Te", listView1.Width / 7);


            // fijar propiedades de la combobox
            comboBox1.Items.Add("FcFs");
            comboBox1.Items.Add("SJF");

            gGant.Text = " ";
           // gin.Text = " ";
            gStdcs.Text = " ";
            
            




        }

        private void chckOrdAl_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void Graficar ( Proceso ProcesoListo, int Yvalue, int acumulado)
        {
            // crear series 
            Series serie = gant.Series.Add(ProcesoListo.GSnombre);
            serie.ChartType = SeriesChartType.Point;

            for (int i = acumulado+1; i <= ProcesoListo.GSduracion + acumulado; i++)
                serie.Points.AddXY(i, Yvalue);


        }

        private void creatGrafico ( Proceso [] vectorProc )
        {
            

            // graficar el vector de procesos 
      

            int acumulado = 0;
            int anterior = 0;
            // graficar cada proceso en el vector
            for (int i = 0; i < vectorProc.Length; i++)
            {
                // agregar serie al grafico
                Series serie = gant.Series.Add(vectorProc[i].GSnombre);

                // cambiar tipo de grafico
                serie.ChartType = SeriesChartType.Point;
                int exp = acumulado  + 1 ;

                // poner todos los puntos en la grafica para una serie
                for (int j = (acumulado + 1); j <= (acumulado + vectorProc[i].GSduracion); j++)
                {
                    serie.Points.AddXY(j, i + 1);
                }
                acumulado += (vectorProc[i].GSduracion); 
            }
        }

        private void chkDurAl_CheckedChanged(object sender, EventArgs e)
        {
           // this.txtDurProc.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {



            string ruta = @"C:\Users\frodo\Desktop\procesos.txt";
            StreamReader lectr = new StreamReader(ruta);
            int numProc = Convert.ToInt16(lectr.ReadLine());
          
            // cola de usados 
            Queue usados = new Queue(numProc);

            // crear vector de procesos 
            Proceso[] vectProc = new Proceso[numProc];
            for (int i = 0; i < vectProc.Length; i++)
                vectProc[i] = new Proceso();

            // crear cola de procesos 
            Queue colaProc = new Queue(numProc);

            // crear un planificador 
            Planificador plnfcdr1 = new Planificador();

            // leer archivos y meterlos en el vector
            plnfcdr1.leerArchivo(vectProc);
            int llegada = vectProc[0].GSTiempoLLegada;
            lectr.Close();

            // decidir segund el algoritmo de planificacion
            switch (this.comboBox1.Text)
            {

                case "SJF":
                    int espera = 0;
                    int Tinicio = 1;
                    int Tfinal = 1;
                    int yValue = 1;
                    int cont = 0;
                    while ( cont < numProc  )
                     {
                        Proceso Procesado = plnfcdr1.PlanificarSJF(colaProc, vectProc, Tinicio,Tfinal, espera);
                        if (Procesado != null)
                        {
                            Graficar(Procesado, yValue, espera);
                            Tinicio += Procesado.GSTiempoLLegada;
                            Tfinal += Procesado.GSduracion;
                            espera += Procesado.GSduracion;

                            yValue++;
                            cont++;

                        }
                        Tfinal++; 
                      }                      
                break;

                case "FcFs":
                    espera = 0;
                    yValue = 1;
                    espera = 0;
                    foreach(Proceso p in vectProc)
                    {
                        colaProc.Enqueue(p);
                        Proceso elegido = (Proceso) colaProc.Peek();
                        colaProc.Dequeue();
                        Proceso Procesado = plnfcdr1.procesar(elegido, espera);
                        Graficar(Procesado, yValue, espera);
                        espera += Procesado.GSduracion;
                        yValue++;

                    }
                    break;
            }






       


                
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // limpiar elementos 
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control ce in c.Controls)
                    {


                        if (ce is TextBox)
                            ce.Text = " ";
                        else
                        {
                            if (ce is ListView)
                            {
                                listView1.Items.Clear();
                            }
                            else {
                                if (ce is Chart)
                                {
                                    foreach (Series s in gant.Series)
                                        s.Points.Clear();
                                    gant.Series.Clear();
                                    gant.Titles.Clear();

                                }
                            }
                        }
                    }  

                }

            }
        }
    }
}
