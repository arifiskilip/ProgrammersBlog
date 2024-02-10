using ProgrammersBlog.Data.Abstract;
using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.UnitOfWork
{
    public interface IUow : IAsyncDisposable
    {
        IArticleRepository Articles { get; } 
        ICategoryRepository Categories { get;}
        ICommentRepository Comments { get; }
    
        Task<int> SaveAsync();
    }
}
