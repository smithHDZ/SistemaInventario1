using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class Conexion
    {
        private MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=proyectobd;Uid=root;Pwd=123456;");

        public MySqlConnection AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }

            return conexion;
        }

        public MySqlConnection CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }

            return conexion;
        }

    }
}
