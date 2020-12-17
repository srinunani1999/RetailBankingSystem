using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionServiceTest
{
    public class TransferModel
    {

        public int Source_AccountId { get; set; }
        public int Target_AccountId { get; set; }
        public int amount { get; set; }
    }
}
