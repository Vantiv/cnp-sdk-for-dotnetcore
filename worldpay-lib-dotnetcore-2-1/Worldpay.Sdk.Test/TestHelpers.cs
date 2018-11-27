using System;
using System.Collections.Generic;
using System.Globalization;

using Worldpay.Sdk.Models;

namespace Worldpay.Sdk.Test
{
    class TestHelpers
    {
        /// <summary>
        /// Create an access token
        /// </summary>
        internal static string CreateToken(AuthService authService)
        {
            #region -- Refactored Code --
            //var tokenRequest = new TokenRequest();
            //tokenRequest.clientKey = Configuration.ClientKey;

            //var cardRequest = new CardRequest();
            //cardRequest.cardNumber = TestMastercardNumber;
            //cardRequest.cvc = TestCvv;
            //cardRequest.name = "csharplib client";

            //cardRequest.expiryMonth = 2;
            //cardRequest.expiryYear = 2018;

            //cardRequest.type = "Card";

            //tokenRequest.paymentMethod = cardRequest;
            #endregion

            // build request object
            var tokenRequest = new TokenRequest()
            {
                clientKey = Configuration.ClientKey,
                paymentMethod = new CardRequest()
                {
                    cardNumber = Constants.TEST_MASTERCARD_NUMBER,
                    cvc = Constants.TEST_CVV,
                    name = @"csharplib client",
                    expiryMonth = DateTime.Now.AddMonths(1).Month,  // calc expiry date so it is in the future
                    expiryYear = DateTime.Now.AddMonths(1).Year,
                    type = @"Card"
                }
            };

            // call the token service
            TokenResponse response = authService.GetToken(tokenRequest);

            return response.token;
        }

        /// <summary>
        /// Create an APM token
        /// </summary>
        internal static string CreateAPMToken(AuthService authService)
        {
            #region -- Refactored Code --
            //var tokenRequest = new TokenRequest();
            //tokenRequest.clientKey = Configuration.ClientKey;

            //var cardRequest = new APMRequest();
            //cardRequest.type = "APM";
            //cardRequest.apmName = "PAYPAL";
            //cardRequest.shopperCountryCode = "GB";
            //cardRequest.apmFields = new Dictionary<string, string>();

            //tokenRequest.paymentMethod = cardRequest;
            #endregion

            // build request object
            var tokenRequest = new TokenRequest()
            {
                clientKey = Configuration.ClientKey,
                paymentMethod = new APMRequest()
                {
                    type = @"APM",
                    apmName = @"PAYPAL",
                    shopperCountryCode = GetCurrentUserCountryCode(), // @"GB",
                    apmFields = new Dictionary<string, string>()
                }
            };

            // call the token service
            TokenResponse response = authService.GetToken(tokenRequest);

            return response.token;
        }

        // added this method to localize country code used
        internal static string GetCurrentUserCountryCode()
        {
            string name = RegionInfo.CurrentRegion.TwoLetterISORegionName;

            return name;
        }

        // added this method to localize addresses generated
        // build an address based on the current user's region settings
        internal static Address GetCurrentUserDefaultAddress()
        {
            string currentUserCountryCode = GetCurrentUserCountryCode();

            var address = new Address()
            {
                address1 = @"line 1",
                address2 = @"line 2",
                city = @"city",
                countryCode = currentUserCountryCode,
                postalCode = currentUserCountryCode == "US" ? @"12345" : @"AB1 2CD"
            };

            return address;
        }

    }
}
