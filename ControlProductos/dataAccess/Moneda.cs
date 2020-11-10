using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class MonedaDa : Base
    {
        public List<Entity.Moneda> GetCatalog(string Codigo, string Descripcion, bool Activo)
        {
            string json = methodGet("GetMonedas/" + Codigo + "/" + Descripcion + "/" + Activo.ToString());
            Entity.GetMonedasResult_ regreso = JsonConvert.DeserializeObject<Entity.GetMonedasResult_>(json);
            return regreso.GetMonedasResult;
        }

        public List<Entity.Moneda> GetCombo()
        {
            string json = methodGet("GetCmbMoneda");
            Entity.GetCmbMonedaResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbMonedaResult_>(json);
            return regreso.GetCmbMonedaResult;
        }

        public int DelMoneda(int IdUser, int IdMoneda)
        {
            Entity.DelMonedaResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMonedaResult_>(methodPost("DelMoneda/" + IdUser.ToString() + "/" + IdMoneda.ToString()));
            return regreso.DelMonedaResult;
        }

        public int InsMoneda(int IdUser, string Codigo, string Nombre, string Simbolo, decimal precs, string sep_millar, string sep_decimal)
        {
            Entity.Moneda moneda = new Entity.Moneda();
            moneda.Codigo = Codigo;
            moneda.Nombre = Nombre;
            moneda.Simbolo = Simbolo;
            moneda.precs = precs;
            moneda.sep_millar = sep_millar;
            moneda.sep_decimal = sep_decimal;

            Entity.InsMonedaResult_ regreso = JsonConvert.DeserializeObject<Entity.InsMonedaResult_>(methodPost("InsMoneda/" + IdUser.ToString(), JsonConvert.SerializeObject(moneda)));
            return regreso.InsMonedaResult;
        }

        public int UpdMoneda(int IdUser, int MonedaId, string Codigo, string Nombre, string Simbolo, decimal precs, string sep_millar, string sep_decimal)
        {
            Entity.Moneda moneda = new Entity.Moneda();
            moneda.MonedaID = MonedaId;
            moneda.Codigo = Codigo;
            moneda.Nombre = Nombre;
            moneda.Simbolo = Simbolo;
            moneda.precs = precs;
            moneda.sep_millar = sep_millar;
            moneda.sep_decimal = sep_decimal;

            Entity.UpdMonedaResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdMonedaResult_>(methodPost("UpdMoneda/" + IdUser.ToString(), JsonConvert.SerializeObject(moneda)));
            return regreso.UpdMonedaResult;
        }

        public int ValMoneda(int IdMoneda, string Codigo, string Descripcion)
        {
            Entity.ValMonedaResult_ regreso = JsonConvert.DeserializeObject<Entity.ValMonedaResult_>(methodPost("ValMoneda/" + IdMoneda.ToString() + "/" + Codigo + "/" + Descripcion));
            return regreso.ValMonedaResult;
        }

        public int DelMonedaSelected(int IdUser, string Valores)
        {
            Entity.DelMonedaSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMonedaSelectedResult_>(methodPost("DelMonedaSelected/" + IdUser.ToString() + "/" + Valores));
            return regreso.DelMonedaSelectedResult;
        }

        public int DelMonedaAll(int IdUser, bool Activo)
        {
            Entity.DelMonedaAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelMonedaAllResult_>(methodPost("DelMonedaAll/" + IdUser.ToString() + "/" + Activo.ToString()));
            return regreso.DelMonedaAllResult;
        }

    }
}