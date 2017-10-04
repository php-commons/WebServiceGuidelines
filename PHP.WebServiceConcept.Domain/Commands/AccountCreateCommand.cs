using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Commands
{
    public class AccountCreateCommand
    {
        public string Name { get; }
        public decimal StartingBalance { get; }

        public AccountCreateCommand(string name, decimal startingBalance)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be null or empty", nameof(name));
            if (startingBalance < 0.0m)
                throw new ArgumentOutOfRangeException(nameof(startingBalance), startingBalance, "startingBalance can not be less than zero");

            Name = name;
            StartingBalance = startingBalance;
        }
    }
}
