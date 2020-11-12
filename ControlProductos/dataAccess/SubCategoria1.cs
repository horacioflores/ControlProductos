using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class SubCategoria1Da : Base
    {
        public List<Entity.SubCategoria1> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetSubCategorias1/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetSubCategorias1Result_ regreso = JsonConvert.DeserializeObject<Entity.GetSubCategorias1Result_>(json);
            return regreso.GetSubCategorias1Result;
        }

        public List<Entity.SubCategoria1> GetCombo()
        {
            string json = methodGet("GetCmbSubCategorias1");
            Entity.GetCmbSubCategorias1Result_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbSubCategorias1Result_>(json);
            return regreso.GetCmbSubCategorias1Result;
        }

        public int DelSubCategoria1(int IdUser, int IdSubCategoria1)
        {
            Entity.DelSubcategoria1Result_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria1Result_>(methodPost("DelSubcategoria1/" + IdUser.ToString() + "/" + IdSubCategoria1.ToString()));
            return regreso.DelSubcategoria1Result;
        }

        public int InsSubCategoria1(int IdUser, string Codigo, string Nombre, string CodigoMaquina)
        {
            Entity.SubCategoria1 scat1 = new Entity.SubCategoria1();
            scat1.Codigo = Codigo;
            scat1.Nombre = Nombre;
            scat1.CodigoMaquina = CodigoMaquina;

            Entity.InsSubcategoria1Result_ regreso = JsonConvert.DeserializeObject<Entity.InsSubcategoria1Result_>(methodPost("InsSubcategoria1/" + IdUser.ToString(), JsonConvert.SerializeObject(scat1)));
            return regreso.InsSubcategoria1Result;
        }

        public int UpdSubCategoria1(int IdUser, int SubCategoria1Id, string Codigo, string Nombre, string CodigoMaquina)
        {
            Entity.SubCategoria1 scat1 = new Entity.SubCategoria1();
            scat1.Subcategoria1ID = SubCategoria1Id;
            scat1.Codigo = Codigo;
            scat1.Nombre = Nombre;
            scat1.CodigoMaquina = CodigoMaquina;

            Entity.UpdSubcategoria1Result_ regreso = JsonConvert.DeserializeObject<Entity.UpdSubcategoria1Result_>(methodPost("UpdSubcategoria1/" + IdUser.ToString(), JsonConvert.SerializeObject(scat1)));
            return regreso.UpdSubcategoria1Result;
        }

        public int ValSubCategoria1(int IdSubCategoria1, string Codigo, string Descripcion)
        {
            Entity.ValSubcategoria1Result_ regreso = JsonConvert.DeserializeObject<Entity.ValSubcategoria1Result_>(methodPost("ValSubcategoria1/" + IdSubCategoria1.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValSubcategoria1Result;
        }

        public int DelSubCategoria1Selected(int IdUser, string Valores)
        {
            Entity.DelSubcategoria1SelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria1SelectedResult_>(methodPost("DelSubcategoria1Selected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelSubcategoria1SelectedResult;
        }

        public int DelSubCategoria1All(int IdUser, bool Activo)
        {
            Entity.DelSubcategoria1AllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelSubcategoria1AllResult_>(methodPost("DelSubcategoria1All/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelSubcategoria1AllResult;
        }
    }
}