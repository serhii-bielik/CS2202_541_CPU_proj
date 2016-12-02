using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CA_CPU_project
{
    public partial class ResultForm : Form
    {
        CPU instance;
        List<TextBox> txtRegistersBin;
        List<TextBox> txtRegistersDec;
        public ResultForm(CPU instance)
        {           
            InitializeComponent();
            this.dataGridView1.DefaultCellStyle.Font = new Font("Consolas", 9);
            this.instance = instance;
            txtRegistersBin = new List<TextBox>();
            txtRegistersBin.Add(textBox1);
            txtRegistersBin.Add(textBox2);
            txtRegistersBin.Add(textBox3);
            txtRegistersBin.Add(textBox4);
            txtRegistersBin.Add(textBox5);
            txtRegistersBin.Add(textBox6);
            txtRegistersBin.Add(textBox7);
            txtRegistersBin.Add(textBox8);

            txtRegistersDec = new List<TextBox>();
            txtRegistersDec.Add(textBox11);
            txtRegistersDec.Add(textBox12);
            txtRegistersDec.Add(textBox13);
            txtRegistersDec.Add(textBox14);
            txtRegistersDec.Add(textBox15);
            txtRegistersDec.Add(textBox16);
            txtRegistersDec.Add(textBox17);
            txtRegistersDec.Add(textBox18);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (instance.hasCode())
                {
                    instance.ExecuteAllLines();
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("The code if fully complited", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong\n\nInfo:\n" + ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (instance.hasCode())
                {
                    instance.ExecuteOneLine();
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("The code if fully complited", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong\n\nInfo:\n" + ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshData()
        {
            String[] registers = instance.getRegistersData();
            for (int i = 0; i < registers.Length; i++)
            {
                txtRegistersBin[i].Text = registers[i];
                txtRegistersDec[i].Text = Convert.ToInt32(registers[i], 2).ToString();
            }

            String[] tableData = instance.getTableData();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            for (int i = 0; i < tableData.Length; i++)
            {
                String[] row = tableData[i].Split('|');
                dataGridView1.Rows.Add(row);
            }

            txt_CPI.Text = instance.getCPI();
        }
    }
}
