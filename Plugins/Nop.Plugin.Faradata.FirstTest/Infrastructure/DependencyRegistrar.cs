using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Faradata.FirstTest.Services;
using Nop.Plugin.Faradata.Test.Domain;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Faradata.FirstTest.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<CustomerTokenService>().As<ICustomerTokenService>().InstancePerLifetimeScope();
            //data context
            builder.RegisterPluginDataContext<CustomerTokenObjectContext>("nop_object_context_CustomerToken_in_FirstTest");
            builder.RegisterType<EfRepository<CustomerToken>>()
                .As<IRepository<CustomerToken>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_CustomerToken_in_FirstTest"))
                .InstancePerLifetimeScope();
            ///===================
            //builder.RegisterType<TestRecordService>().As<ITestRecordService>().InstancePerLifetimeScope();
            ////data context
            //builder.RegisterPluginDataContext<TestRecordObjectContext>(CONTEXT_NAME_TESTRECORD);
            ////override required repository with our custom context
            //builder.RegisterType<EfRepository<TestRecord>>()
            //.As<IRepository<TestRecord>>()
            //.WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME_TESTRECORD))
            //.InstancePerLifetimeScope();
        }
    }
}
