using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class SubCategoria2Da : Base
    {
        public List<Entity.SubCategoria2> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetSubCategorias2/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetSubCategorias2Result_ regreso = JsonConvert.DeserializeObject<Entity.GetSubCategorias2Result_>(json);
            return regreso.GetSubCategorias2Result;
        }

        public List<Entity.SubCategoria2> GetCombo()
        {
            string json = methodGet("GetCmbSubCategorias2");
            Entity.GetCmbSubCategorias2Result_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbSubCategorias2Result_>(json);
            return regreso.GetCmbSubCategorias2Result;
        }

        public int DelSubCategoria2(int IdUser, int IdSubCategoria2)
        {
            Entity.DelSubcategoria2Result_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria2Result_>(methodPost("DelSubcategoria2/" + IdUser.ToString() + "/" + IdSubCategoria2.ToString()));
            return regreso.DelSubcategoria2Result;
        }

        public int InsSubCategoria2(int IdUser, string Codigo, string Nombre, string CodigoMaquina, string CodigoSubcategoria1)
        {
            Entity.SubCategoria2 scat2 = new Entity.SubCategoria2();
            scat2.Codigo = Codigo;
            scat2.Nombre = Nombre;
            scat2.CodigoMaquina = CodigoMaquina;
            scat2.CodigoSubcategoria1 = CodigoSubcategoria1;

            Entity.InsSubcategoria2Result_ regreso = JsonConvert.DeserializeObject<Entity.InsSubcategoria2Result_>(methodPost("InsSubcategoria2/" + IdUser.ToString(), JsonConvert.SerializeObject(scat2)));
            return regreso.InsSubcategoria2Result;
        }

        public int UpdSubCategoria2(int IdUser, int SubCategoria2Id, string Codigo, string Nombre, string CodigoMaquina, string CodigoSubcategoria1)
        {
            Entity.SubCategoria2 scat2 = new Entity.SubCategoria2();
            scat2.Subcategoria2ID = SubCategoria2Id;
            scat2.Codigo = Codigo;
            scat2.Nombre = Nombre;
            scat2.CodigoMaquina = CodigoMaquina;
            scat2.CodigoSubcategoria1 = CodigoSubcategoria1;

            Entity.UpdSubcategoria2Result_ regreso = JsonConvert.DeserializeObject<Entity.UpdSubcategoria2Result_>(methodPost("UpdSubcategoria2/" + IdUser.ToString(), JsonConvert.SerializeObject(scat2)));
            return regreso.UpdSubcategoria2Result;
        }

        public int ValSubCategoria2(int IdSubCategoria2, string Codigo, string Descripcion)
        {
            Entity.ValSubcategoria2Result_ regreso = JsonConvert.DeserializeObject<Entity.ValSubcategoria2Result_>(methodPost("ValSubcategoria2/" + IdSubCategoria2.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValSubcategoria2Result;
        }

        public int DelSubCategoria2Selected(int IdUser, string Valores)
        {
            Entity.DelSubcategoria2SelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria2SelectedResult_>(methodPost("DelSubcategoria2Selected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelSubcategoria2SelectedResult;
        }

        public int DelSubCategoria2All(int IdUser, bool Activo)
        {
            Entity.DelSubcategoria2AllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria2AllResult_>(methodPost("DelSubcategoria2All/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelSubcategoria2AllResult;
        }
    }
}