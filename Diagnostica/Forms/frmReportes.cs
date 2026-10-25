using DAjalpan.Clases;
using DAjalpan.Clases.Modelos;
using DAjalpan.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DAjalpan.Forms
{
    public partial class frmReportes : Form
    {
        #region Variables
        CorteTurnoNeg corteTurnoNeg = new CorteTurnoNeg();
        private static string C_CLASE = "frmReportes";
        private Configuraciones config = null;
        #endregion
        public frmReportes(Configuraciones configuracion)
        {
            InitializeComponent();

            /*Cambiar estilos*/
            foreach (Control gb in this.Controls)
            {
                if (gb is GroupBox)
                {
                    gb.ForeColor = C.COLOR_SISTEMA;
                    foreach (Control ctrl in gb.Controls)
                    {
                        if (ctrl is Button)
                        {
                            ctrl.BackColor = C.COLOR_SISTEMA;
                            ctrl.ForeColor = C.COLOR_SISTEMA_CLARO;
                        }
                        else ctrl.ForeColor = C.COLOR_SISTEMA;
                    }
                }
                else if (gb is DataGridView)
                {
                    DataGridView gv = (DataGridView)gb;
                    gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
                    gv.ForeColor = C.COLOR_SISTEMA;
                    gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
            /*Termina cambiar estilos*/

            this.config = configuracion;
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                List<int> anios = new List<int>();
                for (int i = 2026; i <= DateTime.Now.Year; i++)
                {
                    anios.Add(i);
                }

                cmbAnios.DataSource = anios;               
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "frmReportes_Load", "");
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "reporte_" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".pdf";
                string filePath = C.PathTEMP + fileName;
                DateTime fechaIni = Convert.ToDateTime(cmbCorteIni.Text);
                DateTime fechaFin = Convert.ToDateTime(cmbCorteFin.Text);
                byte[] bytesPDF = null;
                if (rbDoctores.Checked)
                    bytesPDF = corteTurnoNeg.CrearReporteDoctorPDF(fechaIni, fechaFin, this.config.Empresa);
                else if (rbFamiliaEstudios.Checked)
                {
                    bytesPDF = corteTurnoNeg.CrearReporteFamiliaEstudiosPDF(fechaIni, fechaFin, this.config.Empresa);
                }
                else if (rbEstudios.Checked)
                {
                    bytesPDF = corteTurnoNeg.CrearReporteEstudiosPDF(fechaIni, fechaFin, this.config.Empresa);
                }

                if (bytesPDF != null)
                {
                    Utilerias.Imprimir(bytesPDF, filePath);
                    Utilerias.MostrarMensaje("Reporte generado con éxito");
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnGenerar_Click", "");
                Utilerias.MostrarMensaje("¡Ocurrió un problema intenetelo mas trade!");
            }
        }

        private void cmbAnios_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*OBTENER TODOS LOS CORTES */
            cmbCorteIni.DataSource = corteTurnoNeg.ObtenerTodosFechaInicio(Convert.ToInt32(cmbAnios.SelectedItem));
            cmbCorteFin.DataSource = corteTurnoNeg.ObtenerTodosFechaFinal(Convert.ToInt32(cmbAnios.SelectedItem));
            cmbCorteIni.DisplayMember = "FechaCreacion";
            cmbCorteFin.DisplayMember = "FechaFinal";
            cmbCorteFin.ValueMember = cmbCorteIni.ValueMember = "Id";
            cmbCorteFin.SelectedItem = cmbCorteIni.SelectedItem = null;
        }
    }
}
