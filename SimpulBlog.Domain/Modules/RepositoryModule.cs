using Autofac;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Domain.Repositories.Concrete;
using System.Reflection;

namespace SimpulBlog.Domain.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterGeneric(typeof(CommitRepository<>))
                .As(typeof(ICommitRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
