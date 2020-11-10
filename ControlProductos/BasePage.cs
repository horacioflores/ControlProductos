using ControlProductos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace ControlProductos
{
    public class BasePage : System.Web.UI.Page
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
        public BasePage()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");

            Load += Page_Load;
        }


        public Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.ToString().Contains("login.aspx"))
            {
                if (LoginInfo == null)
                {
                    Response.Clear();
                    Response.Write("<script language=javascript>window.parent.location = 'login.aspx?par=TimeOut'</script>");
                    Response.Flush();
                }
            }
        }
    }
}