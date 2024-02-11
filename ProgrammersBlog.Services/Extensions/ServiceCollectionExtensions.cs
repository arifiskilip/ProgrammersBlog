using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Contexts;
using ProgrammersBlog.Data.UnitOfWork;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;

namespace ProgrammersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string conntectionString)
        {
            serviceCollection.AddDbContext<ProgrammersBlogContext>(opt =>
            {
                opt.UseSqlServer(conntectionString);
            });

            serviceCollection.AddScoped<IUow, Uow>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}

