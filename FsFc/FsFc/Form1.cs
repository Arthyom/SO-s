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

        private void creatGrafico ( Proceso [] vectorProc )
        {
            

            // graficar el vector de procesos 
            gant.Titles.Add("Diagrama de Gant de Procesos");
            gant.ChartAreas["ChartArea1"].AxisY.Title = "Procesos";
            gant.ChartAreas["ChartArea1"].AxisX.Title = "Tiempo";

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
            int num = Convert.ToInt16(lectr.ReadLine());
            int numerProcesos = num;
            int tiempoEspera = 0;
            int tiempoProm = 0;
            Random r = new Random();
            Planificador pln1 = new Planificador();
          


            // si todo es automatico
            if ( true  )
            {
                
                Proceso[] vectProcesos = new Proceso[numerProcesos];
                for (int i = 0; i < vectProcesos.Length; i++)
                    vectProcesos[i] = new Proceso();
                int[] tiemposEspera = new int [numerProcesos];


                // leer datos de archivo de texto
                pln1.leerArchivo(vectProcesos);

                // verificar que proceso se trabaja
                switch (comboBox1.Text)
                    {
                        case "FcFs":
                            vectProcesos = pln1.planificarFcFs(vectProcesos);
                        break;

                        case "SJF":
                            vectProcesos = pln1.planificarSJF(vectProcesos);
                        break;
                    }


                //dar salida a la gui
                for (int i = 0; i < vectProcesos.Length; i++)
                {
                    // agregar al list view
                    listView1.View = View.Details;

                    ListViewItem it = new ListViewItem(vectProcesos[i].GSnombre);
                    it.SubItems.Add(vectProcesos[i].GSTiempoLLegada.ToString());
                    it.SubItems.Add(vectProcesos[i].GSduracion.ToString());
                    it.SubItems.Add(vectProcesos[i].Tinicio.ToString());
                    it.SubItems.Add(vectProcesos[i].Tfinal.ToString());
                    it.SubItems.Add(vectProcesos[i].Tretorno.ToString());
                    it.SubItems.Add(vectProcesos[i].tEspera.ToString());


                    // tiempo inicio, tiempo final, tiempo retorno, tiempo espera




                    listView1.Items.Add(it);

                    Thread.Sleep(400);
                    listView1.Refresh();

                    // conseguir tiempo promedio
                    tiempoProm += vectProcesos[i].tEspera;
                }

                float prom = (float)tiempoProm / vectProcesos.Length;
                lblPromedio.Text = prom.ToString("0.00");

                // crear grafico
                creatGrafico(vectProcesos);
                lectr.Close();
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
