using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetCmbDepartamentoResult_
    {
        public List<Departamento> GetCmbDepartamentoResult { get; set; }
    }
    public class GetDepartamentosResult_
    {
        public List<Departamento> GetDepartamentosResult { get; set; }
    }

    public class DelDepartamentoResult_
    {
        public int DelDepartamentoResult { get; set; }
    }
    public class InsDepartamentoResult_
    {
        public int InsDepartamentoResult { get; set; }
    }
    public class UpdDepartamentoResult_
    {
        public int UpdDepartamentoResult { get; set; }
    }
    public class ValDepartamentoResult_
    {
        public int ValDepartamentoResult { get; set; }
    }
    public class DelDepartamentoSelectedResult_
    {
        public int DelDepartamentoSelectedResult { get; set; }
    }
    public class DelDepartamentoAllResult_
    {
        public int DelDepartamentoAllResult { get; set; }
    }

    public class Departamento
    {
        public int DeptoId { get; set; }
        public int IdArea { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string DscArea { get; set; }
        public bool Activo { get; set; }
        public string CodigoYNombre { get; set; }
    }
}