﻿using System;
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
    class accunt_coontroller
    {
        private usuario_model usuario_Model = new usuario_model();
        private readonly conexion cn = new conexion();
        public usuario_model login(string Username, string Password) {
                using (var conexion = cn.obtenerConexion())
                {
                    string cadena = $"select * from Usuarios " +
                    $"inner join Roles on Usuarios.Roles_id = Roles.Rol_Id " +
                    $"where Usuarios.Username = '{Username}'";
                    using (var comando = new SqlCommand(cadena, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                        if (lector.Read())
                        {
                            if (Password == lector["Password"].ToString())
                            {
                                return new usuario_model
                                {
                                    Id_User = (int)lector["Id_User"],
                                    Username = lector["Username"].ToString(),
                                    Password = lector["Password"].ToString(),
                                    Roles_id = (int)lector["Roles_id"],
                                    Detalle_Rol = lector["Detalle"].ToString(),
                                };
                            }
                            else {
                                return new usuario_model();
                            }
                        }
                        else {
                            return new usuario_model();
                        }
                                
                        }
                    }
                }
        }
    }
}
