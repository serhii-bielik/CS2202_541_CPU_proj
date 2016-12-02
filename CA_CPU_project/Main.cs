using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CA_CPU_project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void but_exe_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
            {
                MessageBox.Show("Please enter a code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ResultForm rf = new ResultForm(new CPU(richTextBox1.Text.Split('\n')));
            rf.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            CheckKeyword("mov", Color.Blue, 0);
            CheckKeyword("add", Color.Blue, 0);
            CheckKeyword("sub", Color.Blue, 0);
            CheckKeyword("mul", Color.Blue, 0);
            CheckKeyword("div", Color.Blue, 0);
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }
    }
}
