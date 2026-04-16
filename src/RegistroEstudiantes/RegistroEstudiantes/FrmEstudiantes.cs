using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegistroEstudiantes
{
    public class FrmEstudiantes : Form
    {
        private TextBox txtCarnet;
        private TextBox txtNombres;
        private TextBox txtApellidos;
        private ComboBox cmbCarrera;
        private Button btnGuardarBeta;
        private Button btnBuscarBeta;
        private Button btnListarBeta;
        private Button btnLimpiar;
        private DataGridView dgv;

        // ABB (Árbol Binario de Búsqueda) por Carnet
        private readonly ArbolBinarioBusquedaEstudiantes _abb = new ArbolBinarioBusquedaEstudiantes();

        public FrmEstudiantes()
        {
            Text = "Estudiantes (Prototipo 30% - Beta / ABB)";
            StartPosition = FormStartPosition.CenterParent;
            Width = 900;
            Height = 520;

            Label lblCarnet = new Label { Text = "Carnet:", Left = 20, Top = 20, AutoSize = true };
            txtCarnet = new TextBox { Left = 120, Top = 15, Width = 250, PlaceholderText = "Ej: LP123456" };

            Label lblNombres = new Label { Text = "Nombres:", Left = 20, Top = 55, AutoSize = true };
            txtNombres = new TextBox { Left = 120, Top = 50, Width = 250 };

            Label lblApellidos = new Label { Text = "Apellidos:", Left = 20, Top = 90, AutoSize = true };
            txtApellidos = new TextBox { Left = 120, Top = 85, Width = 250, PlaceholderText = "Ej: López Pérez" };

            Label lblCarrera = new Label { Text = "Carrera:", Left = 20, Top = 125, AutoSize = true };
            cmbCarrera = new ComboBox { Left = 120, Top = 120, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCarrera.Items.AddRange(new object[]
            {
                "Ingeniería en Sistemas",
                "Administración de Empresas",
                "Técnico en Informática"
            });
            if (cmbCarrera.Items.Count > 0) cmbCarrera.SelectedIndex = 0;

            btnGuardarBeta = new Button { Text = "Guardar (Beta)", Left = 420, Top = 15, Width = 160, Height = 35 };
            btnGuardarBeta.Click += (_, __) => GuardarEnAbb();

            btnBuscarBeta = new Button { Text = "Buscar (Beta)", Left = 420, Top = 55, Width = 160, Height = 35 };
            btnBuscarBeta.Click += (_, __) => BuscarEnAbb();

            btnListarBeta = new Button { Text = "Listar InOrden (Beta)", Left = 420, Top = 95, Width = 160, Height = 35 };
            btnListarBeta.Click += (_, __) => RefrescarTablaDesdeAbb();

            btnLimpiar = new Button { Text = "Limpiar", Left = 420, Top = 135, Width = 160, Height = 35 };
            btnLimpiar.Click += (_, __) => Limpiar();

            dgv = new DataGridView
            {
                Left = 20,
                Top = 190,
                Width = 840,
                Height = 260,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = true
            };

            Controls.Add(lblCarnet);
            Controls.Add(txtCarnet);
            Controls.Add(lblNombres);
            Controls.Add(txtNombres);
            Controls.Add(lblApellidos);
            Controls.Add(txtApellidos);
            Controls.Add(lblCarrera);
            Controls.Add(cmbCarrera);

            Controls.Add(btnGuardarBeta);
            Controls.Add(btnBuscarBeta);
            Controls.Add(btnListarBeta);
            Controls.Add(btnLimpiar);
            Controls.Add(dgv);

            // Las pruebas pre cargadas
            _abb.Insertar(new EstudianteBeta { Carnet = "LP123456", Nombres = "Juan", Apellidos = "López Pérez", Carrera = "Ingeniería en Sistemas", Activo = true });
            _abb.Insertar(new EstudianteBeta { Carnet = "GL654321", Nombres = "Ana", Apellidos = "García López", Carrera = "Administración de Empresas", Activo = true });
            _abb.Insertar(new EstudianteBeta { Carnet = "MR000001", Nombres = "Luis", Apellidos = "Martínez Reyes", Carrera = "Técnico en Informática", Activo = true });

            RefrescarTablaDesdeAbb();
        }

        private void GuardarEnAbb()
        {
            string apellidos = (txtApellidos.Text ?? "").Trim();
            string carnet = (txtCarnet.Text ?? "").Trim().ToUpperInvariant();
            string nombres = (txtNombres.Text ?? "").Trim();
            string carrera = cmbCarrera.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(apellidos))
            {
                MessageBox.Show("Ingrese Apellidos (se usan para iniciales del carné).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Regla: Carnet debe ser 2 letras + 6 números, y las 2 letras deben coincidir con iniciales de apellidos
            if (!ValidarCarnetConApellidos(carnet, apellidos, out string error))
            {
                MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = _abb.Insertar(new EstudianteBeta
            {
                Carnet = carnet,
                Nombres = nombres,
                Apellidos = apellidos,
                Carrera = carrera,
                Activo = true
            });

            if (!ok)
            {
                MessageBox.Show("Ya existe un estudiante con ese Carnet (beta).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Guardado en ABB (beta).", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarTablaDesdeAbb();
            Limpiar();
        }

        private void BuscarEnAbb()
        {
            string carnet = (txtCarnet.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(carnet))
            {
                MessageBox.Show("Ingrese el Carnet para buscar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var e = _abb.Buscar(carnet);
            if (e == null)
            {
                MessageBox.Show("No encontrado en ABB (beta).", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtNombres.Text = e.Nombres;
            txtApellidos.Text = e.Apellidos;
            cmbCarrera.SelectedItem = e.Carrera;

            MessageBox.Show("Encontrado en ABB (beta).", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefrescarTablaDesdeAbb()
        {
            List<EstudianteBeta> ordenado = _abb.RecorridoInOrden();

            dgv.DataSource = null;
            dgv.DataSource = ordenado;
        }

        private void Limpiar()
        {
            txtCarnet.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            if (cmbCarrera.Items.Count > 0) cmbCarrera.SelectedIndex = 0;
            txtCarnet.Focus();
        }

        private static readonly Regex RxCarnet = new Regex(@"^[A-Z]{2}\d{6}$", RegexOptions.Compiled);

        private static bool ValidarCarnetConApellidos(string carnet, string apellidos, out string error)
        {
            carnet = (carnet ?? "").Trim().ToUpperInvariant();
            apellidos = (apellidos ?? "").Trim();

            if (!RxCarnet.IsMatch(carnet))
            {
                error = "Carnet inválido. Formato requerido: 2 letras + 6 números. Ej: LP123456";
                return false;
            }

            string iniciales = ObtenerInicialesApellidos(apellidos);
            if (iniciales.Length < 2)
            {
                error = "Apellidos inválidos. Debe escribir al menos 2 apellidos (Ej: López Pérez).";
                return false;
            }

            string letrasCarnet = carnet.Substring(0, 2);
            if (!string.Equals(letrasCarnet, iniciales, StringComparison.OrdinalIgnoreCase))
            {
                error = $"Las letras del carné deben ser las iniciales de los apellidos.\n" +
                        $"Iniciales esperadas: {iniciales}\n" +
                        $"Carnet ingresado: {carnet}";
                return false;
            }

            error = "";
            return true;
        }

        private static string ObtenerInicialesApellidos(string apellidos)
        {
            // Se toma la primera letra del primer apellido y la primera letra del segundo apellido
            // Separamos por espacios y eliminamos partes vacías
            var partes = apellidos.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length < 2) return "";

            char a1 = char.ToUpperInvariant(partes[0][0]);
            char a2 = char.ToUpperInvariant(partes[1][0]);

            // Si vienen apellidos con letras con acento, esto los deja igual (ej: Á -> Á).
            // Para simplificar validación, aceptamos A-Z en carnet, por eso se recomienda que el usuario escriba sin acento en carnet.
            return $"{a1}{a2}";
        }
    }

    // =========================
    // MODELO BETA
    // =========================
    public sealed class EstudianteBeta
    {
        public string Carnet { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Carrera { get; set; } = "";
        public bool Activo { get; set; }
    }

    // =========================
    // ABB POR CARNET (string)
    // =========================
    public sealed class ArbolBinarioBusquedaEstudiantes
    {
        private Nodo? _raiz;

        private sealed class Nodo
        {
            public string Clave;
            public EstudianteBeta Valor;
            public Nodo? Izq;
            public Nodo? Der;

            public Nodo(EstudianteBeta v)
            {
                Valor = v;
                Clave = NormalizarCarnet(v.Carnet);
            }
        }

        private static string NormalizarCarnet(string carnet)
            => (carnet ?? "").Trim().ToUpperInvariant();

        public bool Insertar(EstudianteBeta e)
        {
            var nuevo = new Nodo(e);

            if (_raiz == null)
            {
                _raiz = nuevo;
                return true;
            }

            var actual = _raiz;
            while (true)
            {
                int cmp = string.Compare(nuevo.Clave, actual.Clave, StringComparison.Ordinal);

                if (cmp == 0) return false;

                if (cmp < 0)
                {
                    if (actual.Izq == null) { actual.Izq = nuevo; return true; }
                    actual = actual.Izq;
                }
                else
                {
                    if (actual.Der == null) { actual.Der = nuevo; return true; }
                    actual = actual.Der;
                }
            }
        }

        public EstudianteBeta? Buscar(string carnet)
        {
            var clave = NormalizarCarnet(carnet);
            var actual = _raiz;

            while (actual != null)
            {
                int cmp = string.Compare(clave, actual.Clave, StringComparison.Ordinal);
                if (cmp == 0) return actual.Valor;
                actual = (cmp < 0) ? actual.Izq : actual.Der;
            }
            return null;
        }

        public List<EstudianteBeta> RecorridoInOrden()
        {
            var list = new List<EstudianteBeta>();
            InOrden(_raiz, list);
            return list;
        }

        private static void InOrden(Nodo? n, List<EstudianteBeta> salida)
        {
            if (n == null) return;
            InOrden(n.Izq, salida);
            salida.Add(n.Valor);
            InOrden(n.Der, salida);
        }
    }
}