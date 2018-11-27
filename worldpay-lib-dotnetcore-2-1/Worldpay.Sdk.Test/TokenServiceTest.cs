using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using Worldpay.Sdk;
using Worldpay.Sdk.Models;
using Worldpay.Sdk.Test;

namespace WorldPay.Sdk.Test
{
    //[TestClass]
    public class TokenServiceTest
    {
        /// <summary>
        /// Authorization service, for obtaining access tokens
        /// </summary>
        private AuthService _authService;

        /// <summary>
        /// Token service, for handling interaction with the token API
        /// </summary>
        private TokenService _tokenService;

        /// <summary>
        /// Initialise the service clients
        /// </summary>
        //[TestInitialize]
        //public void Setup()
        // for xunit, move from Setup method to class constructor
        public TokenServiceTest()
        {
            var restClient = new WorldpayRestClient(Configuration.ServiceKey);
            _authService = restClient.GetAuthService();
            _tokenService = restClient.GetTokenService();
        }

        /// <summary>
        /// Verify that retrieving an existing token works
        /// </summary>
        //[TestMethod]
        [Fact]
        public void ShouldRetrieveToken()
        {
            var token = CreateToken();

            TokenResponse response = _tokenService.Get(token);

            //Assert.AreEqual(token, response.token);
            //Assert.AreEqual("csharplib client", response.paymentMethod.name);
            //Assert.IsInstanceOfType(response.paymentMethod, typeof(PaymentResponse));
            //Assert.AreEqual("**** **** **** 4444", ((PaymentResponse)response.paymentMethod).maskedCardNumber);

            Assert.Equal(token, response.token);
            Assert.Equal("csharplib client", response.paymentMethod.name);
            //Assert.InstanceOfType(response.paymentMethod, typeof(PaymentResponse));
            Assert.IsType<PaymentResponse>(response.paymentMethod);
            Assert.Equal("**** **** **** 4444", ((PaymentResponse)response.paymentMethod).maskedCardNumber);
        }

        /// <summary>
        /// Create an access token
        /// </summary>
        private string CreateToken()
        {
            return TestHelpers.CreateToken(_authService);
        }
    }
}
