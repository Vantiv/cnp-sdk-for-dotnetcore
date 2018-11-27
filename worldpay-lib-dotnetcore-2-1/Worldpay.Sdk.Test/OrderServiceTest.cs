using System;
using System.Collections.Generic;

using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using Worldpay.Sdk.Enums;
using Worldpay.Sdk.Models;

namespace Worldpay.Sdk.Test
{
    // https://xunit.github.io/docs/comparisons.html
    //[TestClass]
    public class OrderServiceTest
    {
        // Authorization service, for obtaining access tokens
        private AuthService _authService;
        // Order service, for handling interaction with the order API
        private OrderService _orderService;
        // Settings service, for handling interaction with the settings API
        private SettingsService _settingsService;

        /// <summary>
        /// Initialise the service clients
        /// </summary>
        //[TestInitialize]
        //public void Setup()
        // for xunit, move from Setup method to class constructor
        public OrderServiceTest()
        {
            var restClient = new WorldpayRestClient(Configuration.ServiceKey);

            _authService = restClient.GetAuthService();
            _orderService = restClient.GetOrderService();
            _settingsService = restClient.GetSettingsService();
        }

        /// <summary>
        /// Verify that creating an order works for a valid token
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldCreateOrderForValidToken()
        {
            OrderRequest orderRequest = createOrderRequest();
            orderRequest.token = CreateToken();

            OrderResponse response = _orderService.Create(orderRequest);

            //Assert.IsNotNull(response.orderCode);
            //Assert.AreEqual(1999, response.amount);
            //Assert.IsNotNull(response.customerIdentifiers);

            Assert.NotNull(response.orderCode);
            Assert.Equal(1999, response.amount);
            Assert.NotNull(response.customerIdentifiers);
        }

        //[TestMethod]
        [Fact]
        public void ShouldCreateTelephoneOrder()
        {
            OrderRequest orderRequest = createOrderRequest();
            orderRequest.token = CreateToken();
            orderRequest.orderType = OrderType.MOTO.ToString();

            OrderResponse response = _orderService.Create(orderRequest);

            //Assert.IsNotNull(response.orderCode);
            //Assert.AreEqual(1999, response.amount);
            //Assert.IsNotNull(response.customerIdentifiers);

            Assert.NotNull(response.orderCode);
            Assert.Equal(1999, response.amount);
            Assert.NotNull(response.customerIdentifiers);
        }

        //[TestMethod]
        [Fact]
        public void ShouldCreateAuthorizationRequest()
        {
            //SettingsResponse2 settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            SettingsResponse2 settings = _settingsService.GetSettings(Configuration.MerchantId);

            try
            {
                OrderRequest orderRequest = createOrderRequest();
                orderRequest.token = CreateToken();
                orderRequest.authorizeOnly = true;

                OrderResponse response = _orderService.Create(orderRequest);

                //Assert.IsNotNull(response.orderCode);
                //Assert.AreEqual(1999, response.authorizedAmount);
                //Assert.IsTrue(response.authorizeOnly);
                //Assert.AreEqual(OrderStatus.AUTHORIZED, response.paymentStatus);

                Assert.NotNull(response.orderCode);
                Assert.Equal(1999, response.authorizedAmount);
                Assert.True(response.authorizeOnly);
                Assert.Equal(OrderStatus.AUTHORIZED, response.paymentStatus);
            }
            catch (Worldpay.Sdk.WorldpayException ex)
            {
                // we expect: Worldpay.Sdk.WorldpayException: API error: Authorize only orders not allowed for auto capture merchants
                if (!settings.orderSetting.autoCaptureEnabled)
                {
                    throw ex;
                }
            }
        }

        //[TestMethod]
        [Fact]
        public void ShouldCapturePaymentForAuthorizedOrder()
        {
            //SettingsResponse2 settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            SettingsResponse2 settings = _settingsService.GetSettings(Configuration.MerchantId);

            try
            {
                OrderRequest orderRequest = createOrderRequest();
                orderRequest.token = CreateToken();
                orderRequest.authorizeOnly = true;

                string orderCode = _orderService.Create(orderRequest).orderCode;

                OrderResponse response = _orderService.CaptureAuthorizedOrder(orderCode);

                //Assert.IsNotNull(response.orderCode);
                //Assert.AreEqual(1999, response.authorizedAmount);
                //Assert.AreEqual(1999, response.amount);
                //Assert.AreEqual(OrderStatus.SUCCESS, response.paymentStatus);

                Assert.NotNull(response.orderCode);
                Assert.Equal(1999, response.authorizedAmount);
                Assert.Equal(1999, response.amount);
                Assert.Equal(OrderStatus.SUCCESS, response.paymentStatus);
            }
            catch (Worldpay.Sdk.WorldpayException ex)
            {
                // we expect: Worldpay.Sdk.WorldpayException: API error: Authorize only orders not allowed for auto capture merchants
                if (!settings.orderSetting.autoCaptureEnabled)
                {
                    throw ex;
                }
            }
        }

