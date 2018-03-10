using System;
using Nethereum.Web3.Accounts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.StandardTokenEIP20.Functions;
using Nethereum.Contracts.CQS;
using System.Threading.Tasks;
using System.Numerics;
namespace EthereumWalletApp.Core
{
    
    public class EthClient
    {
        protected Nethereum.Web3.Web3 web3;
        
        public RpcUrl rpcUrl{
            get; protected set;
        }

        public EthClient(RpcUrl aRpcUrl){
            rpcUrl = aRpcUrl;
            web3 = setServer(rpcUrl);
        }

        protected Nethereum.Web3.Web3 setServer(RpcUrl aRpcUrl){
            return new Nethereum.Web3.Web3(aRpcUrl.url);
        }

        public async Task<string> createAccount(string aPassword){
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            Task<string> newAccountTask = web3.Personal.NewAccount.SendRequestAsync(aPassword);
            var newAccount = await newAccountTask;
            return newAccount;
        }

        public async Task<BigInteger> getAccountBalance(string addressOwner, string contractAddress)
        {
            var tokenService = new Nethereum.StandardTokenEIP20.StandardTokenService(web3, contractAddress);
            var ownerBalanceTask = tokenService.GetBalanceOfAsync<BigInteger>(addressOwner);
            return await ownerBalanceTask;
        }

        public async Task<string> transferTokens(string senderAddress, string privateKey, string receiverAddress, string contractAddress, UInt64 tokens){
            var transactionMessage = new TransferFunction(){
                FromAddress = senderAddress,
                To = receiverAddress,
                TokenAmount = tokens
            };
            var transferHandler = web3.Eth.GetContractTrasactionHandler<TransferFunction>();
            Task<string> transactionHashTask = transferHandler.SendRequestAsync(transactionMessage, contractAddress);
            return await transactionHashTask;
        }
    }

    public class RpcUrl
    {
        public string url{
            get; protected set;
        }

        public RpcUrl(string aUrl){
            url = aUrl;
        }
    }
}