using System;
using System.Collections.Generic;
using System.Text;

namespace PHP.WebServiceConcept.Domain.Commands
{
    public class CreateAccountCommand
    {
        public string Name { get; }
        public decimal StartingBalance { get; }

        public CreateAccountCommand(string name, decimal startingBalance)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be null or empty", nameof(name));
            if (startingBalance <= 0.0m)
                throw new ArgumentOutOfRangeException(nameof(startingBalance), startingBalance, nameof(startingBalance));

            Name = name;
            StartingBalance = startingBalance;
        }
    }
}
