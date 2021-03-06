﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class ControlProductosda : Base
    {
        public List<Entity.ControlProductos> GetCtrlProd_MisSolicitudes(string fechaini, string fechafin, string folio,string codigoSolicitante)
        {
            string json = methodGet("GetCtrlProd_MisSolicitudes/" + fechaini + "/" + fechafin + "/" + folio + "/" + codigoSolicitante);
            Entity.GetCtrlProd_MisSolicitudesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProd_MisSolicitudesResult_>(json);
            return regreso.GetCtrlProd_MisSolicitudesResult;
        }
        
        public List<Entity.ControlProductos> GetCtrlProd_MisPendientes(string fechaini, string fechafin, string folio, string codigoPerfil)
        {
            string json = methodGet("GetCtrlProd_MisPendientes/" + fechaini + "/" + fechafin + "/" + folio + "/" + codigoPerfil);
            Entity.GetCtrlProd_MisPendientesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProd_MisPendientesResult_>(json);
            return regreso.GetCtrlProd_MisPendientesResult;
        }

        public List<Entity.ControlProductos> GetCtrlProd_ProductosVigentes(string fechaini, string fechafin, string folio, string codigoPerfil)
        {
            string json = methodGet("GetCtrlProd_ProductosVigentes/" + fechaini + "/" + fechafin + "/" + folio + "/" + codigoPerfil);
            Entity.GetCtrlProd_ProductosVigentesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProd_ProductosVigentesResult_>(json);
            return regreso.GetCtrlProd_ProductosVigentesResult;
        }

        public List<Entity.ControlProductos> GetCtrlProd(string fechaini, string fechafin, string folio)
        {
            string json = methodGet("GetCtrlProd/" + fechaini + "/" + fechafin + "/" + folio );
            Entity.GetCtrlProdResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProdResult_>(json);
            return regreso.GetCtrlProdResult;
        }

        public List<Entity.ControlProductos> GetCombo()
        {
            string json = methodGet("GetCtrlProdCombo");
            Entity.GetCtrlProdComboResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProdComboResult_>(json);
            return regreso.GetCtrlProdComboResult;
        }

        public List<Entity.ControlProductos> GetCtrlProducto(string ctrlProdsID)
        {
            string json = methodGet("GetCtrlProducto/" + ctrlProdsID);
            Entity.GetCtrlProductoResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProductoResult_>(json);
            return regreso.GetCtrlProductoResult;
        }

        public int InsCtrlP(Entity.ControlProductos ctrlProds, string UsuarioId)
        {
            int regreso = Convert.ToInt32(methodPost("InsCtrlP/" + UsuarioId, JsonConvert.SerializeObject(ctrlProds)));
            return regreso;
        }

        public int UpdCtrlP(Entity.ControlProductos ctrlProds, string UsuarioId)
        {
            int regreso = Convert.ToInt32(methodPost("UpdCtrlP/" + UsuarioId, JsonConvert.SerializeObject(ctrlProds)));
            return regreso;
        }

        public int changeSts(string idctrlProds, string UsuarioId, string sigPerfil, string DM,string codigoArticulo, string aprobacion, string comentario)
        {
            int regreso = Convert.ToInt32(methodPost("changeSts/"+ idctrlProds + "/" + UsuarioId + "/" + sigPerfil + "/" + DM + "/" + codigoArticulo+ "/" + aprobacion + "/" + comentario));
            return regreso;
        }

        public string newDoc()
        {
            string json = methodGet("newDocCtrlProd");
            Entity.newDocResult_ regreso = JsonConvert.DeserializeObject<Entity.newDocResult_>(json);
            return regreso.newDocResult;
        }

        public int ValProducto(string ctrlProdsID, string codigoArticulo, string codigoProducto)
        {
            int regreso = Convert.ToInt32(methodPost("ValProducto/" + ctrlProdsID + "/" + codigoArticulo + "/" + codigoProducto));
            return regreso;
        }

        public int ValProducto2(string codigoProducto)
        {
            int regreso = Convert.ToInt32(methodPost("ValProducto2/" + codigoProducto));
            return regreso;
        }
    }
}