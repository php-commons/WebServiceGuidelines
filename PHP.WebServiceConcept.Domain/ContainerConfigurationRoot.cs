using System;
using System.Collections.Generic;
using System.Text;
using PHP.WebServiceConcept.Domain.Services;
using SimpleInjector;

namespace PHP.WebServiceConcept.Domain
{
    public static class ContainerConfigurationRoot
    {
        public static Container AddDomain(this Container container, string environmentName = null)
        {
            container.Register<IAccountCommandProcessor, AccountCommandProcessor>();
            container.Register<ITransactionCommandProcessor, TransactionCommandProcessor>();

            container.Register<IAccountQueryProcessor, AccountQueryProcessor>();
            container.Register<ITransactionQueryProcessor, TransactionQueryProcesor>();

            return container;
        }
    }
}
