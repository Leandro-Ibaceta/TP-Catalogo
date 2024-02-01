using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> ListarArticulos()
        {
            List<Articulo> lista = new List<Articulo>();

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("Select A.Id, Codigo, A.Nombre , A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, A.ImagenUrl, A.Precio from Articulos A, Marcas M, Categorias C where M.Id = A.IdMarca and C.Id = A.IdCategoria");
                datos.EjecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Marca();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

            
        }

        public void AgregarArticulo(Articulo articulo)
        {
            AccesoDatos negocio = new AccesoDatos();

            try
            {
                negocio.SetearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) Values('@codigo', '@nombre', '@descripcion', @idMarca ,@idCategoria, '@imagenUrl', @precio)");
                negocio.SetearParametro("codigo", articulo.Codigo);
                negocio.SetearParametro("nombre", articulo.Nombre);
                negocio.SetearParametro("descripcion", articulo.Descripcion);
                negocio.SetearParametro("idMarca", (object)articulo.Marca.Id);
                negocio.SetearParametro("idCategoria", (object)articulo.Categoria.Id);
                negocio.SetearParametro("imagenUrl", articulo.UrlImagen);
                negocio.SetearParametro("precio", articulo.Precio);

                negocio.EjecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                negocio.CerrarConexion();
            }
        }

        public void EliminarArticulo(int id)
        {
            AccesoDatos negocio = new AccesoDatos();

            try
            {
                negocio.SetearConsulta("Delete from ARTICULOS where Id = @id");
                negocio.SetearParametro("id", id);

                negocio.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                negocio.CerrarConexion();
            }
        }
    }
}
