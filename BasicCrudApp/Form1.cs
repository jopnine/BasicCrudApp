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
            
        }

        private Boolean validateFields()
        {
            for (int i = 0; i < fields.Count; i++)
            {
                if (String.IsNullOrEmpty(fields[i].Text))
                {
                    MessageBox.Show("All fields must me filled");
                    return false; 
                }
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

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
    }
}
