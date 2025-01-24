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

namespace Usuarios.Vistas.Roles
{
    public partial class CURoles : UserControl
    {
        private roles_controller rolesController = new roles_controller();
        public CURoles()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var rol = new roles_model
            {
                Detalle = txtDetalle.Text
            };

            var resultado = rolesController.insertarRol(rol);

            if (resultado == "ok")
            {
                MessageBox.Show("Rol guardado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarListaRoles();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar el rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (lst_Roles.SelectedItem is roles_model rolSeleccionado)
            {
                rolSeleccionado.Detalle = txtDetalle.Text;

                var resultado = rolesController.actualizarRol(rolSeleccionado);

                if (resultado == "ok")
                {
                    MessageBox.Show("Rol actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaRoles();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al actualizar el rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un rol de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lst_Roles.SelectedItem is roles_model rolSeleccionado)
            {
                var resultado = rolesController.eliminarRol(rolSeleccionado.Rol_Id);

                if (resultado == "ok")
                {
                    MessageBox.Show("Rol eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaRoles();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un rol de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void ActualizarListaRoles()
        {
            lst_Roles.DataSource = null;
            lst_Roles.DataSource = rolesController.todos();
            lst_Roles.DisplayMember = "Detalle"; // Muestra el detalle del rol en la lista.
        }

        private void LimpiarCampos()
        {
            txtDetalle.Clear();
        }
        private void CargarRoles()
        {
            var listaRoles = rolesController.todos();

            if (listaRoles.Count > 0)
            {
                lst_Roles.DataSource = null;
                lst_Roles.DataSource = listaRoles;
                lst_Roles.DisplayMember = "Detalle"; // Campo a mostrar.
                lst_Roles.ValueMember = "Rol_Id"; // Campo oculto para referencia.
            }
            else
            {
                MessageBox.Show("No hay roles disponibles para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lst_Roles.DataSource = null;
            }
        }

        private void CURoles_Load(object sender, EventArgs e)
        {
            CargarRoles();
        }
    }
}
