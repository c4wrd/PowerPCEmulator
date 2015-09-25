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
using MetroFramework;
using System.IO;

namespace PowerPCEmulator
{
    public partial class EmulatorForm : Form
    {
        public EmulatorForm()
        {
            InitializeComponent();
        }

        public class TextBoxWriter : StringWriter
        {
            TextBox _output = null;

            public TextBoxWriter(TextBox box)
            {
                _output = box;
            }
            public override void WriteLine(string x)
            {
                _output.AppendText(x + "\r\n");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.SetOut(new TextBoxWriter(textBox2));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int lines = textBox1.Lines.Count();
            locLabel.Text = string.Format("Lines: {0}", lines);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Emulator emu = new Emulator();
            var status = emu.Evaluate(textBox1.Text, textBox1.Lines.Count());
            if (status["status"] == "ok")
            {
                parseTimeLabel.Text = string.Format("Parse Time: {0}ms", status["parse_time"]);
                execTimeLabel.Text = string.Format("Execution Time: {0}ms", status["execution_time"]);
            }
            else
            {
                Console.WriteLine("Error {0} at line {1}, column {2}", 
                    status["error_message"], 
                    ((SourceLocation)status["error_location"]).Line+1,
                    ((SourceLocation)status["error_location"]).Column
                );
            }
        }
    }
}
