using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
namespace 串口助手
{
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort bus_port=new SerialPort();
        SynchronizationContext m_SyncContext = null;
        public Form1()
        {
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] port = System.IO.Ports.SerialPort.GetPortNames();
            foreach(string var in port)
            {
                port_num.Items.Add(var);

            }
            int[] arry = { 4800, 9600, 115200 };
            foreach(int a in arry)
            {
                comboBox2.Items.Add(a.ToString());
            }
            int[] arry1 = { 6, 7, 8,9 };
            foreach (int a in arry1)
            {
                comboBox3.Items.Add(a);

            }

            foreach (System.IO.Ports.StopBits a in System.IO.Ports.StopBits.GetValues(typeof(System.IO.Ports.StopBits)))
            {
                comboBox4.Items.Add(a);

            }
            foreach (System.IO.Ports.Parity a in System.IO.Ports.SerialData.GetValues(typeof(System.IO.Ports.Parity)))
            {
                comboBox5.Items.Add(a);

            }

            port_num.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(port_num.SelectedItem.ToString());
            if (bus_port.IsOpen)
            {
                bus_port.Close();
            }
            bus_port.PortName = port_num.SelectedItem.ToString();
            bus_port.BaudRate =Convert.ToInt32(comboBox2.SelectedItem);
            bus_port.DataBits =Convert.ToInt16( comboBox3.SelectedItem);
            foreach (System.IO.Ports.StopBits a in System.IO.Ports.StopBits.GetValues(typeof(System.IO.Ports.StopBits)))
            {
                if (a.ToString().Equals(comboBox4.SelectedItem)) bus_port.StopBits = a;
            }
            foreach (System.IO.Ports.Parity a in System.IO.Ports.StopBits.GetValues(typeof(System.IO.Ports.StopBits)))
            {
                if (a.ToString().Equals(comboBox5.SelectedItem)) bus_port.Parity = a;
            }
           
            bus_port.Open();
            bus_port.DataReceived += Bus_port_DataReceived;
        }
        bool data_clock=true;
        int data_num = 0;
        int[] data=new int[5];
        StringBuilder str=new StringBuilder();
        private void date_chuli(Object Odata) {

            str.Clear();
            str.Append(textBox1.Text);
            str.Append( Convert.ToInt32(Odata).ToString("X"));
            textBox1.Text = str.ToString();
          
          
            
        }
       
        private void Bus_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int s = bus_port.BytesToRead;
            while (s-- != 0) {
                 Object temp = bus_port.ReadByte();
                 if(temp!=null)
                 m_SyncContext.Post(date_chuli, temp);
            }
           
           
        }
        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
