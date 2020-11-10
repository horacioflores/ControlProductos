using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DevExpress.Web;
using ControlProductos.dataAccess;
using ControlProductos.Entity;
using ControlProductos.models;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace ControlProductos
{
    public partial class Principal : BaseMaster
    {
        string UploadDirectory = "~/Assets/images/users/";
        int IdApplication = Convert.ToInt32(ConfigurationManager.AppSettings["IdApplication"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cookies.Clear();

            int PerfilId = LoginInfo.CurrentPerfil.PerfilId;
            if (PerfilId != 0)
            {

                imgProfile.Src = UploadDirectory +  LoginInfo.CurrentUsuario.ImagenUrl;// cambiar el UploadDirectory para que benga de la bd
                imbPhotoBig.Src = UploadDirectory + LoginInfo.CurrentUsuario.ImagenUrl; // cambiar el UploadDirectory para que benga de la bd

                lblUsuario.Text = LoginInfo.CurrentUsuario.NombreCompleto;
                lblDepartamento.Text = LoginInfo.CurrentUsuario.Departamento;
                lblPerfil.Text = LoginInfo.CurrentPerfil.Descripcion;

                int AplicacionId = IdApplication;
                int IdUsuario = LoginInfo.CurrentUsuario.UsuarioId;
                //int IdPerfile = LoginInfo.CurrentPerfil.PerfilId;

                var BMenu = new MenuDa();
                var Nivel = 0;
                var oListMenu = BMenu.GetMenu(AplicacionId, IdUsuario);
                var myMenuItemList = new HtmlGenericControl();

                HtmlGenericControl myMenuUl_ItemOptions = new HtmlGenericControl("ul");
                myMenuUl_ItemOptions.Attributes.Add("class", "list-unstyled");

                var Max = oListMenu.Count();
                var cuenta = 0;
                GuardarMenusList(Nivel, oListMenu, myMenuItemList, Max, cuenta);
            }
        }

        public void GuardarMenusList(int Raiz, List<Entity.Menu> oListMenu, HtmlGenericControl myMenuItemList, int max, int cuenta)
        {
            if (cuenta < max)
            {
                var MainMenu = oListMenu.Where(m => m.RaizMenuId == Raiz);
                int rows = MainMenu.Count();

                //Encabezados.            
                foreach (Entity.Menu myMenu in MainMenu)
                {
                    cuenta = cuenta + 1;

                    if (Raiz == 0)
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "has_sub");

                        //Encabezado.
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "#");
                        anchor.Attributes.Add("class", "waves-effect waves-light");
                        HtmlGenericControl efect = new HtmlGenericControl("em");
                        efect.Attributes.Add("class", myMenu.Imagen);
                        anchor.Controls.Add(efect);

                        //Nota: MyMenu.Imagen es un campo en la base de datos donde almacenamos el icono que mostraremos en el menu.
                        //Mas imagenes en:http://zavoloklom.github.io/material-design-iconic-font/v1/icons.html#action

                        //Imagenes usadas en el Menu: http://fontawesome.io/examples/

                        HtmlGenericControl spannombre = new HtmlGenericControl("span");
                        spannombre.InnerText = myMenu.Nombre;
                        anchor.Controls.Add(spannombre);

                        HtmlGenericControl span2 = new HtmlGenericControl("span");
                        span2.Attributes.Add("class", "pull-right");
                        HtmlGenericControl i2 = new HtmlGenericControl("i");
                        i2.Attributes.Add("class", "md md-add");
                        span2.Controls.Add(i2);
                        anchor.Controls.Add(span2);
                        li.Controls.Add(anchor);

                        var Regs = oListMenu.Where(m => m.RaizMenuId == myMenu.MenuId);
                        if (Regs.Count() == 0)
                        {
                            feedbackTab.Controls.Add(li);
                        }
                        else
                        {
                            myMenuItemList = li;

                            HtmlGenericControl ulAtri = new HtmlGenericControl("ul");
                            ulAtri.Attributes.Add("class", "list-unstyled");

                            var Opciones = oListMenu.Where(m => m.RaizMenuId == myMenu.MenuId);
                            foreach (Entity.Menu myMenuItems in Opciones)
                            {
                                HtmlGenericControl li2 = new HtmlGenericControl("li");
                                HtmlGenericControl a2 = new HtmlGenericControl("a");
                                a2.InnerText = myMenuItems.Nombre;
                                a2.Attributes.Add("href", myMenuItems.URL);
                                li2.Controls.Add(a2);
                                ulAtri.Controls.Add(li2);
                            }

                            myMenuItemList.Controls.Add(ulAtri);
                            feedbackTab.Controls.Add(myMenuItemList);
                        }
                    }
                    GuardarMenusList(myMenu.MenuId, oListMenu, myMenuItemList, max, cuenta);
                }
            }
        }

        public void DisableCriticalJavaScriptFiles()
        {
            HeadContent.Visible = false;
        }
    }
}