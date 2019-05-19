using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace TextEncryptorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                txtEncrypted.Text = Encrypt.EncryptString(txtMessage.Text, txtPassword.Text);
            }
            else
            {
                MessageBox.Show("Message and Password cannot be empty!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEncrypted.Text.Trim() != "" && txtPassword.Text.Trim() != "")
                {
                    txtDecrypted.Text = Encrypt.DecryptString(txtEncrypted.Text, txtPassword.Text);
                }
                else
                {
                    MessageBox.Show("No encrypted message to decrypt", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Error decrypting, password may be incorrect", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }
    }
}
