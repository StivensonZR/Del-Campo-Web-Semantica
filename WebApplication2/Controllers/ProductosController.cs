using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VDS.RDF.Query;
using WebApplication2.Models;


namespace DelCampoWebSemantica.Controllers
{
    public class InicioController : Controller
    {

        private static SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/Del_Campo_WS/sparql"));


        // GET: Inicio
        public ActionResult index()
        {
            return View();
        }


        public ActionResult ofertas()
        {     
            List<Producto> listaP = new List<Producto>();
            SparqlResultSet resultados = endpoint.QueryWithResultSet(
                "PREFIX dato: <http://www.semanticweb.org/usuario/ontologies/2022/6/campo#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?Imagen ?Nombre ?Peso ?Precio ?Stock ?p " +
                "WHERE { " +
                    "?p rdf:type dato:Producto. " +
                    "?p dato:Imagen ?Imagen. " +
                    "?p dato:NombreProducto ?Nombre. " +
                    "?p dato:Peso ?Peso. " +
                    "?p dato:Precio ?Precio. " +
                    "?p dato:Stock ?Stock. " +
                "}"
                );
            foreach (var result in resultados.Results)
            {
                Producto producto = new Producto();

                var datos = result.ToList();
                producto.Imagen = datos[0].Value.ToString();
                producto.Nombre = datos[1].Value.ToString();
                producto.Peso = datos[2].Value.ToString();
                producto.Precio = datos[3].Value.ToString().Substring(0, datos[3].ToString().IndexOf("^") - 9);
                producto.Stock = datos[4].Value.ToString().Substring(0, 2); 
                producto.Id_producto = datos[5].Value.ToString();

                listaP.Add(producto);
            }
            return View(listaP);
          
        }

        public ActionResult detalleOferta(string id_producto)
        {

            if (id_producto == null)
            {
                return RedirectToAction("ofertas", "Inicio");
            }

            Producto producto = new Producto();
            SparqlResultSet resultados = endpoint.QueryWithResultSet(
                "PREFIX dato: <http://www.semanticweb.org/usuario/ontologies/2022/6/campo#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?Imagen ?Nombre ?Categoria ?Peso ?Precio ?Stock ?Descripcion " +
                "WHERE { " +
                    "<" + id_producto + "> rdf:type dato:Producto. " +
                    "<" + id_producto + "> dato:Imagen ?Imagen. " +
                    "<" + id_producto + "> dato:NombreProducto ?Nombre. " +
                    "<" + id_producto + "> dato:Categoria ?Categoria. " +
                    "<" + id_producto + "> dato:Peso ?Peso. " +
                    "<" + id_producto + "> dato:Precio ?Precio. " +
                    "<" + id_producto + "> dato:Stock ?Stock. " +
                    "<" + id_producto + "> dato:Descripción ?Descripcion. " +
                    
                "}"
               );

            var datosP = resultados.Results.Single().ToList();

            producto.Imagen = datosP[0].Value.ToString();
            producto.Nombre = datosP[1].Value.ToString();
            producto.Categoria = datosP[2].Value.ToString();
            producto.Peso = datosP[3].Value.ToString();
            producto.Precio = datosP[4].Value.ToString().Substring(0, datosP[4].ToString().IndexOf("^") - 9);
            producto.Stock = datosP[5].Value.ToString().Substring(0, 2);
            producto.Descripcion = datosP[6].Value.ToString();

            
            List<Vendedor> vendedor1 = new List<Vendedor>();
            SparqlResultSet ven = endpoint.QueryWithResultSet(
                "PREFIX dato: <http://www.semanticweb.org/usuario/ontologies/2022/6/campo#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?NombreV ?p " +
                "WHERE {" +
                    "?p rdf:type dato:Vendedor. " +
                    "?p dato:NombreVendedor ?NombreV. " +
                    "?p dato:VendeA <" + id_producto + ">. " +
                "}"
                );
            foreach (var result in ven.Results)
            {
                Vendedor vendedor = new Vendedor();
                var d = result.ToList();
                vendedor.Nombre = d[0].Value.ToString();
                vendedor.Id_vendedor = d[1].Value.ToString();

                vendedor1.Add(vendedor);
            }

            producto.Vend = vendedor1;
            
            return View(producto);
           
        }
        
