using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Models
{
    public class Bibliografia
    {
        public int IdBibliografia { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}