using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Nop.Web.Framework.Infrastructure.Extensions;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using Nop.Plugin.Faradata.AlarmShopping.Services;
using Nop.Plugin.Faradata.AlarmShopping.Data;

namespace Nop.Plugin.Faradata.AlarmShopping.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<FaraBotUserService>().As<IFaraBotUserService>().InstancePerLifetimeScope();
            //data context
            builder.RegisterPluginDataContext<FaraObjectContext>("nop_object_context_FaraBotUser_in_AlarmShopping");
            builder.RegisterType<EfRepository<FaraBotUser>>()
                .As<IRepository<FaraBotUser>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_FaraBotUser_in_AlarmShopping"))
                .InstancePerLifetimeScope();

            builder.RegisterType<FaraBotConfigService>().As<IFaraBotConfigService>().InstancePerLifetimeScope();
            //data context
            builder.RegisterPluginDataContext<FaraObjectContext>("nop_object_context_FaraBotConfig_in_AlarmShopping");
            builder.RegisterType<EfRepository<FaraBotConfig>>()
                .As<IRepository<FaraBotConfig>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_FaraBotConfig_in_AlarmShopping"))
                .InstancePerLifetimeScope();
        }
    }
}