        //[TestMethod]
        [Fact]
        public void ShouldPartiallyCapturePaymentForAuthorizedOrder()
        {
            //SettingsResponse2 settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            SettingsResponse2 settings = _settingsService.GetSettings(Configuration.MerchantId);

            try
            {
                OrderRequest orderRequest = createOrderRequest();
                orderRequest.token = CreateToken();
                orderRequest.authorizeOnly = true;

                string orderCode = _orderService.Create(orderRequest).orderCode;

                OrderResponse response = _orderService.CaptureAuthorizedOrder(orderCode, 500);

                //Assert.IsNotNull(response.orderCode);
                //Assert.AreEqual(1999, response.authorizedAmount);
                //Assert.AreEqual(500, response.amount);
                //Assert.AreEqual(OrderStatus.SUCCESS, response.paymentStatus);

                Assert.NotNull(response.orderCode);
                Assert.Equal(1999, response.authorizedAmount);
                Assert.Equal(500, response.amount);
                Assert.Equal(OrderStatus.SUCCESS, response.paymentStatus);
            }
            catch (Worldpay.Sdk.WorldpayException ex)
            {
                // we expect: Worldpay.Sdk.WorldpayException: API error: Authorize only orders not allowed for auto capture merchants
                if (!settings.orderSetting.autoCaptureEnabled)
                {
                    throw ex;
                }
            }
        }

