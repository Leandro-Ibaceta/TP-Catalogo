using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace CatalogoProyectoFinal
{
    public partial class VentanaAccion : Form
    {
        private Articulo articulo = null;
        public VentanaAccion()
        {
            InitializeComponent();
        }

        public VentanaAccion(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if(articulo == null)
                articulo = new Articulo();
            
            articulo.Codigo = txtBoxCodigo.Text;
            articulo.Nombre = txtBoxNombre.Text;
            articulo.Descripcion= txtBoxDescripcion.Text;
            articulo.UrlImagen = txtBoxUrlImagen.Text;
            articulo.Precio= decimal.Parse(txtBoxPrecio.Text);
            articulo.Categoria = (Marca)cboBoxCategoria.SelectedItem;
            articulo.Marca = (Marca)cboBoxMarca.SelectedItem;


        }

        private void VentanaAccion_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {

                cboBoxCategoria.DataSource = categoriaNegocio.Listar();
                cboBoxCategoria.ValueMember = "Id";
                cboBoxCategoria.DisplayMember = "Descripcion";
                cboBoxMarca.DataSource = marcaNegocio.Listar();
                cboBoxMarca.ValueMember = "Id";
                cboBoxMarca.DisplayMember = "Descripcion";

                if(articulo != null)
                {
                    txtBoxCodigo.Text = articulo.Codigo;
                    txtBoxNombre.Text = articulo.Nombre;
                    txtBoxDescripcion.Text = articulo.Descripcion;
                    txtBoxUrlImagen.Text = articulo.UrlImagen;
                    txtBoxPrecio.Text = articulo.Precio.ToString();
                    cboBoxMarca.SelectedValue = articulo.Marca.Id;
                    cboBoxCategoria.SelectedValue = articulo.Categoria.Id;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

    }
}
