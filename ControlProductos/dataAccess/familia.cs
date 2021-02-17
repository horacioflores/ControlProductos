using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;


namespace ControlProductos.dataAccess
{
    public class familiada : Base
    {
        public List<Entity.familia> GetCombo()
        {
            string json = methodGet("Getfamilias");
            Entity.GetfamiliasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetfamiliasResult_>(json);
            return regreso.GetfamiliasResult;
        }
    }
}