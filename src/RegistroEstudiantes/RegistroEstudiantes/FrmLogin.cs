using System;
using System.Windows.Forms;

namespace RegistroEstudiantes
{
    public class FrmLogin : Form
    {
        private Label lblUser;
        private Label lblPass;
        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnIngresar;
        private Button btnCancelar;

        public FrmLogin()
        {
            Text = "Login (Beta)";
            StartPosition = FormStartPosition.CenterParent;
            Width = 420;
            Height = 230;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblUser = new Label { Text = "Usuario:", Left = 20, Top = 25, AutoSize = true };
            txtUser = new TextBox { Left = 120, Top = 20, Width = 250, Text = "admin" };

            lblPass = new Label { Text = "Clave:", Left = 20, Top = 65, AutoSize = true };
            txtPass = new TextBox { Left = 120, Top = 60, Width = 250, UseSystemPasswordChar = true, Text = "1234" };

            btnIngresar = new Button { Text = "Ingresar", Left = 120, Top = 110, Width = 120, Height = 35 };
            btnIngresar.Click += (_, __) =>
            {
                MessageBox.Show("Login exitoso (beta).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar = new Button { Text = "Cancelar", Left = 250, Top = 110, Width = 120, Height = 35 };
            btnCancelar.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblUser);
            Controls.Add(txtUser);
            Controls.Add(lblPass);
            Controls.Add(txtPass);
            Controls.Add(btnIngresar);
            Controls.Add(btnCancelar);
        }
    }
}