using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ControlProductos.Entity
{
    public class GetMenuByUsuarioResult_
    {
        public List<Menu> GetMenuByUsuarioResult { get; set; }
    }

    public class Menu
    {
        public int MenuId { get; set; }
        public int RaizMenuId { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string URL { get; set; }
    }
}