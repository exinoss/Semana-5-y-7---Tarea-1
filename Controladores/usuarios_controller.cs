using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Modelos;   //doy acceso a la carpeta
using Usuarios.Config;
using System.Data.SqlClient;
using System.Data;
using Usuarios.Helpers;
namespace Usuarios.Controladores
{
    class usuarios_controller
    {
        private usuario_model usuario_Model = new usuario_model();
        private readonly conexion cn = new conexion();
        private Encriptar encriptar = new Encriptar();
        public List<usuario_model> todos()
        {
            var lista_usuarios = new List<usuario_model>();
            using (var conexion = cn.obtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_ObtenerUsuarios", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var usuario = new usuario_model
                            {
                                Detalle_Rol = lector["Detalle"].ToString(),
                                Disponibilidad = (int)lector["Disponibilidad"],
                                Id_User = (int)lector["Id_User"],
                                Password = lector["Password"].ToString(),
                                Roles_id = (int)lector["Roles_id"],
                                Username = lector["Username"].ToString()
                            };
                            lista_usuarios.Add(usuario);
                        }
                    }
                }
            }
            return lista_usuarios;
        }

        public usuario_model uno(int Id_User)
        {
            using (var conexion = cn.obtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_ObtenerUsuarioPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id_User", Id_User);
                    using (var lector = comando.ExecuteReader())
                    {
                        if (!lector.Read()) return null;
                        return new usuario_model
                        {
                            Detalle_Rol = lector["Detalle"].ToString(),
                            Disponibilidad = (int)lector["Disponibilidad"],
                            Id_User = (int)lector["Id_User"],
                            Password = lector["Password"].ToString(),
                            Roles_id = (int)lector["Roles_id"],
                            Username = lector["Username"].ToString()
                        };
                    }
                }
            }
        }

        public string insertar(usuario_model usuario)
        {
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_InsertarUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Username", usuario.Username);
                    comando.Parameters.AddWithValue("@Password", encriptar.HashPassword(usuario.Password));
                    comando.Parameters.AddWithValue("@Disponibilidad", usuario.Disponibilidad);
                    comando.Parameters.AddWithValue("@Roles_id", usuario.Roles_id);
                    conexion.Open();
                    return comando.ExecuteNonQuery() != 0 ? "ok" : "error";
                }
            }
        }

        public string actualizar(usuario_model usuario)
        {
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_ActualizarUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id_User", usuario.Id_User);
                    comando.Parameters.AddWithValue("@Username", usuario.Username);
                    comando.Parameters.AddWithValue("@Password", encriptar.HashPassword(usuario.Password));
                    comando.Parameters.AddWithValue("@Disponibilidad", usuario.Disponibilidad);
                    comando.Parameters.AddWithValue("@Roles_id", usuario.Roles_id);
                    conexion.Open();
                    return comando.ExecuteNonQuery() != 0 ? "ok" : "error";
                }
            }
        }

        public string eliminar(int Id_User)
        {
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_EliminarUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id_User", Id_User);
                    conexion.Open();
                    return comando.ExecuteNonQuery() != 0 ? "ok" : "error";
                }
            }
        }


    }
}
