using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Company.Media.Data;
using Nop.Plugin.Company.Media.Services;
using Nop.Plugin.Company.Media.Services.MediaCategories;
using Nop.Plugin.Company.Media.Services.MediaCategory;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Company.Media.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <inheritdoc />
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {         
            builder.RegisterType<MediaService>().As<IMediaService>().InstancePerLifetimeScope();        
            //data context
            builder.RegisterPluginDataContext<SettingMediaDbContex>("nop_object_context_Media_in_Media");
            builder.RegisterType<EfRepository<Domain.Media>>()
                .As<IRepository<Domain.Media>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_Media_in_Media"))
                .InstancePerLifetimeScope();
            //
            builder.RegisterType<MediaCategoryService>().As<IMediaCategoryService>().InstancePerLifetimeScope();
            //data context
            builder.RegisterPluginDataContext<SettingMediaDbContex>("nop_object_context_MediaCategory_in_Media");
            builder.RegisterType<EfRepository<Domain.MediaCategory>>()
                .As<IRepository<Domain.MediaCategory>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_MediaCategory_in_Media"))
                .InstancePerLifetimeScope();
            //           
            builder.RegisterType<MediaCategoriesService>().As<IMediaCategoriesService>().InstancePerLifetimeScope();        
            //data context
            builder.RegisterPluginDataContext<SettingMediaDbContex>("nop_object_context_MediaCategories_in_Media");
            builder.RegisterType<EfRepository<Domain.MediaCategories>>()
                .As<IRepository<Domain.MediaCategories>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_MediaCategories_in_Media"))
                .InstancePerLifetimeScope();
        }
        /// <inheritdoc />
        /// <summary>
        /// Order of this dependency registrar implementation            
        /// </summary>
        public int Order => 1;
    }
}
