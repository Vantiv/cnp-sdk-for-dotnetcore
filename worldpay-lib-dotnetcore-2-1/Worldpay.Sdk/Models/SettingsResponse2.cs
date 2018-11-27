using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worldpay.Sdk.Models
{
    public class SettingsResponse2
    {
        public List<KeyBE> keys { get; set; }
        public RiskSettingBE riskSetting { get; set; }
        public OrderSettingBE orderSetting { get; set; }
        public string webhookKey { get; set; }
        public List<TemplateBE> templates { get; set; }
        public ApmSettingBE apmSetting { get; set; }
        public List<SettlementDetailBE> settlementDetails { get; set; }
        public CardtypeSettingsBE cardTypeSettings { get; set; }
        public string serviceLevel { get; set; }
    }

    public class RiskSettingBE
    {
        public bool cvcEnabled { get; set; }
        public string cvcSetting { get; set; }
        public bool avsEnabled { get; set; }
        public string avsSetting { get; set; }
    }

    public class OrderSettingBE
    {
        public bool optInForRecurringBilling { get; set; }
        public bool motoEnabled { get; set; }
        public bool autoCaptureEnabled { get; set; }
        public string authorisationSetting { get; set; }
    }

    public class ApmSettingBE
    {
        public string defaultShopperCountryCode { get; set; }
    }

    public class CardtypeSettingsBE
    {
        public bool AMEX { get; set; }
        public bool DINERS { get; set; }
        public bool JCB { get; set; }
        public bool MAESTRO { get; set; }
        public bool VISA_CREDIT { get; set; }
        public bool VISA_CORPORATE_CREDIT { get; set; }
        public bool VISA_DEBIT { get; set; }
        public bool ELECTRON { get; set; }
        public bool VISA_CORPORATE_DEBIT { get; set; }
        public bool MASTERCARD_CREDIT { get; set; }
        public bool MASTERCARD_CORPORATE_CREDIT { get; set; }
        public bool MASTERCARD_DEBIT { get; set; }
        public bool MASTERCARD_CORPORATE_DEBIT { get; set; }
        public bool CARTEBLEUE { get; set; }
    }

    public class KeyBE
    {
        public string keyType { get; set; }
        public string environment { get; set; }
        public bool enabled { get; set; }
        public string key { get; set; }
    }

    public class TemplateBE
    {
        public string id { get; set; }
        public string templateType { get; set; }
        public string templateName { get; set; }
        public string templateCode { get; set; }
        public string template { get; set; }
        public bool enabled { get; set; }
        public string environment { get; set; }
        public bool partnerTemplate { get; set; }
    }

    public class SettlementDetailBE
    {
        public string settlementCurrency { get; set; }
    }

}
