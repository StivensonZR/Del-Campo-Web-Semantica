using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class Producto
    {

        public string Id_producto { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Peso { get; set; }
        public string Precio { get; set; }
        public string Stock { get; set; }
        public string Descripcion { get; set; }
        //public string imagen { get; set; }

        public List<Vendedor> Vend { get; set; }

    }
}