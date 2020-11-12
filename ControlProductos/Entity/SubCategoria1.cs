using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetSubCategorias1Result_
    {
        public List<SubCategoria1> GetSubCategorias1Result { get; set; }
    }
    public class GetCmbSubCategorias1Result_
    {
        public List<SubCategoria1> GetCmbSubCategorias1Result { get; set; }
    }

    public class DelSubcategoria1Result_
    {
        public int DelSubcategoria1Result { get; set; }
    }
    public class InsSubcategoria1Result_
    {
        public int InsSubcategoria1Result { get; set; }
    }
    public class UpdSubcategoria1Result_
    {
        public int UpdSubcategoria1Result { get; set; }
    }
    public class ValSubcategoria1Result_
    {
        public int ValSubcategoria1Result { get; set; }
    }
    public class DelSubcategoria1SelectedResult_
    {
        public int DelSubcategoria1SelectedResult { get; set; }
    }
    public class DelSubcategoria1AllResult_
    {
        public int DelSubcategoria1AllResult { get; set; }
    }

    public class SubCategoria1
    {
        public int Subcategoria1ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoMaquina { get; set; }
        public string Maquina { get; set; }
        public string CodigoYNombre { get; set; }
    }
}