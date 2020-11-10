using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;


namespace ControlProductos.dataAccess
{
    public class MenuDa : Base
    {
        public List<Entity.Menu> GetMenu(int AplicacionId, int UsuarioId)
        {
            string json = methodGet("GetMenuByUsuario/" + AplicacionId.ToString()+ "/" + UsuarioId.ToString());
            Entity.GetMenuByUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.GetMenuByUsuarioResult_>(json);
            return regreso.GetMenuByUsuarioResult;
        }
    }
}