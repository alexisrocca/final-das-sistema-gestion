using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_POO_Sistema_de_Gestion.Forms
{
    public partial class FrmSimTiempo : Form
    {
        public DateTime FechaSeleccionada { get; set; }
        public FrmSimTiempo()
        {
            InitializeComponent();
        }

        private void btnCambiarFecha_Click(object sender, EventArgs e)
        {
            this.FechaSeleccionada = dtpFecha.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
