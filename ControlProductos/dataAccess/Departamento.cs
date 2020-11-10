using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class DepartamentoDa : Base
    { 
        public List<Entity.Departamento> GetCombo()
        {
            string json = methodGet("GetCmbDepartamento");
            Entity.GetCmbDepartamentoResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbDepartamentoResult_>(json);
            return regreso.GetCmbDepartamentoResult;
        }

        public List<Entity.Departamento> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetDepartamentos/"+Codigo+"/" +Descripcion+ "/" +Activo.ToString());
            Entity.GetDepartamentosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetDepartamentosResult_>(json);
            return regreso.GetDepartamentosResult;
        }

        public int DelDepartamento(int IdUser, int IdDepartamento)
        {
            Entity.Departamento dep = new Entity.Departamento();
            dep.DeptoId = IdDepartamento;

            Entity.DelDepartamentoResult_ regreso = JsonConvert.DeserializeObject<Entity.DelDepartamentoResult_>(methodPost("DelDepartamento/" + IdUser.ToString(), JsonConvert.SerializeObject(dep)));
            return regreso.DelDepartamentoResult;
        }

        public int InsDepartamento(int IdUser, string Codigo, string Descripcion)
        {
            Entity.Departamento dep = new Entity.Departamento();
            dep.Codigo = Codigo;
            dep.Descripcion = Descripcion;

            Entity.InsDepartamentoResult_ regreso = JsonConvert.DeserializeObject<Entity.InsDepartamentoResult_>(methodPost("InsDepartamento/" + IdUser.ToString(), JsonConvert.SerializeObject(dep)));
            return regreso.InsDepartamentoResult;
        }

        public int UpdDepartamento(int IdUser, int DeptoId, string Codigo, string Descripcion)
        {
            Entity.Departamento dep = new Entity.Departamento();
            dep.DeptoId = DeptoId;
            dep.Codigo = Codigo;
            dep.Descripcion = Descripcion;

            Entity.UpdDepartamentoResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdDepartamentoResult_>(methodPost("UpdDepartamento/" + IdUser.ToString(), JsonConvert.SerializeObject(dep)));
            return regreso.UpdDepartamentoResult;
        }

        public int ValDepartamento(int IdDepartamento, string Codigo, string Descripcion)
        {
            Entity.Departamento dep = new Entity.Departamento();
            dep.DeptoId = IdDepartamento;
            dep.Codigo = Codigo;
            dep.Descripcion = Descripcion;

            int regreso =Convert.ToInt32(methodPost("ValDepartamento", JsonConvert.SerializeObject(dep)));
            return regreso;
        }

        public int DelDepartamentoSelected(int IdUser, string Valores, bool Activo)
        {
            Entity.DelDepartamentoSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelDepartamentoSelectedResult_>(methodPost("DelDepartamentoSelected/" + IdUser.ToString() + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelDepartamentoSelectedResult;
        }

        public int DelDepartamentoAll(int IdUser, bool Activo)
        {
            Entity.DelDepartamentoAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelDepartamentoAllResult_>(methodPost("DelDepartamentoAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelDepartamentoAllResult;
        }
    }
}