using Irony.Interpreter;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bionware.PowerPC;
using System.Text.RegularExpressions;

namespace PowerPCEmulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = Regex.Split(textBox1.Text.Trim(), "\r\n");
            Emulator.Evaluate(textBox1.Text, lines.Length);
        }

        }
}
