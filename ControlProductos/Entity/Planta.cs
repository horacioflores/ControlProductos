using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetPlantasResult_
    {
        public List<Planta> GetPlantasResult { get; set; }
    }
    public class GetCmbPlantaResult_
    {
        public List<Planta> GetCmbPlantaResult { get; set; }
    }

    public class DelPlantaResult_
    {
        public int DelPlantaResult { get; set; }
    }
    public class InsPlantaResult_
    {
        public int InsPlantaResult { get; set; }
    }
    public class UpdPlantaResult_
    {
        public int UpdPlantaResult { get; set; }
    }
    public class ValPlantaResult_
    {
        public int ValPlantaResult { get; set; }
    }
    public class DelPlantaSelectedResult_
    {
        public int DelPlantaSelectedResult { get; set; }
    }
    public class DelPlantaAllResult_
    {
        public int DelPlantaAllResult { get; set; }
    }

    public class Planta
    {
        public int PlantaId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public string CodigoYNombre { get; set; }
    }
}