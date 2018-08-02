using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSigExample
{
  class Program
  {
    static void Main(string[] args)
    {
      MainAsync().Wait();
    }

    private static async Task MainAsync()
    {
      try
      {
        var account = new Account("privatekey");
        var web3 = new Web3(account, "http://localhost:8545");

        var exampleContractAddress = "0xf8318fC2BDDc6F46F356EcBEe300064213DA9FA5";
        var exampleContract = web3.Eth.GetContract(MultiSigExample.Properties.Resources.ExampleContractABI, exampleContractAddress);
        var exampleExecuteFunction = exampleContract.GetFunction("Execute");
        var data = exampleExecuteFunction.CreateCallInput(20).Data;
        var byteData = data.HexToByteArray();

        var multiSigContractAddress = "0x1F271047cC3F971D8B2D226A1Af0B5e7DEE5aF20";
        var multiSigContract = web3.Eth.GetContract(MultiSigExample.Properties.Resources.MultiSigABI, multiSigContractAddress);
        var multiSigExecuteFunction = multiSigContract.GetFunction("execute");
        var gas = await multiSigExecuteFunction.EstimateGasAsync(exampleContractAddress, 0, byteData);
        var transactionHash = await multiSigExecuteFunction.SendTransactionAsync(account.Address, gas, null, exampleContractAddress, 0, byteData);
      }
      catch (Exception e)
      {

        throw;
      }

    }
  }
}
