using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class Vendedor
    {

        public string Id_vendedor { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string documento { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }

        public List<Producto> productos { get; set; }


    }
}