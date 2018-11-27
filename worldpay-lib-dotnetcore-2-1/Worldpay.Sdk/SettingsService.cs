using System;
using Worldpay.Sdk.Models;

namespace Worldpay.Sdk
{
    /// <summary>
    /// Service for interacting with the Worldpay Order API
    /// </summary>
    [Serializable]
    public class SettingsService : AbstractService
    {
        private readonly string _baseUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsService(string baseUrl, Http http) : base(http)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        //public SettingsResponse GetSettings(string merchantId)
        //{
        //    var url = String.Format("{0}/merchants/{1}/settings", _baseUrl, merchantId);
        //    return Http.Get<SettingsResponse>(url);
        //}
        public SettingsResponse2 GetSettings(string merchantId)
        {
            //var url = String.Format("{0}/merchants/{1}/settings", _baseUrl, merchantId);
            var url = $"{_baseUrl}/merchants/{merchantId}/settings";
            return Http.Get<SettingsResponse2>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="riskSettings"></param>
        public void UpdateRiskSettings(string merchantId, RiskSettingBE riskSettings)
        {
            //var url = String.Format("{0}/merchants/{1}/settings/riskSettings", _baseUrl, merchantId);
            var url = $"{_baseUrl}/merchants/{merchantId}/settings/riskSettings";
            Http.Put<RiskSettingBE, RiskSettingBE>(url, riskSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="enable"></param>
        public void UpdateRecurringBilling(string merchantId, bool enable)
        {
            //var url = String.Format("{0}/merchants/{1}/settings/orderSettings/recurringBilling/{2}", _baseUrl, merchantId, enable);
            var url = $"{_baseUrl}/merchants/{merchantId}/settings/orderSettings/recurringBilling/{enable}";
            Http.Put<RiskSettingBE, RiskSettingBE>(url, null);
        }
    }
}
