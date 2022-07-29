using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApplication2.Models
{
    public class Usuario
    {

        public string Id_usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Identificacion { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }

        public List<Producto> productos { get; set; }
    }
}