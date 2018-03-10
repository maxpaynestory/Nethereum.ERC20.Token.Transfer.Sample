using System;
using EthereumWalletApp.Core;

namespace EthereumWalletApp
{
    class CliScript
    {
        static void Main(string[] args)
        {
            var client = new EthClient(new RpcUrl("http://localhost:8545"));
            
            /* create new account */
            var privateKey = "Pass@sdasdasdnbewrwndsnf";
            Console.WriteLine("your private key: " + privateKey);
            var addressTask = client.createAccount(privateKey);
            var toAddress = addressTask.Result.ToString();
            Console.WriteLine("New Account Address: " + toAddress);

            var contractAddress = "0x920aad4a2fe079f82d71bfd4419a25bb5af41e90";
            var coinOwnerAddress = "0xe9fa5c9b698433d9267d0d8cf640b55e4f18ce21";
            var coinOwnerPrivateKey = "72c642d4552a4e782db10fdbcf8a82ee771d9e830b143b8fa28a04fb0ba2cdcc";

            /* get balance of token creator */
            var ownerBalanceTask = client.getAccountBalance(coinOwnerAddress,contractAddress);
            var ownerBalance = ownerBalanceTask.Result.ToString();
            Console.WriteLine("Balance of token creator :" + ownerBalance);
            
            /* transfer 100 tokens */
            var transactionHashTask = client.transferTokens(coinOwnerAddress, coinOwnerPrivateKey, toAddress, contractAddress,10000);
            var transactionHash = transactionHashTask.Result.ToString();
            Console.WriteLine("Transaction hash: " + transactionHash);

            /* get balance of account */
            var tokenBalanceTask = client.getAccountBalance(toAddress,contractAddress);
            var tokenBalance = tokenBalanceTask.Result.ToString();
            Console.WriteLine("Balance is :" + tokenBalance);

            /* get balance of token creator */
            ownerBalanceTask = client.getAccountBalance(coinOwnerAddress,contractAddress);
            ownerBalance = ownerBalanceTask.Result.ToString();
            Console.WriteLine("Balance of token creator :" + ownerBalance);

        }
    }
}
