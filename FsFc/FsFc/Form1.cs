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
            this.gin.Enabled = false;
            // inicair elementos del formulario
            this.gin.Text = " ";
            this.button1.Text = " ->";
            button3.Text = ">>";
            this.button2.Text = "Procesar";

            this.lbProcesos.Text = " ";
            this.txtDurProc.BackColor = Color.Gray;
            this.txtNombreProc.BackColor = Color.Gray;
            chkNombre.Text = "Nombre Automatico";

            chckOrdAl.Checked = true;
            chkDurAl.Checked = true;
            chkNombre.Checked = true;

            
            // agregar elementos a la lista
            
            listView1.Columns.Add("1" ,"Nombre", listView1.Width / 3);
            listView1.Columns.Add("2", "Duracion", listView1.Width / 3);
            listView1.Columns.Add("3","Tiempo de espera", listView1.Width / 3);


            // fijar propiedades de la combobox
            comboBox1.Items.Add("FsFS");
            comboBox1.Items.Add("SJF");

            gGant.Text = " ";
            gin.Text = " ";
            gStdcs.Text = " ";
            
            




        }

        private void chckOrdAl_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.gin.Enabled == false)
                gin.Enabled = true;


        }

        private void creatGrafico ( Proceso [] vectorProc,int []tiempoEspera,int tfinal)
        {
            

            // graficar el vector de procesos 
            gant.Titles.Add("Diagrama de Gant de Procesos");
            gant.ChartAreas["ChartArea1"].AxisY.Title = "Procesos";
            gant.ChartAreas["ChartArea1"].AxisX.Title = "Tiempo";
           
           

            for (int i = 0; i< vectorProc.Length; i ++)
            {
                // agregar serie al grafico
                Series serie = gant.Series.Add(vectorProc[i].GSnombre);
                

                // cambiar tipo de grafico
                serie.ChartType = SeriesChartType.Point;

                // ver si es solo 1 proceso
                if (vectorProc.Length == 1)
                {
                    // poner puntos para cada tiempo de espera 
                    for (int j = 0; j < vectorProc[i].GSduracion; j++)

                        // agragar puntos al grafico
                        serie.Points.AddXY(j, i + 1);
                    return;
                }
                    


                if ( i >= 0 && i < tiempoEspera.Length - 1 )
                {
                    // poner puntos para cada tiempo de espera 
                    for (int j = tiempoEspera[i]; j < tiempoEspera[i+1]; j++)
                    {
                        // agragar puntos al grafico
                        serie.Points.AddXY(j,i+1);
                    }
                }
                else
                {
                    // poner puntos para cada tiempo de espera 
                    for (int j = tiempoEspera[tiempoEspera.Length-1]; j < (tiempoEspera[tiempoEspera.Length - 1]  + vectorProc[vectorProc.Length-1].GSduracion) ; j++)
                    {
                        // agragar puntos al grafico
                        serie.Points.AddXY(j, i+1);
                    }

                }






            }

           
        }

        private void chkDurAl_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDurProc.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numerProcesos = Convert.ToInt16(numeroProcesos.Text);
            int tiempoEspera = 0;
            int tiempoProm = 0;
            Random r = new Random();
            Planificador pln1 = new Planificador();


            // si todo es automatico
            if ( chkNombre.Checked && chkDurAl.Checked)
            {
                Proceso[] vectProcesos = new Proceso[numerProcesos];
                int[] tiemposEspera = new int [numerProcesos];

                // iterar para cada uno de los procesos 
                for (int i = 0; i < vectProcesos.Length; i++)
                {
                    int cuenta = i + 1;

                    lbProcesos.Text = "Proceso " + cuenta.ToString();
                    Thread.Sleep(400);
                    lbProcesos.Refresh();

                    vectProcesos[i] = new Proceso();
                    vectProcesos[i].GSnombre = "p" + cuenta.ToString();
                    vectProcesos[i].GSduracion = r.Next(1, 25);
                }

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


                // procesar cada proceso del vector 
                for (int i = 0; i < vectProcesos.Length; i++)
                {
                    // agregar al list view
                    listView1.View = View.Details;

                    ListViewItem it = new ListViewItem(vectProcesos[i].GSnombre);
         
                    it.SubItems.Add(vectProcesos[i].GSduracion.ToString());
                    it.SubItems.Add(tiempoEspera.ToString() );

                    listView1.Items.Add(it);

                    Thread.Sleep(400);
                    listView1.Refresh();

                    tiemposEspera[i] = tiempoEspera;
                    tiempoProm += tiempoEspera;
                    tiempoEspera += vectProcesos[i].GSduracion;
                }

                float prom = (float)tiempoProm / vectProcesos.Length;
                lblPromedio.Text = prom.ToString("0.00");

             
                // crear grafico
                creatGrafico(vectProcesos,tiemposEspera,numerProcesos-1);
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
    }
}
