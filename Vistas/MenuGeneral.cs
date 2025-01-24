using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Usuarios.Vistas.Roles;
using Usuarios.Vistas.Usuarios;

namespace Usuarios.Vistas
{
    public partial class MenuGeneral : Form
    {
        public MenuGeneral()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CUUsuarios Usuarios = new CUUsuarios();
            splitContainer1.Panel2.Controls.Clear();
            Usuarios.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(Usuarios);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm_Reporte_Usuarios = new Reportes.ResporteUsuarios();
            frm_Reporte_Usuarios.ShowDialog();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            CURoles Roles = new CURoles();
            splitContainer1.Panel2.Controls.Clear();
            Roles.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(Roles);
        }

        
    }
}
