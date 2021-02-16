using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class tipoDocumentoda : Base
    {
        public List<Entity.tipoDocumento> GetTiposDocumentos()
        {
            string json = methodGet("GetTiposDocumentos");
            Entity.GetTiposDocumentosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetTiposDocumentosResult_>(json);
            return regreso.GetTiposDocumentosResult;
        }
    }
}