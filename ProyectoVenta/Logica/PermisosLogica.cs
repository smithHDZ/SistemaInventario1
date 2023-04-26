using MySql.Data.MySqlClient;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class PermisosLogica
    {

        private static PermisosLogica _instancia = null;

        public PermisosLogica()
        {

        }

        public static PermisosLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new PermisosLogica();
                return _instancia;
            }
        }



        public Permisos Obtener(int idpermisos)
        {
            Permisos obj = new Permisos();
            try
            {
                using (MySqlConnection conexion = new Conexion().AbrirConexion())
                {
                    conexion.Open();
                    string query = "select IdPermisos,Descripcion,Salidas,Entradas,Productos,Clientes,Proveedores,Inventario,Configuracion from PERMISOS where IdPermisos = @pid";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.Add(new MySqlParameter("@pid", idpermisos));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Permisos()
                            {
                                IdPermisos = int.Parse(dr["IdPermisos"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                Salidas = int.Parse(dr["Salidas"].ToString()),
                                Entradas = int.Parse(dr["Entradas"].ToString()),
                                Productos = int.Parse(dr["Productos"].ToString()),
                                Clientes = int.Parse(dr["Clientes"].ToString()),
                                Proveedores = int.Parse(dr["Proveedores"].ToString()),
                                Inventario = int.Parse(dr["Inventario"].ToString()),
                                Configuracion = int.Parse(dr["Configuracion"].ToString())
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj = new Permisos();
            }
            return obj;
        }

        public int Guardar(Permisos objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (MySqlConnection conexion = new Conexion().AbrirConexion())
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PERMISOS SET");
                    query.AppendLine("Salidas = @psalida,");
                    query.AppendLine("Entradas = @pentrada,");
                    query.AppendLine("Productos = @pproducto,");
                    query.AppendLine("Clientes = @pcliente,");
                    query.AppendLine("Proveedores = @pproveedor,");
                    query.AppendLine("Inventario = @pinventario,");
                    query.AppendLine("Configuracion = @pconfiguracion");
                    query.AppendLine("where IdPermisos = @pid;");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new MySqlParameter("@psalida", objeto.Salidas));
                    cmd.Parameters.Add(new MySqlParameter("@pentrada", objeto.Entradas));
                    cmd.Parameters.Add(new MySqlParameter("@pproducto", objeto.Productos));
                    cmd.Parameters.Add(new MySqlParameter("@pcliente", objeto.Clientes));
                    cmd.Parameters.Add(new MySqlParameter("@pproveedor", objeto.Proveedores));
                    cmd.Parameters.Add(new MySqlParameter("@pinventario", objeto.Inventario));
                    cmd.Parameters.Add(new MySqlParameter("@pconfiguracion", objeto.Configuracion));
                    cmd.Parameters.Add(new MySqlParameter("@pid", objeto.IdPermisos));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo actualizar los permisos";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

    }
}
