using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Usuarios.Controladores;
using Usuarios.Modelos;
using Usuarios.Vistas.Roles;

namespace Usuarios.Vistas.Usuarios
{
    public partial class CUUsuarios : UserControl
    {
        private usuarios_controller usuariosController = new usuarios_controller();
        public CUUsuarios()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var usuario = new usuario_model
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Disponibilidad = chkDisponibilidad.Checked ? 1 : 0,
                Roles_id = int.Parse(cmbRoles.SelectedValue.ToString())
            };

            var resultado = usuariosController.insertar(usuario);

            if (resultado == "ok")
            {
                MessageBox.Show("Usuario guardado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarListaUsuarios();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lst_Usuarios.SelectedItem is usuario_model usuarioSeleccionado)
            {
                var resultado = usuariosController.eliminar(usuarioSeleccionado.Id_User);

                if (resultado == "ok")
                {
                    MessageBox.Show("Usuario eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaUsuarios();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (lst_Usuarios.SelectedItem is usuario_model usuarioSeleccionado)
            {
                usuarioSeleccionado.Username = txtUsername.Text;
                usuarioSeleccionado.Password = txtPassword.Text;
                usuarioSeleccionado.Disponibilidad = chkDisponibilidad.Checked ? 1 : 0;
                usuarioSeleccionado.Roles_id = int.Parse(cmbRoles.SelectedValue.ToString());

                var resultado = usuariosController.actualizar(usuarioSeleccionado);

                if (resultado == "ok")
                {
                    MessageBox.Show("Usuario actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaUsuarios();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            
        }
        private void ActualizarListaUsuarios()
        {
            lst_Usuarios.DataSource = null;
            lst_Usuarios.DataSource = usuariosController.todos();
            lst_Usuarios.DisplayMember = "Username"; // Muestra el nombre de usuario en la lista.
        }

        private void LimpiarCampos()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            chkDisponibilidad.Checked = false;
            cmbRoles.SelectedIndex = 0;
        }
    }
}
