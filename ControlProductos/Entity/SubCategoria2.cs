using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetSubCategorias2Result_
    {
        public List<SubCategoria2> GetSubCategorias2Result { get; set; }
    }
    public class GetCmbSubCategorias2Result_
    {
        public List<SubCategoria2> GetCmbSubCategorias2Result { get; set; }
    }

    public class DelSubcategoria2Result_
    {
        public int DelSubcategoria2Result { get; set; }
    }
    public class InsSubcategoria2Result_
    {
        public int InsSubcategoria2Result { get; set; }
    }
    public class UpdSubcategoria2Result_
    {
        public int UpdSubcategoria2Result { get; set; }
    }
    public class ValSubcategoria2Result_
    {
        public int ValSubcategoria2Result { get; set; }
    }
    public class DelSubcategoria2SelectedResult_
    {
        public int DelSubcategoria2SelectedResult { get; set; }
    }
    public class DelSubcategoria2AllResult_
    {
        public int DelSubcategoria2AllResult { get; set; }
    }

    public class SubCategoria2
    {
        public int Subcategoria2ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoMaquina { get; set; }
        public string Maquina { get; set; }
        public string CodigoSubcategoria1 { get; set; }
        public string Subcategoria1 { get; set; }
        public string CodigoYNombre { get; set; }
    }
}