using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ControlProductos.Entity;

namespace ControlProductos
{
    public class BaseMaster : System.Web.UI.MasterPage
    {
        public loggedEmpleado LoginInfo
        {
            get
            {
                return (loggedEmpleado)Session["LoginInfo"];
            }
            set
            {
                Session["LoginInfo"] = value;
            }
        }
    }
}