        //[TestMethod]
        [Fact]
        public void ShouldCancelPaymentForAuthorizedOrder()
        {
            //SettingsResponse2 settings = _settingsService.GetSettings(Constants.MERCHANT_ID);
            SettingsResponse2 settings = _settingsService.GetSettings(Configuration.MerchantId);

            try
            {
                OrderRequest orderRequest = createOrderRequest();
                orderRequest.token = CreateToken();
                orderRequest.authorizeOnly = true;

                string orderCode = _orderService.Create(orderRequest).orderCode;

                _orderService.CancelAuthorizedOrder(orderCode);
            }
            catch (Worldpay.Sdk.WorldpayException ex)
            {
                // we expect: Worldpay.Sdk.WorldpayException: API error: Authorize only orders not allowed for auto capture merchants
                if (!settings.orderSetting.autoCaptureEnabled)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Verify that creating a 3DS order works
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldCreate3DSOrder()
        {
            OrderRequest orderRequest = create3DSOrderRequest();
            orderRequest.token = CreateToken();

            OrderResponse response = _orderService.Create(orderRequest);

            //Assert.IsNotNull(response.orderCode);
            //Assert.AreEqual(1999, response.amount);
            //Assert.IsNotNull(response.oneTime3DsToken);
            //Assert.IsTrue(response.is3DSOrder);
            //Assert.AreEqual(OrderStatus.PRE_AUTHORIZED, response.paymentStatus);

            Assert.NotNull(response.orderCode);
            Assert.Equal(1999, response.amount);
            Assert.NotNull(response.oneTime3DsToken);
            Assert.True(response.is3DSOrder);
            Assert.Equal(OrderStatus.PRE_AUTHORIZED, response.paymentStatus);
        }

        /// <summary>
        /// Verify that creating a APM order works
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldCreateAPMOrder()
        {
            OrderRequest orderRequest = createAPMOrderRequest();
            orderRequest.token = CreateAPMToken();

            OrderResponse response = _orderService.Create(orderRequest);

            //Assert.IsNotNull(response.redirectURL);
            //Assert.IsNotNull(response.orderCode);
            //Assert.AreEqual(1999, response.amount);
            //Assert.IsFalse(response.is3DSOrder);

            //Assert.AreEqual(OrderStatus.PRE_AUTHORIZED, response.paymentStatus);

            Assert.NotNull(response.redirectURL);
            Assert.NotNull(response.orderCode);
            Assert.Equal(1999, response.amount);
            Assert.False(response.is3DSOrder);

            Assert.Equal(OrderStatus.PRE_AUTHORIZED, response.paymentStatus);
        }

        /// <summary>
        /// Vefiy that authorize 3DS Order works
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldAuthorize3DSOrder()
        {
            OrderRequest orderRequest = create3DSOrderRequest();
            orderRequest.token = CreateToken();

            OrderResponse response = _orderService.Create(orderRequest);

            var threeDSInfo = new ThreeDSecureInfo()
            {
                shopperIpAddress = @"127.0.0.1",
                shopperSessionId = @"sessionId",
                shopperUserAgent = @"Mozilla/v1",
                shopperAcceptHeader = @"application/json"
            };

            var authorizationResponse = _orderService.Authorize(response.orderCode, @"IDENTIFIED", threeDSInfo);

            //Assert.AreEqual(response.orderCode, authorizationResponse.orderCode);
            //Assert.AreEqual(1999, authorizationResponse.amount);
            //Assert.IsTrue(response.is3DSOrder);
            //Assert.AreEqual(OrderStatus.SUCCESS, authorizationResponse.paymentStatus);

            Assert.Equal(response.orderCode, authorizationResponse.orderCode);
            Assert.Equal(1999, authorizationResponse.amount);
            Assert.True(response.is3DSOrder);
            Assert.Equal(OrderStatus.SUCCESS, authorizationResponse.paymentStatus);
        }

        /// <summary>
        /// Vefiy that authorize APM Order works
        /// </summary>
        //[Ignore]
        //[TestMethod]
        [Fact(Skip = "simulator not ready")]
        public void ShouldAuthorizeAPMOrder()
        {
            //We need to amend the simulator to auto submit the form and send notifications automatically in order to unit test this
        }

        /// <summary>
        /// Verify that refunding the order works
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldRefundOrder()
        {
            OrderRequest orderRequest = createOrderRequest();
            orderRequest.token = CreateToken();

            string orderCode = _orderService.Create(orderRequest).orderCode;
            //Assert.IsNotNull(orderCode);
            Assert.NotNull(orderCode);

            _orderService.Refund(orderCode);
        }

        //[TestMethod]
        [Fact]
        public void ShouldGetExistingOrder()
        {
            OrderRequest orderRequest = createOrderRequest();
            orderRequest.token = CreateToken();
            string orderCode = _orderService.Create(orderRequest).orderCode;

            TransferOrder order = _orderService.FindOrder(orderCode);

            //Assert.AreEqual(orderCode, order.orderCode);
            Assert.Equal(orderCode, order.orderCode);
        }

        //[TestMethod]
        [Fact]
        public void ShouldPartiallyRefundOrder()
        {
            OrderRequest orderRequest = createOrderRequest();
            orderRequest.token = CreateToken();

            string orderCode = _orderService.Create(orderRequest).orderCode;
            //Assert.IsNotNull(orderCode);
            Assert.NotNull(orderCode);

            _orderService.Refund(orderCode, 500);
        }

        /// <summary>
        /// Verify that an exception is thrown when creating an order with an invalid token
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldThrowExceptionForInvalidToken()
        {
            OrderRequest orderRequest = createOrderRequest();
            orderRequest.token = @"invalid-token";

            try
            {
                _orderService.Create(orderRequest);
            }
            catch (WorldpayException e)
            {
                //Assert.AreEqual(@"TKN_NOT_FOUND", e.apiError.customCode);
                Assert.Equal(@"TKN_NOT_FOUND", e.apiError.customCode);
            }
        }

        private string CreateToken()
        {
            return TestHelpers.CreateToken(_authService);
        }

        private string CreateAPMToken()
        {
            return TestHelpers.CreateAPMToken(_authService);
        }

        /// <summary>
        /// Create an order request
        /// </summary>
        private OrderRequest createOrderRequest()
        {
            #region -- refactored code --
            //var orderRequest = new OrderRequest();
            //orderRequest.amount = 1999;
            //orderRequest.currencyCode = CurrencyCode.GBP.ToString();
            //orderRequest.name = @"test name";
            //orderRequest.orderDescription = @"test description";

            //var address = new Address();
            //address.address1 = @"line 1";
            //address.address2 = @"line 2";
            //address.city = @"city";
            //address.countryCode = CountryCode.GB.ToString();
            //address.postalCode = @"AB1 2CD";
            //orderRequest.billingAddress = address;

            //var customerIdentifiers = new Dictionary<string, string>();
            //customerIdentifiers[@"test key 1"] = @"test value 1";

            //orderRequest.customerIdentifiers = customerIdentifiers;
            #endregion

            var orderRequest = new OrderRequest()
            {
                amount = 1999,
                currencyCode = CurrencyCode.GBP.ToString(),
                name = @"test name",
                orderDescription = @"test description",

                billingAddress = TestHelpers.GetCurrentUserDefaultAddress(),

                customerIdentifiers = new Dictionary<string, string>() { { @"test key 1", @"test value 1" } }
            };

            return orderRequest;
        }

        /// <summary>
        /// Create a 3DS order request
        /// </summary>
        private OrderRequest create3DSOrderRequest()
        {
            #region -- refactored code --
            //var orderRequest = new OrderRequest();
            //orderRequest.amount = 1999;
            //orderRequest.currencyCode = CurrencyCode.GBP.ToString();
            //orderRequest.name = @"3D";
            //orderRequest.orderDescription = @"test description";

            //var threeDSInfo = new ThreeDSecureInfo();
            //threeDSInfo.shopperIpAddress = "127.0.0.1";
            //threeDSInfo.shopperSessionId = "sessionId";
            //threeDSInfo.shopperUserAgent = "Mozilla/v1";
            //threeDSInfo.shopperAcceptHeader = "application/json";
            //orderRequest.threeDSecureInfo = threeDSInfo;
            //orderRequest.is3DSOrder = true;

            //var address = new Address();
            //address.address1 = "line 1";
            //address.address2 = "line 2";
            //address.city = "city";
            //address.countryCode = CountryCode.GB.ToString();
            //address.postalCode = "AB1 2CD";
            //orderRequest.billingAddress = address;

            //var customerIdentifiers = new Dictionary<string, string>();
            //customerIdentifiers["test key 1"] = "test value 1";

            //orderRequest.customerIdentifiers = customerIdentifiers;
            #endregion

            var orderRequest = new OrderRequest()
            {
                amount = 1999,
                currencyCode = CurrencyCode.GBP.ToString(),
                name = @"3D",
                orderDescription = @"test description",

                threeDSecureInfo = new ThreeDSecureInfo()
                {
                    shopperIpAddress = @"127.0.0.1",
                    shopperSessionId = @"sessionId",
                    shopperUserAgent = @"Mozilla/v1",
                    shopperAcceptHeader = @"application/json"
                },
                is3DSOrder = true,

                billingAddress = TestHelpers.GetCurrentUserDefaultAddress(),

                customerIdentifiers = new Dictionary<string, string>() { { @"test key 1", @"test value 1" } }
            };

            return orderRequest;
        }

        /// <summary>
        /// Create a APM order request
        /// </summary>
        private OrderRequest createAPMOrderRequest()
        {
            #region -- refactored code --
            //var orderRequest = new OrderRequest();
            //orderRequest.amount = 1999;
            //orderRequest.successUrl = "http://www.testurl.com/success";
            //orderRequest.cancelUrl = "http://www.testurl.com/cancel";
            //orderRequest.failureUrl = "http://www.testurl.com/failure";
            //orderRequest.pendingUrl = "http://www.testurl.com/pending";

            //orderRequest.currencyCode = CurrencyCode.GBP.ToString();
            //orderRequest.name = "Test";
            //orderRequest.orderDescription = "test description";
            //orderRequest.is3DSOrder = false;

            //var address = new Address();
            //address.address1 = "line 1";
            //address.address2 = "line 2";
            //address.city = "city";
            //address.countryCode = CountryCode.GB.ToString();
            //address.postalCode = "AB1 2CD";
            //orderRequest.billingAddress = address;

            //var customerIdentifiers = new Dictionary<string, string>();
            //customerIdentifiers["test key 1"] = "test value 1";

            //orderRequest.customerIdentifiers = customerIdentifiers;
            #endregion

            var orderRequest = new OrderRequest()
            {
                amount = 1999,
                successUrl = @"http://www.testurl.com/success",
                cancelUrl = @"http://www.testurl.com/cancel",
                failureUrl = @"http://www.testurl.com/failure",
                pendingUrl = @"http://www.testurl.com/pending",

                currencyCode = CurrencyCode.GBP.ToString(),
                name = @"Test",
                orderDescription = @"test description",
                is3DSOrder = false,

                billingAddress = TestHelpers.GetCurrentUserDefaultAddress(),

                customerIdentifiers = new Dictionary<string, string>() { { @"test key 1", @"test value 1" } }

            };

            return orderRequest;
        }
    }
}