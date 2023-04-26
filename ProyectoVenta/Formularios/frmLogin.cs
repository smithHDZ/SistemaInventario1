using MySql.Data.MySqlClient;
using ProyectoVenta.Formularios.Permisos;
using ProyectoVenta.Formularios;
using ProyectoVenta.Logica;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Formularios
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //MySqlConnection conexion = new MySqlConnection("server=localhost; database=proyectobd; User Id=Arnoldo; Password=123456");

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ouo.io/RK1tRH");
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ouo.io/VRgLgZ");
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            txtusuario.Text = "";
            txtclave.Text = "";
            this.Show();
            txtusuario.Focus();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexion = new Conexion().AbrirConexion())
            {
                string Uid = txtusuario.Text;
                string Pwd = txtclave.Text;

                string query = "SELECT COUNT(*) FROM usuario WHERE NombreUsuario=@Uid AND Clave=@Pwd";

                // get the count
                int count = 0;

                using (var command = new MySqlCommand(query, conexion))
                {
                    // Agregar los parámetros a la consulta
                    command.Parameters.AddWithValue("@Uid", Uid);
                    command.Parameters.AddWithValue("@Pwd", Pwd);

                    count = Convert.ToInt32(command.ExecuteScalar());
                }

                if (count == 1)
                {
                    MessageBox.Show("¡Bienvenido " + Uid + "!");
                    Inicio frm = new Inicio();
                    //frmPermisos frm = new frmPermisos();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
        }

        private void iconPictureBox1_MouseHover(object sender, EventArgs e)
        {
            iconPictureBox1.BackColor = Color.DarkCyan;
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            iconPictureBox1.BackColor = Color.Teal;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
