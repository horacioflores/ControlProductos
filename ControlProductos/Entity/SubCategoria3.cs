using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetSubCategorias3Result_
    {
        public List<SubCategoria3> GetSubCategorias3Result { get; set; }
    }
    public class GetCmbSubCategorias3Result_
    {
        public List<SubCategoria3> GetCmbSubCategorias3Result { get; set; }
    }

    public class DelSubcategoria3Result_
    {
        public int DelSubcategoria3Result { get; set; }
    }
    public class InsSubcategoria3Result_
    {
        public int InsSubcategoria3Result { get; set; }
    }
    public class UpdSubcategoria3Result_
    {
        public int UpdSubcategoria3Result { get; set; }
    }
    public class ValSubcategoria3Result_
    {
        public int ValSubcategoria3Result { get; set; }
    }
    public class DelSubcategoria3SelectedResult_
    {
        public int DelSubcategoria3SelectedResult { get; set; }
    }
    public class DelSubcategoria3AllResult_
    {
        public int DelSubcategoria3AllResult { get; set; }
    }

    public class SubCategoria3
    {
        public int Subcategoria3ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoMaquina { get; set; }
        public string Maquina { get; set; }
        public string CodigoSubcategoria1 { get; set; }
        public string Subcategoria1 { get; set; }
        public string CodigoSubcategoria2 { get; set; }
        public string Subcategoria2 { get; set; }
        public string CodigoYNombre { get; set; }
    }
}