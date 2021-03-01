using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetCombosResult_
    {
        public List<Combos> GetCombosResult { get; set; }
    }

    public class GetApprovalRouteResult_
    {
        public List<Combos> GetApprovalRouteResult { get; set; }
    }

    public class GetCostCenterResult_
    {
        public List<Combos> GetCostCenterResult { get; set; }
    }

    public class GetAccountsResult_
    {
        public List<Combos> GetAccountsResult { get; set; }
    }

    public class GetCurrencyResult_
    {
        public List<Combos> GetCurrencyResult { get; set; }
    }

    public class GetUoMResult_
    {
        public List<Combos> GetUoMResult { get; set; }
    }

    public class GetPlantResult_
    {
        public List<Combos> GetPlantResult { get; set; }
    }

    public class GetLocDepartmentResult_
    {
        public List<Combos> GetLocDepartmentResult { get; set; }
    }

    public class GetLocEquipResult_
    {
        public List<Combos> GetLocEquipResult { get; set; }
    }
    public class GetGLClassResult_
    {
        public List<Combos> GetGLClassResult { get; set; }
    }

    public class GetPurchaseCategoryResult_
    {
        public List<Combos> GetPurchaseCategoryResult { get; set; }
    }

    public class GetLineTypeResult_
    {
        public List<Combos> GetLineTypeResult { get; set; }
    }

    public class GetVendorResult_
    {
        public List<Combos> GetVendorResult { get; set; }
    }

    public class GetVendorCountryResult_
    {
        public List<Combos> GetVendorCountryResult { get; set; }
    }

    public class GetEMailBuyerResult_
    {
        public List<Combos> GetEMailBuyerResult { get; set; }
    }

    public class GetUserbyUsernameResult_
    {
        public List<Combos> GetUserbyUsernameResult { get; set; }
    }

    public class GetUserbyEmployeeNoResult_
    {
        public List<Combos> GetUserbyEmployeeNoResult { get; set; }
    }

    public class GetOQResult_
    {
        public List<Combos> GetOQResult { get; set; }
    }

    public class Combos
    {
        public string ValorCombo { get; set; }
        public string TextoCombo { get; set; }

        //public string ValorYTexto { get { return ValorCombo + " - " + TextoCombo; } }
    }
}