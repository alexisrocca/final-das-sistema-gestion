using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_POO_Sistema_de_Gestion.Forms.Stock
{
    public partial class FrmSeleccionFecha : Form
    {
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public FrmSeleccionFecha()
        {
            InitializeComponent();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            this.fechaInicio = mcRangoFechas.SelectionStart;
            this.fechaFin = mcRangoFechas.SelectionEnd;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
