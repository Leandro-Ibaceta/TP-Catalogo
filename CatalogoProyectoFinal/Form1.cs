using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace CatalogoProyectoFinal
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos = new List<Articulo>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarLista();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirVentana("Agregar Articulo");
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            AbrirVentana("Modificar Articulo");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirVentana("Eliminar Articulo");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void dgvCatalogo_CurrentCellChanged(object sender, EventArgs e)
        {
            if(dgvCatalogo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvCatalogo.CurrentRow.DataBoundItem;
                MostrarImagen(seleccionado.UrlImagen);
            }
        }


        //Metodos

        private void AbrirVentana(string nombreVentana)
        {
            VentanaAccion nuevaVentana = new VentanaAccion();
            nuevaVentana.Text = nombreVentana;
            nuevaVentana.ShowDialog();

        }

        private void MostrarImagen(string imagen)
        {
            try
            {
                pboxArticulos.Load(imagen);
            }
            catch (Exception)
            {

                pboxArticulos.Load("https://www.afim.com.eg/public/images/no-photo.png");
            }
        }

        private void CargarLista()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.ListarArticulos();
            dgvCatalogo.DataSource = listaArticulos;
            MostrarImagen(listaArticulos[0].UrlImagen);
        }

    }
}
