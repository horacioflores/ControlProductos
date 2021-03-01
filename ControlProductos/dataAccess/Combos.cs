using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class CombosDa : Base
    {
        public List<Entity.Combos> GetApprovalRoute()
        {
            string json = methodGet("GetApprovalRoute");
            Entity.GetApprovalRouteResult_ regreso = JsonConvert.DeserializeObject<Entity.GetApprovalRouteResult_>(json);
            return regreso.GetApprovalRouteResult;
        }
        public List<Entity.Combos> GetCostCenter()
        {
            string json = methodGet("GetCostCenter");
            Entity.GetCostCenterResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCostCenterResult_>(json);
            return regreso.GetCostCenterResult;
        }
        public List<Entity.Combos> GetAccounts()
        {
            string json = methodGet("GetAccounts");
            Entity.GetAccountsResult_ regreso = JsonConvert.DeserializeObject<Entity.GetAccountsResult_>(json);
            return regreso.GetAccountsResult;
        }
        public List<Entity.Combos> GetCurrency()
        {
            string json = methodGet("GetCurrency");
            Entity.GetCurrencyResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCurrencyResult_>(json);
            return regreso.GetCurrencyResult;
        }
        public List<Entity.Combos> GetUoM()
        {
            string json = methodGet("GetUoM");
            Entity.GetUoMResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUoMResult_>(json);
            return regreso.GetUoMResult;
        }
        public List<Entity.Combos> GetPlant()
        {
            string json = methodGet("GetPlant");
            Entity.GetPlantResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPlantResult_>(json);
            return regreso.GetPlantResult;
        }
        public List<Entity.Combos> GetLocDepartment()
        {
            string json = methodGet("GetLocDepartment");
            Entity.GetLocDepartmentResult_ regreso = JsonConvert.DeserializeObject<Entity.GetLocDepartmentResult_>(json);
            return regreso.GetLocDepartmentResult;
        }
        public List<Entity.Combos> GetLocEquip()
        {
            string json = methodGet("GetLocEquip");
            Entity.GetLocEquipResult_ regreso = JsonConvert.DeserializeObject<Entity.GetLocEquipResult_>(json);
            return regreso.GetLocEquipResult;
        }
        public List<Entity.Combos> GetGLClass()
        {
            string json = methodGet("GetGLClass");
            Entity.GetGLClassResult_ regreso = JsonConvert.DeserializeObject<Entity.GetGLClassResult_>(json);
            return regreso.GetGLClassResult;
        }
        public List<Entity.Combos> GetPurchaseCategory()
        {
            string json = methodGet("GetPurchaseCategory");
            Entity.GetPurchaseCategoryResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPurchaseCategoryResult_>(json);
            return regreso.GetPurchaseCategoryResult;
        }
        public List<Entity.Combos> GetLineType()
        {
            string json = methodGet("GetLineType");
            Entity.GetLineTypeResult_ regreso = JsonConvert.DeserializeObject<Entity.GetLineTypeResult_>(json);
            return regreso.GetLineTypeResult;
        }
        public List<Entity.Combos> GetVendor()
        {
            string json = methodGet("GetVendor");
            Entity.GetVendorResult_ regreso = JsonConvert.DeserializeObject<Entity.GetVendorResult_>(json);
            return regreso.GetVendorResult;
        }
        public List<Entity.Combos> GetVendorCountry()
        {
            string json = methodGet("GetVendorCountry");
            Entity.GetVendorCountryResult_ regreso = JsonConvert.DeserializeObject<Entity.GetVendorCountryResult_>(json);
            return regreso.GetVendorCountryResult;
        }
        public List<Entity.Combos> GetEMailBuyer()
        {
            string json = methodGet("GetEMailBuyer");
            Entity.GetEMailBuyerResult_ regreso = JsonConvert.DeserializeObject<Entity.GetEMailBuyerResult_>(json);
            return regreso.GetEMailBuyerResult;
        }
        public List<Entity.Combos> GetUserbyUsername()
        {
            string json = methodGet("GetUserbyUsername");
            Entity.GetUserbyUsernameResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUserbyUsernameResult_>(json);
            return regreso.GetUserbyUsernameResult;
        }
        public List<Entity.Combos> GetUserbyEmployeeNo()
        {
            string json = methodGet("GetUserbyEmployeeNo");
            Entity.GetUserbyEmployeeNoResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUserbyEmployeeNoResult_>(json);
            return regreso.GetUserbyEmployeeNoResult;
        }
        public List<Entity.Combos> GetOQ()
        {
            string json = methodGet("GetOQ");
            Entity.GetOQResult_ regreso = JsonConvert.DeserializeObject<Entity.GetOQResult_>(json);
            return regreso.GetOQResult;
        }
    }
}