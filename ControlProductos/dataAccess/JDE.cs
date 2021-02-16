using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class JDEda : Base
    {
        public List<Entity.JDE> GetCombo()
        {
            string json = methodGet("GetJDEs");
            Entity.GetJDEsResult_ regreso = JsonConvert.DeserializeObject<Entity.GetJDEsResult_>(json);
            return regreso.GetJDEsResult;
        }
    }
}