using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class ActFijoda : Base
    {
        public List<Entity.ActFijos> GetCombo()
        {
            string json = methodGet("GetActFijos");
            Entity.GetActFijosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetActFijosResult_>(json);
            return regreso.GetActFijosResult;
        }
    }
}