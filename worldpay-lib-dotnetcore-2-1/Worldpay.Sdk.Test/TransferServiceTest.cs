using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Worldpay.Sdk.Test
{
    //[TestClass]
    public class TransferServiceTest
    {
        private TransferService _transferService;

        //[TestInitialize]
        //public void Setup()
        // for xunit, move from Setup method to class constructor
        public TransferServiceTest()
        {
            var restClient = new WorldpayRestClient(Configuration.ServiceKey);
            _transferService = restClient.GetTransferService();
        }

        //[TestMethod]
        [Fact]
        public void TestGetTransfers()
        {
            var response = _transferService.GetTransfers(Configuration.MerchantId, null);
            //Assert.IsNotNull(response);
            Assert.NotNull(response);
        }

        //[Ignore]
        //[TestMethod]
        [Fact(Skip = "???")]
        public void TestGetTransfer()
        {
            var response = _transferService.GetTransfer(Configuration.MerchantId);
            //Assert.IsNotNull(response);
            Assert.NotNull(response);
        }
    }
}
