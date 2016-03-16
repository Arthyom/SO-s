namespace FsFc
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.gin = new System.Windows.Forms.GroupBox();
            this.chkNombre = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.chckOrdAl = new System.Windows.Forms.CheckBox();
            this.chkDurAl = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbProcesos = new System.Windows.Forms.Label();
            this.txtDurProc = new System.Windows.Forms.TextBox();
            this.txtNombreProc = new System.Windows.Forms.TextBox();
            this.numeroProcesos = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gStdcs = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPromedio = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.gGant = new System.Windows.Forms.GroupBox();
            this.gant = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            this.gin.SuspendLayout();
            this.gStdcs.SuspendLayout();
            this.gGant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gant)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.gin);
            this.groupBox1.Controls.Add(this.numeroProcesos);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 195);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // gin
            // 
            this.gin.Controls.Add(this.chkNombre);
            this.gin.Controls.Add(this.button3);
            this.gin.Controls.Add(this.chckOrdAl);
            this.gin.Controls.Add(this.chkDurAl);
            this.gin.Controls.Add(this.label2);
            this.gin.Controls.Add(this.label1);
            this.gin.Controls.Add(this.lbProcesos);
            this.gin.Controls.Add(this.txtDurProc);
            this.gin.Controls.Add(this.txtNombreProc);
            this.gin.Location = new System.Drawing.Point(6, 47);
            this.gin.Name = "gin";
            this.gin.Size = new System.Drawing.Size(167, 142);
            this.gin.TabIndex = 2;
            this.gin.TabStop = false;
            // 
            // chkNombre
            // 
            this.chkNombre.AutoSize = true;
            this.chkNombre.Location = new System.Drawing.Point(6, 118);
            this.chkNombre.Name = "chkNombre";
            this.chkNombre.Size = new System.Drawing.Size(99, 17);
            this.chkNombre.TabIndex = 18;
            this.chkNombre.Text = "Orden Aleatorio";
            this.chkNombre.UseVisualStyleBackColor = true;
            this.chkNombre.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(115, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 24);
            this.button3.TabIndex = 17;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chckOrdAl
            // 
            this.chckOrdAl.AutoSize = true;
            this.chckOrdAl.Location = new System.Drawing.Point(6, 101);
            this.chckOrdAl.Name = "chckOrdAl";
            this.chckOrdAl.Size = new System.Drawing.Size(99, 17);
            this.chckOrdAl.TabIndex = 16;
            this.chckOrdAl.Text = "Orden Aleatorio";
            this.chckOrdAl.UseVisualStyleBackColor = true;
            this.chckOrdAl.CheckedChanged += new System.EventHandler(this.chckOrdAl_CheckedChanged);
            // 
            // chkDurAl
            // 
            this.chkDurAl.AutoSize = true;
            this.chkDurAl.Location = new System.Drawing.Point(6, 84);
            this.chkDurAl.Name = "chkDurAl";
            this.chkDurAl.Size = new System.Drawing.Size(113, 17);
            this.chkDurAl.TabIndex = 15;
            this.chkDurAl.Text = "Duracion Aleatoria";
            this.chkDurAl.UseVisualStyleBackColor = true;
            this.chkDurAl.CheckedChanged += new System.EventHandler(this.chkDurAl_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(90, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Duracion";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 13;
            this.label1.Tag = "Nombre";
            this.label1.Text = "Nombre";
            // 
            // lbProcesos
            // 
            this.lbProcesos.AutoSize = true;
            this.lbProcesos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProcesos.ForeColor = System.Drawing.Color.Red;
            this.lbProcesos.Location = new System.Drawing.Point(6, 16);
            this.lbProcesos.Name = "lbProcesos";
            this.lbProcesos.Size = new System.Drawing.Size(57, 20);
            this.lbProcesos.TabIndex = 12;
            this.lbProcesos.Text = "label1";
            // 
            // txtDurProc
            // 
            this.txtDurProc.Location = new System.Drawing.Point(90, 57);
            this.txtDurProc.Name = "txtDurProc";
            this.txtDurProc.Size = new System.Drawing.Size(68, 20);
            this.txtDurProc.TabIndex = 11;
            // 
            // txtNombreProc
            // 
            this.txtNombreProc.Location = new System.Drawing.Point(6, 57);
            this.txtNombreProc.Name = "txtNombreProc";
            this.txtNombreProc.Size = new System.Drawing.Size(78, 20);
            this.txtNombreProc.TabIndex = 10;
            // 
            // numeroProcesos
            // 
            this.numeroProcesos.Location = new System.Drawing.Point(6, 21);
            this.numeroProcesos.Name = "numeroProcesos";
            this.numeroProcesos.Size = new System.Drawing.Size(109, 20);
            this.numeroProcesos.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(121, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gStdcs
            // 
            this.gStdcs.Controls.Add(this.label4);
            this.gStdcs.Controls.Add(this.lblPromedio);
            this.gStdcs.Controls.Add(this.listView1);
            this.gStdcs.Location = new System.Drawing.Point(188, 2);
            this.gStdcs.Name = "gStdcs";
            this.gStdcs.Size = new System.Drawing.Size(412, 223);
            this.gStdcs.TabIndex = 1;
            this.gStdcs.TabStop = false;
            this.gStdcs.Text = "groupBox2";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(331, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 14;
            this.label4.Tag = "Nombre";
            this.label4.Text = "Promedio";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPromedio
            // 
            this.lblPromedio.AutoSize = true;
            this.lblPromedio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromedio.ForeColor = System.Drawing.Color.Red;
            this.lblPromedio.Location = new System.Drawing.Point(337, 104);
            this.lblPromedio.Name = "lblPromedio";
            this.lblPromedio.Size = new System.Drawing.Size(57, 20);
            this.lblPromedio.TabIndex = 13;
            this.lblPromedio.Text = "label1";
            // 
            // listView1
            // 
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(6, 10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(316, 208);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // gGant
            // 
            this.gGant.Controls.Add(this.gant);
            this.gGant.Location = new System.Drawing.Point(2, 226);
            this.gGant.Name = "gGant";
            this.gGant.Size = new System.Drawing.Size(598, 256);
            this.gGant.TabIndex = 2;
            this.gGant.TabStop = false;
            this.gGant.Text = "groupBox2";
            // 
            // gant
            // 
            chartArea1.Name = "ChartArea1";
            this.gant.ChartAreas.Add(chartArea1);
            legend1.DockedToChartArea = "ChartArea1";
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            this.gant.Legends.Add(legend1);
            this.gant.Location = new System.Drawing.Point(0, 19);
            this.gant.Name = "gant";
            this.gant.Size = new System.Drawing.Size(598, 237);
            this.gant.TabIndex = 0;
            this.gant.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 483);
            this.Controls.Add(this.gGant);
            this.Controls.Add(this.gStdcs);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gin.ResumeLayout(false);
            this.gin.PerformLayout();
            this.gStdcs.ResumeLayout(false);
            this.gStdcs.PerformLayout();
            this.gGant.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gant)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gin;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chckOrdAl;
        private System.Windows.Forms.CheckBox chkDurAl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbProcesos;
        private System.Windows.Forms.TextBox txtDurProc;
        private System.Windows.Forms.TextBox txtNombreProc;
        private System.Windows.Forms.TextBox numeroProcesos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gStdcs;
        private System.Windows.Forms.GroupBox gGant;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chkNombre;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPromedio;
        private System.Windows.Forms.DataVisualization.Charting.Chart gant;
    }
}

