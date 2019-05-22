using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core;
using Nop.Data;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using Nop.Plugin.Faradata.AlarmShopping.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.AlarmShopping.Data
{
    public class FaraObjectContext : DbContext, IDbContext
    {
        public FaraObjectContext(DbContextOptions<FaraObjectContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FaraBotUserMap());
            modelBuilder.ApplyConfiguration(new FaraBotConfigMap());
            base.OnModelCreating(modelBuilder);
        }
        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public virtual string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }

        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            using (var transaction = this.Database.BeginTransaction())
            {
                var result = this.Database.ExecuteSqlCommand(sql, parameters);
                transaction.Commit();

                return result;
            }
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Install()
        {
            this.ExecuteSqlScript(this.GenerateCreateScript());
        }

        public void Uninstall()
        {
            this.DropPluginTable(nameof(FaraBotUser));
            this.DropPluginTable(nameof(FaraBotConfig));
        }
    }
}
