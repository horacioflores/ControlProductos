using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetPosicionesResult_
    {
        public List<Posicion> GetPosicionesResult { get; set; }
    }
    public class GetCmbPosicionesResult_
    {
        public List<Posicion> GetCmbPosicionesResult { get; set; }
    }

    public class DelPosicionResult_
    {
        public int DelPosicionResult { get; set; }
    }
    public class InsPosicionResult_
    {
        public int InsPosicionResult { get; set; }
    }
    public class UpdPosicionResult_
    {
        public int UpdPosicionResult { get; set; }
    }
    public class ValPosicionResult_
    {
        public int ValPosicionResult { get; set; }
    }
    public class DelPosicionSelectedResult_
    {
        public int DelPosicionSelectedResult { get; set; }
    }
    public class DelPosicionAllResult_
    {
        public int DelPosicionAllResult { get; set; }
    }

    public class Posicion
    {
        public int PosicionId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public bool Activo { get; set; }
        public string CodigoYNombre { get; set; }
    }
}