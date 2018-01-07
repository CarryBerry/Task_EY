using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private object _locker = new object();

        private void ModelRecordEntry(string fileEvent, string filePath)
        {
            lock (_locker)
            {
                //string path = ConfigurationManager.AppSettings["LoggerPath"]; // Pass can be changed
                textbox_Output.Text = $"{DateTime.Now:dd/MM/yyyy hh:mm:ss} file {filePath} was {fileEvent}\r\n";
            }
        }

        private void Creator_Click(object sender, EventArgs e)
        {
            Task_EY.FileCreator creator = new Task_EY.FileCreator();
            creator.CreateMany(Convert.ToInt32(numericUpDown1.Value));
            //textBox1.Text = numericUpDown1.Value.ToString();
        }
    }
}
