using System;
using Autofac;
using MyNoSqlServer.Abstractions;
using Service.Service.KYC.MyNoSql;

namespace Service.Service.KYC.Modules
{
    public class MyNoSqlModule : Module
    {
        private readonly Func<string> _myNoSqlServerWriterUrl;

        public MyNoSqlModule(Func<string> myNoSqlServerWriterUrl)
        {
            _myNoSqlServerWriterUrl = myNoSqlServerWriterUrl;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterMyNoSqlWriter<KycStatusNoSqlEntity>(builder, KycStatusNoSqlEntity.TableName);
        }

        private void RegisterMyNoSqlWriter<TEntity>(ContainerBuilder builder, string table)
            where TEntity : IMyNoSqlDbEntity, new()
        {
            builder.Register(ctx =>
                    new MyNoSqlServer.DataWriter.MyNoSqlServerDataWriter<TEntity>(_myNoSqlServerWriterUrl, table, true))
                .As<IMyNoSqlServerDataWriter<TEntity>>()
                .SingleInstance();
        }
    }
}