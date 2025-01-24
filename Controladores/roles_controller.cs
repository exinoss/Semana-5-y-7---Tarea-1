using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Modelos;   //doy acceso a la carpeta
using Usuarios.Config;
using System.Data.SqlClient;
using System.Data;
namespace Usuarios.Controladores
{
    class roles_controller
    {
        
        private readonly conexion cn = new conexion();
        public List<roles_model> todos()
        {
            var listaRoles = new List<roles_model>();
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_ObtenerRoles", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listaRoles.Add(new roles_model
                            {
                                Detalle = lector["Detalle"].ToString(),
                                Rol_Id = (int)lector["Rol_Id"]
                            });
                        }
                    }
                }
            }
            return listaRoles;
        }
        public roles_model uno(int Rol_Id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_ObtenerRolPorId", conexion)) // Asegúrate de que existe un procedimiento almacenado
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Rol_Id", Rol_Id);

                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new roles_model
                            {
                                Rol_Id = (int)lector["Rol_Id"],
                                Detalle = lector["Detalle"].ToString()
                            };
                        }
                        else
                        {
                            return null; // Retorna null si no encuentra el rol
                        }
                    }
                }
            }
        }

        public string insertarRol(roles_model rol)
        {
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_InsertarRol", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Detalle", rol.Detalle);
                    conexion.Open();
                    return comando.ExecuteNonQuery() != 0 ? "ok" : "error";
                }
            }
        }

        public string actualizarRol(roles_model rol)
        {
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_ActualizarRol", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Rol_Id", rol.Rol_Id);
                    comando.Parameters.AddWithValue("@Detalle", rol.Detalle);
                    conexion.Open();
                    return comando.ExecuteNonQuery() != 0 ? "ok" : "error";
                }
            }
        }

        public string eliminarRol(int Rol_Id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                using (var comando = new SqlCommand("sp_EliminarRol", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Rol_Id", Rol_Id);
                    conexion.Open();
                    return comando.ExecuteNonQuery() != 0 ? "ok" : "error";
                }
            }
        }

    }
}
