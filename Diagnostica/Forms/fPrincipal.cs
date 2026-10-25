using DAjalpan.Clases;
using DAjalpan.Clases.Modelos;
using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DAjalpan.Forms
{
    public partial class fPrincipal : Form
    {
        #region Variables
        private Configuraciones config;
        private Usuario usuario;
        #endregion

        public fPrincipal()
        {
            InitializeComponent();

            //foreach (Control ctrl in this.Controls)
            //{
            //    Utilerias.CambiarEstilos(ctrl);
            //}

            LeerConfiguracionesDeArchivo();

            customizeDesign();
            MostrarFormEnPanelContenedor(new frmVenta(this.config));

            //if (us.Administrador)
            //{
            //    pnlMenu.Visible = true;
            //    btnCerrarCorteTurno.Visible = true;
            //}
            //else
            //{
            //    if (us.TienePermiso(Permisos.Corte_de_Turno.ToString()))
            //    {
            //        pnlMenu.Visible = true;
            //        btnVenta.Visible = true;
            //        btnCerrarCorteTurno.Visible = true;
            //        btnCatalogos.Visible = false;
            //        btnHerramientas.Visible = false;
            //        pnlCatalogoSubMenu.Visible = false;
            //        btnHerramientas.Visible = true;
            //        pnlHerramientasSubMenu.Visible = true;
            //        btnInventario.Visible = false;
            //        btnTraspaso.Visible = false;

            //    }
            //    else
            //    {
            //        pnlMenu.Visible = false;
            //    }
            //}

        }

        private void customizeDesign()
        {
            pnlCatalogoSubMenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (pnlCatalogoSubMenu.Visible)
                pnlCatalogoSubMenu.Visible = false;
            if (pnlReportesSubMenu.Visible)
                pnlReportesSubMenu.Visible = false;
        }

        private void showSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubMenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        private void MostrarFormEnPanelContenedor(object frmhijo)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);

            Form fh = frmhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
        }
        #region Métodos
        #endregion

        private void btnCatalogos_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlCatalogoSubMenu);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlReportesSubMenu);
        }

        private void btnHerramientas_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlReportesSubMenu);
        }


        private void btnDesplegarMenu_Click(object sender, EventArgs e)
        {
            if (pnlMenu.Width == 200)
            {
                tmOcultarMenu.Enabled = true;
            }
            else if (pnlMenu.Width == 60)
            {
                tmMostrarMenu.Enabled = true;
            }

        }

        private void tmOcultarMenu_Tick(object sender, EventArgs e)
        {
            if (pnlMenu.Width <= 60)
            {
                tmOcultarMenu.Enabled = false;
            }
            else
            {
                pnlMenu.Width = pnlMenu.Width - 20;
            }
        }

        private void tmMostrarMenu_Tick(object sender, EventArgs e)
        {
            if (pnlMenu.Width >= 200)
            {
                tmMostrarMenu.Enabled = false;
            }
            else
            {
                pnlMenu.Width = pnlMenu.Width + 20;
            }
        }

        private void btnCerrarCorteTurno_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Utilerias.MostrarMensajeConfirmacion("¿Esta seguro de hacer el corte de turno?"))
                //{
                //    CorteTurnoDia ct = CorteTurnoNeg.Agregar(usuario.Id);
                //    MostrarFormEnPanelContenedor(new fCorteTurno(ct, usuario.Id));
                //}
            }
            catch (Exception ex)
            {
                //Utilerias.MostrarMensaje("No se pudo crear el corte de turno. Cierre el programa y vuelva a abrir e intente de nuevo hacer el corte de turno.");
            }
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            //MostrarFormEnPanelContenedor(new frmVenta(usuario));
        }

        /// <summary>
        /// Leer las configuraciones del sistema desde un archivo.
        /// </summary>
        public void LeerConfiguracionesDeArchivo()
        {
            var xmlSerializerR = new XmlSerializer(typeof(Configuraciones));

            using (var reader = XmlReader.Create(C.PathConfigFile))
            {
                this.config = xmlSerializerR.Deserialize(reader) as Configuraciones;

            }
        }
    }
}
