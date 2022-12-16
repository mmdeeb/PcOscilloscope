using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellMouseUp += DataGridView_CellMouseUp;
            dataGridView1.ColumnCount = 1;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = 1000;
            string[] ports = SerialPort.GetPortNames();

            string[] items = new string[] { "2400", "4800", "9600", "14400", "19200", "28800", "38400", "57600", "76800", "115200", "230400", "250000" };
            foreach (var item in items)
            {
                BaudRate.Items.Add(item);
            }
            foreach (var port in ports)
            {
                comboBox1.Items.Add(port);

            }

        }
        int rowIndex = 0;
        void DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
            }
        }
        
        List<String> Ex = new List<String>();
        double T = 0;
        Thread masterThread;
        SerialPort port;
      
        
        private void Start_Click(object sender, EventArgs e)
        {
            try
            {

                port = new SerialPort(comboBox1.Text, Convert.ToInt32(BaudRate.Text), Parity.None, 8, StopBits.One);
                if (port.IsOpen)
                    port.Close();
                port.Open();
                try
                {

                    masterThread = new Thread(Runit);
                    masterThread.Start();

                }
                catch (Exception ex)
                {
                    Ex.Add(ex.Message);
                    dataGridView1.Rows.Add("Error read from serial: " + ex.Message);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.BackColor = Color.DarkGoldenrod;
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.ForeColor = Color.White;

                }


            }
            catch (Exception ex)
            {
                Ex.Add(ex.Message);
                dataGridView1.Rows.Add(ex.Message);
                dataGridView1.ClearSelection();
                dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.BackColor = Color.DarkRed;
                dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.ForeColor = Color.White;
            }


        }
        void Runit()
        {

            while (port.IsOpen)
            {
                try
                {

                    if (chart1.InvokeRequired)
                    {
                        T += 33;





                        double getValueI = (Convert.ToInt32(Convert.ToByte(port.ReadByte()))) / 51.2;
                        chart1.Invoke((MethodInvoker)delegate
                        {
                            if (checkBox1.Checked)
                            {

                                double to;
                                double from; 
                                if ( double.TryParse(To_text.Text,out to) && double.TryParse(From_text.Text, out from) && (from < to))
                                {
                                    currentXMax = to;
                                    currentXMin = from;
                                    
                                }
                                else 
                                {  
                                    
                                    currentXMax = T;

                                    currentXMin = T - TimeT;
                                }
                                    


                            }
                            else
                            {
                               
                                currentXMax = T;
                                currentXMin = T - TimeT;
                            }

                            chart1.Series["Series1"].Points.AddXY(T, getValueI);
                            chart1.ChartAreas[0].AxisX.Minimum = currentXMin;
                            chart1.ChartAreas[0].AxisX.Maximum = currentXMax;
                            V_Text.Text = getValueI.ToString();
                            T_Text.Text = ((currentXMax - currentXMin) / 1000).ToString();

                        });
                    }

                }
                catch (Exception ex)
                {
                    Ex.Add(ex.Message);
                    dataGridView1.Rows.Add(ex.Message);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.BackColor = Color.DarkGreen;
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.ForeColor = Color.White;
                }


            }

        }
        
        private void Up_Click(object sender, EventArgs e)
        {
            currentYMax += 1;
            currentYMin += 1;
            setScale();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            currentYMax -= 1;
            currentYMin -= 1;
            setScale();

        }
        double currentXMax = 10;
        double currentXMin = 1;
        double currentYMax = 5;
        double currentYMin = -5;
        void setScale()
        {
            if (currentXMax > currentXMin && currentYMax > currentYMin)
            {

                chart1.ChartAreas[0].AxisY.Maximum = currentYMax;
                chart1.ChartAreas[0].AxisY.Minimum = currentYMin;
            }
        }
        
        int TimeT = 1;
        
        private void Time_Scroll(object sender, EventArgs e)
        {
            setTimeT();
        }
        void setTimeT()
        {
            TimeT = trackBar1.Value;
        }


       
    }
}
