using MySql.Data.MySqlClient;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class UsuarioLogica
    {
        MySqlDataReader resultado;
        DataTable tabla = new DataTable();
        MySqlConnection sqlConexion = new MySqlConnection();

        private static UsuarioLogica _instancia = null;

        public UsuarioLogica()
        {

        }

        public static UsuarioLogica Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new UsuarioLogica();
                return _instancia;
            }
        }

        public DataTable ObtenerUsuarios()
        {
            try
            {
                sqlConexion = new Conexion().AbrirConexion();
                MySqlCommand comando = new MySqlCommand("ObtenerUsuarios", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                sqlConexion.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        public int resetear()
        {
            int respuesta = 0;
            try
            {
                using (MySqlConnection conexion = new Conexion().AbrirConexion())
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update USUARIO set NombreUsuario = 'Admin', Clave = '123' where IdUsuario = 1;");
                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
            }

            return respuesta;
        }


        public List<Usuario> Listar(out string mensaje)
        {
            mensaje = string.Empty;
            List<Usuario> oLista = new List<Usuario>();

            try
            {
                using (MySqlConnection conexion = new Conexion().AbrirConexion())
                {
                    

                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario,u.NombreCompleto,u.NombreUsuario,u.Clave,u.IdPermisos,p.Descripcion from USUARIO u");
                    query.AppendLine("inner join PERMISOS p on p.IdPermisos = u.IdPermisos;");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Usuario()
                            {
                                IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                IdPermisos = Convert.ToInt32(dr["IdPermisos"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Usuario>();
                mensaje = ex.Message;
            }
            return oLista;
        }

        public int Existe(string usuario, int defaultid, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            using (MySqlConnection conexion = new Conexion().AbrirConexion())
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*)[resultado] from USUARIO where upper(NombreUsuario) = upper(@pnombreusuario) and IdUsuario != @defaultid");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new MySqlParameter("@pnombreusuario", usuario));
                    cmd.Parameters.Add(new MySqlParameter("@defaultid", defaultid));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "El usuario ya existe";

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }

            }
            return respuesta;
        }

        public int Guardar(Usuario objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (MySqlConnection conexion = new Conexion().AbrirConexion())
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("insert into USUARIO(NombreCompleto,NombreUsuario,Clave,IdPermisos) values (@pnombrecompleto,@pnombreusuario,@pclave,@pidpermisos);");
                    query.AppendLine("select last_insert_rowid();");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new MySqlParameter("@pnombrecompleto", objeto.NombreCompleto));
                    cmd.Parameters.Add(new MySqlParameter("@pnombreusuario", objeto.NombreUsuario));
                    cmd.Parameters.Add(new MySqlParameter("@pclave", objeto.Clave));
                    cmd.Parameters.Add(new MySqlParameter("@pidpermisos", objeto.IdPermisos));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta < 1)
                        mensaje = "No se pudo registrar el usuario";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        public int Editar(Usuario objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (MySqlConnection conexion = new Conexion().AbrirConexion())
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update USUARIO set NombreCompleto = @pnombrecompleto,NombreUsuario = @pnombreusuario,Clave = @pclave,IdPermisos = @pidpermisos  where IdUsuario = @pid");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new MySqlParameter("@pnombrecompleto", objeto.NombreCompleto));
                    cmd.Parameters.Add(new MySqlParameter("@pnombreusuario", objeto.NombreUsuario));
                    cmd.Parameters.Add(new MySqlParameter("@pclave", objeto.Clave));
                    cmd.Parameters.Add(new MySqlParameter("@pidpermisos", objeto.IdPermisos));
                    cmd.Parameters.Add(new MySqlParameter("@pid", objeto.IdUsuario));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo editar el usuario";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }


        public int Eliminar(int id)
        {
            int respuesta = 0;
            try
            {
                using (MySqlConnection conexion = new Conexion().AbrirConexion())
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from USUARIO where IdUsuario= @id;");
                    MySqlCommand cmd = new MySqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new MySqlParameter("@id", id));
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
            }
            return respuesta;
        }


    }
}
