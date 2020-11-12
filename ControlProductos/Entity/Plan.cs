using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetPlansResult_
    {
        public List<Plan> GetPlansResult { get; set; }
    }
    public class GetCmbPlansResult_
    {
        public List<Plan> GetCmbPlansResult { get; set; }
    }

    public class DelPlanResult_
    {
        public int DelPlanResult { get; set; }
    }
    public class InsPlanResult_
    {
        public int InsPlanResult { get; set; }
    }
    public class UpdPlanResult_
    {
        public int UpdPlanResult { get; set; }
    }
    public class ValPlanResult_
    {
        public int ValPlanResult { get; set; }
    }
    public class DelPlanSelectedResult_
    {
        public int DelPlanSelectedResult { get; set; }
    }
    public class DelPlanAllResult_
    {
        public int DelPlanAllResult { get; set; }
    }

    public class Plan
    {
        public int PlanID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoYNombre { get; set; }
    }
}