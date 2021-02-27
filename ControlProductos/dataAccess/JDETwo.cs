using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class JDETwoda : Base
    {
        public List<Entity.JDETwo> GetCombo()
        {
            string json = methodGet("GetJDETwos");
            Entity.GetJDETwosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetJDETwosResult_>(json);
            return regreso.GetJDETwosResult;
        }
    }
}