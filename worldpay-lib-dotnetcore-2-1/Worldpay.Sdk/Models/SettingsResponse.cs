using System;
using System.Collections.Generic;

namespace Worldpay.Sdk.Models
{
    [Serializable]
    public class SettingsResponse
    {
        public List<Setting> keys { get; set; }

        public RiskSettingBE riskSetting { get; set; }

        public MerchantOrderSetting orderSetting { get; set; }

        public List<WebhookSetting> webhooks { get; set; }

        public string webhookKey { get; set; }
    }
}
