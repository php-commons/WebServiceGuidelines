using System;
using System.Collections.Generic;
using System.Text;
using PHP.WebServiceConcept.Domain;
using SimpleInjector;

namespace PHP.WebServiceConcept.Persistence
{
    public static class ContainerConfigurationRoot
    {
        public static Container AddPersistence(this Container container, string environmentName = null)
        {
            container.Register<IAccountRepository, AccountRepository>();
            container.Register<ITransactionRepository, TransactionRepository>();

            return container;
        }
    }
}
