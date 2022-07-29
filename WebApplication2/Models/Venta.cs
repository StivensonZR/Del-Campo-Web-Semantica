using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Venta
    {


        public int id_venta { get; set; }
        public int id_publi { get; set; }
        public string cod_venta { get; set; }
        public string precio { get; set; }
        public string total { get; set; }
        public string fecha { get; set; }
        public string comprador { get; set; }
        public string vendedor { get; set; }
        public string cantidad { get; set; }
        public string producto { get; set; }

        public List<Producto> productos { get; set; }


    }
}