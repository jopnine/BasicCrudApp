using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCrudApp
{
    public partial class FrmLogin : Form
    {
        List<TextBox> fields = new List<TextBox>();
        public FrmLogin()
        {
            InitializeComponent();
        }
        public void populateFields()
        {
            fields.Add(TxtUser);
            fields.Add(TxtPassword);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            populateFields();   
        }

        private Boolean validateFields()
        {
            for (int i = 0; i < fields.Count; i++)
            {
                if (String.IsNullOrEmpty(fields[i].Text))
                {
                    //MessageBox.Show("All fields must me filled");
                    MessageBox.Show($"The field {fields[i].Tag.ToString()} must be filled!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false; 
                }
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateFields())
                {
                    MessageBox.Show("Login sucessful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
            //if (Txtuser.Text != '')
            //{
            //    MessageBox.Show("Test");
            //}
        }

        private void TxtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  this.Hide(); // Hide the initial Form
            FrmSettings frmsettings = new FrmSettings();
            frmsettings.ShowDialog();
        }
    }
}
