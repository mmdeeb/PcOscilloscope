using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
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
            dataGridView1.Columns[0].Width =1000;
            string[] ports = SerialPort.GetPortNames();

            string[] items =new string[] { "9600","14400","38400","57600","76800","115200,200000"};
            foreach (var item in items)
            {
                BaudRate.Items.Add(item);
            }
            foreach (var port in ports)
            {
                comboBox1.Items.Add(port);
               
            }
            
        }
        private int rowIndex = 0;
        private void DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
        double currentXMax = 10;
        double currentXMin = 1;
        double currentYMax = 6;
        double currentYMin = -6;
        void setScale()
        {
            if (currentXMax > currentXMin && currentYMax > currentYMin)
            {
             
                chart1.ChartAreas[0].AxisY.Maximum = currentYMax;
                chart1.ChartAreas[0].AxisY.Minimum = currentYMin;
            }
        }
        List<String> Ex = new List<String>();
        double T = 25;
        double X = 1;
        private void Start_Click(object sender, EventArgs e)
        {
            try
            {

            port = new SerialPort(comboBox1.Text,Convert.ToInt32(BaudRate.Text),Parity.None,8,StopBits.One);
            if(port.IsOpen)
                port.Close();
            port.Open();
                try
                {
                    Thread masterThread;
                    masterThread = new Thread(Runit);
                    masterThread.Start();

                }
                catch (Exception ex)
                {
                    Ex.Add(ex.Message);
                    dataGridView1.Rows.Add("Error read from serial: "+ex.Message);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.BackColor = Color.DarkGoldenrod;
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.ForeColor = Color.White;

                }
               /* try
                {
                    Thread myThread;
                    myThread = new Thread(setTime);
                    myThread.Start();

                }
                catch (Exception ex)
                {
                    Ex.Add(ex.Message);
                    dataGridView1.Rows.Add("Error Thread Time: " + ex.Message);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.BackColor = Color.DarkGoldenrod;
                    dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.ForeColor = Color.White;

                }*/

            }
            catch (Exception ex)
            {
                Ex.Add(ex.Message);
                dataGridView1.Rows.Add(ex.Message);
                dataGridView1.ClearSelection();
                dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.BackColor = Color.DarkRed;
                dataGridView1.Rows[Ex.Count - 1].Cells[0].Style.ForeColor = Color.White;
            }
           void Runit()
            {
                
                while (port.IsOpen)
                {
                    try
                    {
                        
                        byte re;
                        re=Convert.ToByte(port.ReadByte());
                        getValue = Convert.ToByte(port.ReadByte());
                        int getValueI = Convert.ToInt32(getValue);
                        
                        chart1.Invoke((MethodInvoker)(() => chart1.Series["Series1"].Points.AddXY(T, Convert.ToDouble(getValueI) / 51.2)));
                        //Console.WriteLine( "hello");
                        T +=25;
                        currentXMax = T;
                        currentXMin =currentXMax- 10;
                        //chart1.ChartAreas[0].AxisY.Minimum = currentXMin;




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



        }
        SerialPort port;
        Byte getValue;
        

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Up_Click(object sender, EventArgs e)
        {
            currentYMax +=1;
            currentYMin +=1;
            setScale();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            currentYMax -= 1;
            currentYMin -= 1;
            setScale();

        }
         void setTime()
        {
            Thread.Sleep(200);
            chart1.ChartAreas[0].AxisX.Maximum = currentXMax;
            
            chart1.ChartAreas[0].AxisX.Minimum  =currentXMin;


        }
        void setTimeT()
        {
            X = trackBar1.Value;

        }

        private void Time_Scroll(object sender, EventArgs e) 
        {
        setTimeT(); 
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}
