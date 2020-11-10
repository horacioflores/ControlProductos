using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetMaquinasResult_
    {
        public List<Maquina> GetMaquinasResult { get; set; }
    }
    public class GetCmbMaquinasResult_
    {
        public List<Maquina> GetCmbMaquinasResult { get; set; }
    }

    public class DelMaquinaResult_
    {
        public int DelMaquinaResult { get; set; }
    }
    public class InsMaquinaResult_
    {
        public int InsMaquinaResult { get; set; }
    }
    public class UpdMaquinaResult_
    {
        public int UpdMaquinaResult { get; set; }
    }
    public class ValMaquinaResult_
    {
        public int ValMaquinaResult { get; set; }
    }
    public class DelMaquinaSelectedResult_
    {
        public int DelMaquinaSelectedResult { get; set; }
    }
    public class DelMaquinaAllResult_
    {
        public int DelMaquinaAllResult { get; set; }
    }

    public class Maquina
    {
        public int MaquinaID { get; set; }   
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoYNombre { get; set; }
    }
}