using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using Worldpay.Sdk.Models;

namespace Worldpay.Sdk.Test
{
    //[TestClass]
    public class SettingsServiceTest
    {
        private SettingsService _settingsService;

        //[TestInitialize]
        //public void Setup()
        // for xunit, move from Setup method to class constructor
        public SettingsServiceTest()
        {
            var restClient = new WorldpayRestClient(Configuration.ServiceKey);

            _settingsService = restClient.GetSettingsService();
        }

        //[TestMethod]
        [Fact]
        public void TestGetSettings()
        {
            //SettingsResponse2 settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            SettingsResponse2 settings = _settingsService.GetSettings(Configuration.MerchantId);

            //Assert.IsNotNull(settings);
            Assert.NotNull(settings);
        }

        //[Ignore]
        //[TestMethod]
        [Fact(Skip = "???")]
        public void TestUpdateBillingSettings()
        {
            //_settingsService.UpdateRecurringBilling(Constants.MERCHANT_ID, true);
            _settingsService.UpdateRecurringBilling(Configuration.MerchantId, true);

            //SettingsResponse settings = _settingsService.GetSettings(MERCHANT_ID);
            SettingsResponse2 settings = _settingsService.GetSettings(Configuration.MerchantId);
            //Assert.AreEqual(true, settings.orderSetting.optInForRecurringBilling);
            Assert.True(settings.orderSetting.optInForRecurringBilling);

            //_settingsService.UpdateRecurringBilling(Constants.MERCHANT_ID, true);
            _settingsService.UpdateRecurringBilling(Configuration.MerchantId, true);

            //settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            settings = _settingsService.GetSettings(Configuration.MerchantId);
            //Assert.AreEqual(false, settings.orderSetting.optInForRecurringBilling);
            Assert.False(settings.orderSetting.optInForRecurringBilling);
        }

        //[TestMethod]
        [Fact]
        public void TestUpdateRiskSettings()
        {
            var riskSetting = new RiskSettingBE()
            {
                avsEnabled = false,
                cvcEnabled = true
            };

            //_settingsService.UpdateRiskSettings(Constants.MERCHANT_ID, riskSetting);
            _settingsService.UpdateRiskSettings(Configuration.MerchantId, riskSetting);

            //var settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            var settings = _settingsService.GetSettings(Configuration.MerchantId);
            //Assert.AreEqual(false, settings.riskSetting.avsEnabled);
            //Assert.AreEqual(true, settings.riskSetting.cvcEnabled);
            Assert.False(settings.riskSetting.avsEnabled);
            Assert.True(settings.riskSetting.cvcEnabled);

            riskSetting = new RiskSettingBE()
            {
                avsEnabled = true,
                cvcEnabled = false
            };
            //_settingsService.UpdateRiskSettings(Constants.MERCHANT_ID, riskSetting);
            _settingsService.UpdateRiskSettings(Configuration.MerchantId, riskSetting);

            //settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            settings = _settingsService.GetSettings(Configuration.MerchantId);
            //Assert.AreEqual(true, settings.riskSetting.avsEnabled);
            //Assert.AreEqual(false, settings.riskSetting.cvcEnabled);
            Assert.True(settings.riskSetting.avsEnabled);
            Assert.False(settings.riskSetting.cvcEnabled);
        }
    }
}

