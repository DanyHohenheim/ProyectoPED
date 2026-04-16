using System;
using System.Windows.Forms;

namespace RegistroEstudiantes
{
    public class FrmPrincipal : Form
    {
        private Button btnLogin;
        private Button btnEstudiantes;
        private Button btnSalir;
        private Label lblTitulo;

        public FrmPrincipal()
        {
            Text = "Registro de Estudiantes - Prototipo (30% - Beta)";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 700;
            Height = 420;

            lblTitulo = new Label
            {
                Text = "Registro de Estudiantes (Prototipo 30% - Beta)",
                AutoSize = true,
                Left = 20,
                Top = 20,
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
            };

            btnLogin = new Button
            {
                Text = "Login (Beta)",
                Left = 20,
                Top = 90,
                Width = 200,
                Height = 40
            };
            btnLogin.Click += (_, __) =>
            {
                using var f = new FrmLogin();
                f.ShowDialog(this);
            };

            btnEstudiantes = new Button
            {
                Text = "Mantenimiento Estudiantes",
                Left = 20,
                Top = 150,
                Width = 200,
                Height = 40
            };
            btnEstudiantes.Click += (_, __) =>
            {
                using var f = new FrmEstudiantes();
                f.ShowDialog(this);
            };

            btnSalir = new Button
            {
                Text = "Salir",
                Left = 20,
                Top = 210,
                Width = 200,
                Height = 40
            };
            btnSalir.Click += (_, __) => Close();

            Controls.Add(lblTitulo);
            Controls.Add(btnLogin);
            Controls.Add(btnEstudiantes);
            Controls.Add(btnSalir);
        }
    }
}