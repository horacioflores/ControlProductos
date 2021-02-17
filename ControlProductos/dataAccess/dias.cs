using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class diasda : Base
    {
        public List<Entity.dias> GetCombo()
        {
            string json = methodGet("Getdias");
            Entity.GetdiasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetdiasResult_>(json);
            return regreso.GetdiasResult;
        }
    }
}