        public ActionResult vendedores()
        {
            List<Vendedor> listaP = new List<Vendedor>();
            SparqlResultSet resultados = endpoint.QueryWithResultSet(
                "PREFIX dato: <http://www.semanticweb.org/usuario/ontologies/2022/6/campo#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?Nombre ?Apellidos ?Identificacion ?Telefono ?Direccion ?Correo ?p " +
                "WHERE { " +
                    "?p rdf:type dato:Vendedor. " +
                    "?p dato:NombreVendedor ?Nombre. " +
                    "?p dato:ApellidoVendedor ?Apellidos. " +
                    "?p dato:No_Documento ?Identificacion. " +
                    "?p dato:Telefono ?Telefono. " +
                    "?p dato:DirecciónVendedor ?Direccion. " +
                    "?p dato:Correo ?Correo. " +
                "}"
                );
            foreach (var result in resultados.Results)
            {
                Vendedor vendedor = new Vendedor();

                var datos = result.ToList();
                vendedor.Nombre = datos[0].Value.ToString();
                vendedor.Apellidos = datos[1].Value.ToString();
                vendedor.Identificacion = datos[2].Value.ToString().Substring(0, 8);
                vendedor.Telefono = datos[3].Value.ToString().Substring(0, 10); ;
                vendedor.Direccion = datos[4].Value.ToString();
                vendedor.Correo = datos[5].Value.ToString();
                vendedor.Id_vendedor = datos[6].Value.ToString();

                listaP.Add(vendedor);
   
            }
            return View(listaP);
        }
/*
        public ActionResult detalleVendedor(string id_vendedor)
        {

            if (id_vendedor == null)
            {
                return RedirectToAction("vendedores", "Inicio");
            }

            Vendedor vendedor = new Vendedor();
            SparqlResultSet resultados = endpoint.QueryWithResultSet(
                "PREFIX dato: <http://www.semanticweb.org/usuario/ontologies/2022/6/campo#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?Imagen ?Nombre ?Apellido ?Doc ?Telefono ?Direccion ?Correo " +
                "WHERE { " +
                    "<" + id_vendedor + "> rdf:type dato:Vendedor. " +
                    "<" + id_vendedor + "> dato:Imagen ?Imagen. " +
                    "<" + id_vendedor + "> dato:NombreVendedor ?Nombre. " +
                    "<" + id_vendedor + "> dato:ApellidoVendedor ?Apellidos. " +
                    "<" + id_vendedor + "> dato:No_Documento ?Doc. " +
                    "<" + id_vendedor + "> dato:Telefono ?Telefono. " +
                    "<" + id_vendedor + "> dato:DirecciónVendedor ?Direccion. " +
                    "<" + id_vendedor + "> dato:Correo ?Correo. " +

                "}"
               );

          
            var datosP = resultados.Results.Single().ToList();

            vendedor.Nombre = datosP[0].Value.ToString();
            vendedor.Apellidos = datosP[1].Value.ToString();
            vendedor.Identificacion = datosP[2].Value.ToString();
            vendedor.Telefono = datosP[3].Value.ToString().Substring(0, datosP[3].ToString().IndexOf("^") - 9);
            vendedor.Direccion = datosP[4].Value.ToString().Substring(0, 2);
            vendedor.Correo = datosP[5].Value.ToString();
            vendedor.Id_vendedor = datosP[6].Value.ToString();

            return View(vendedor);
        }



        public ActionResult ListaUsuarios()
        {
            List<Usuario> listaP = new List<Usuario>();
            SparqlResultSet resultados = endpoint.QueryWithResultSet(
                "PREFIX dato: <http://www.semanticweb.org/juan/ontologies/2022/6/Del_Campo_WS#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?Nombre ?Apellidos ?Identificacion ?Telefono ?Direccion ?p" +
                "WHERE { " +
                    "?p rdf:type dato:Usuario. " +
                    "?p dato:Nombre ?Nombre. " +
                    "?p dato:Apellidos ?Apellidos. " +
                    "?p dato:Identificacion ?Identificacion. " +
                    "?p dato:Telefono ?Telefono. " +
                    "?p dato:Direccion ?Direccion. " +
                "}"
                 );
            foreach (var result in resultados.Results)
            {
                Usuario usuario = new Usuario();
                var datos = result.ToList();
                usuario.Nombre = datos[0].Value.ToString();
                usuario.Apellidos = datos[1].Value.ToString();
                usuario.Telefono = Int16.Parse(datos[2].Value.ToString().Substring(0, datos[3].ToString().IndexOf("^") - 7));
                usuario.Identificacion = Int16.Parse(datos[3].Value.ToString().Substring(0, datos[3].ToString().IndexOf("^") - 7));
                usuario.Direccion = datos[4].Value.ToString();
                usuario.Id_usuario = datos[5].Value.ToString();

                listaP.Add(usuario);
            }
            return View(listaP);
        }

 */

        public ActionResult acercaDelCampo()
        {
            return View();
        }

        public ActionResult servicioCliente()
        {
            return View();
        }



    }
}