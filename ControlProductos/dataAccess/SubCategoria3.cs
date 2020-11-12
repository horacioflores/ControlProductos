using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class SubCategoria3Da : Base
    {
        public List<Entity.SubCategoria3> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetSubCategorias3/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetSubCategorias3Result_ regreso = JsonConvert.DeserializeObject<Entity.GetSubCategorias3Result_>(json);
            return regreso.GetSubCategorias3Result;
        }

        public List<Entity.SubCategoria3> GetCombo()
        {
            string json = methodGet("GetCmbSubCategorias3");
            Entity.GetCmbSubCategorias3Result_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbSubCategorias3Result_>(json);
            return regreso.GetCmbSubCategorias3Result;
        }

        public int DelSubCategoria3(int IdUser, int IdSubCategoria3)
        {
            Entity.DelSubcategoria3Result_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria3Result_>(methodPost("DelSubcategoria3/" + IdUser.ToString() + "/" + IdSubCategoria3.ToString()));
            return regreso.DelSubcategoria3Result;
        }

        public int InsSubCategoria3(int IdUser, string Codigo, string Nombre, string CodigoMaquina, string CodigoSubcategoria1, string CodigoSubcategoria2)
        {
            Entity.SubCategoria3 scat3 = new Entity.SubCategoria3();
            scat3.Codigo = Codigo;
            scat3.Nombre = Nombre;
            scat3.CodigoMaquina = CodigoMaquina;
            scat3.CodigoSubcategoria1 = CodigoSubcategoria1;
            scat3.CodigoSubcategoria2 = CodigoSubcategoria2;

            Entity.InsSubcategoria3Result_ regreso = JsonConvert.DeserializeObject<Entity.InsSubcategoria3Result_>(methodPost("InsSubcategoria3/" + IdUser.ToString(), JsonConvert.SerializeObject(scat3)));
            return regreso.InsSubcategoria3Result;
        }

        public int UpdSubCategoria3(int IdUser, int SubCategoria3Id, string Codigo, string Nombre, string CodigoMaquina, string CodigoSubcategoria1, string CodigoSubcategoria2)
        {
            Entity.SubCategoria3 scat3 = new Entity.SubCategoria3();
            scat3.Subcategoria3ID = SubCategoria3Id;
            scat3.Codigo = Codigo;
            scat3.Nombre = Nombre;
            scat3.CodigoMaquina = CodigoMaquina;
            scat3.CodigoSubcategoria1 = CodigoSubcategoria1;
            scat3.CodigoSubcategoria2 = CodigoSubcategoria2;

            Entity.UpdSubcategoria3Result_ regreso = JsonConvert.DeserializeObject<Entity.UpdSubcategoria3Result_>(methodPost("UpdSubcategoria3/" + IdUser.ToString(), JsonConvert.SerializeObject(scat3)));
            return regreso.UpdSubcategoria3Result;
        }

        public int ValSubCategoria3(int IdSubCategoria3, string Codigo, string Descripcion)
        {
            Entity.ValSubcategoria3Result_ regreso = JsonConvert.DeserializeObject<Entity.ValSubcategoria3Result_>(methodPost("ValSubcategoria3/" + IdSubCategoria3.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValSubcategoria3Result;
        }

        public int DelSubCategoria3Selected(int IdUser, string Valores)
        {
            Entity.DelSubcategoria3SelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria3SelectedResult_>(methodPost("DelSubcategoria3Selected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelSubcategoria3SelectedResult;
        }

        public int DelSubCategoria3All(int IdUser, bool Activo)
        {
            Entity.DelSubcategoria3AllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria3AllResult_>(methodPost("DelSubcategoria3All/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelSubcategoria3AllResult;
        }
    }
}