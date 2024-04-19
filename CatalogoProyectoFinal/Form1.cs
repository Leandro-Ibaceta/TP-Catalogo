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
            VentanaAccion ventana = new VentanaAccion();
            ventana.Text = "Agregar Articulo";
            ventana.ShowDialog();

            CargarLista();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo modificar;
            modificar = (Articulo)dgvCatalogo.CurrentRow.DataBoundItem;
            VentanaAccion ventana = new VentanaAccion(modificar);
            ventana.Text = "Modificar Articulo";
            
            ventana.ShowDialog();

            CargarLista();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("¿Esta seguro que quiere eliminarlo?","Eliminacion de articulo" ,MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo seleccionado = (Articulo)dgvCatalogo.CurrentRow.DataBoundItem;

                    negocio.EliminarArticulo(seleccionado.Id);
                    CargarLista();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
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
        //Muestra la lista de articulos en la ventana principal
        private void CargarLista()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.ListarArticulos();
            dgvCatalogo.DataSource = listaArticulos;
            MostrarImagen(listaArticulos[0].UrlImagen);
            OcultarColumna();
            
        }

        private void OcultarColumna()
        {
            dgvCatalogo.Columns["UrlImagen"].Visible = false;
        }

    }
}
