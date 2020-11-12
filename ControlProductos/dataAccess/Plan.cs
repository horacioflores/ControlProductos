using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class PlanDa : Base
    {
        public List<Entity.Plan> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetPlans/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetPlansResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPlansResult_>(json);
            return regreso.GetPlansResult;
        }

        public List<Entity.Plan> GetCombo()
        {
            string json = methodGet("GetCmbPlans");
            Entity.GetCmbPlansResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbPlansResult_>(json);
            return regreso.GetCmbPlansResult;
        }

        public int DelPlan(int IdUser, int IdPlan)
        {
            Entity.DelPlanResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlanResult_>(methodPost("DelPlan/" + IdUser.ToString() + "/" + IdPlan.ToString()));
            return regreso.DelPlanResult;
        }

        public int InsPlan(int IdUser, string Codigo, string Nombre)
        {
            Entity.Plan util = new Entity.Plan();
            util.Codigo = Codigo;
            util.Nombre = Nombre;

            Entity.InsPlanResult_ regreso = JsonConvert.DeserializeObject<Entity.InsPlanResult_>(methodPost("InsPlan/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.InsPlanResult;
        }

        public int UpdPlan(int IdUser, int PlanId, string Codigo, string Nombre)
        {
            Entity.Plan util = new Entity.Plan();
            util.PlanID = PlanId;
            util.Codigo = Codigo;
            util.Nombre = Nombre;

            Entity.UpdPlanResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdPlanResult_>(methodPost("UpdPlan/" + IdUser.ToString(), JsonConvert.SerializeObject(util)));
            return regreso.UpdPlanResult;
        }

        public int ValPlan(int IdPlan, string Codigo, string Descripcion)
        {
            Entity.ValPlanResult_ regreso = JsonConvert.DeserializeObject<Entity.ValPlanResult_>(methodPost("ValPlan/" + IdPlan.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValPlanResult;
        }

        public int DelPlanSelected(int IdUser, string Valores)
        {
            Entity.DelPlanSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlanSelectedResult_>(methodPost("DelPlanSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelPlanSelectedResult;
        }

        public int DelPlanAll(int IdUser, bool Activo)
        {
            Entity.DelPlanAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelPlanAllResult_>(methodPost("DelPlanAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelPlanAllResult;
        }
    }
}