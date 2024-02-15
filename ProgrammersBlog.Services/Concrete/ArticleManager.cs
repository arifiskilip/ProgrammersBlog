using AutoMapper;
using ProgrammersBlog.Data.UnitOfWork;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities.StaticMessagess;
using ProgrammersBlog.Shared.Results;
using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class ArticleManager : BaseManager, IArticleService
    {
        public ArticleManager(IUow uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByDeletedAsync()
        {
            var articles =
                await Uow.Articles.GetAllAsync(a => a.IsDeleted, ar => ar.User,
                    ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound, null);
        }


        public async Task<IResult> UndoDeleteAsync(int articleId, string modifiedByName)
        {
            var result = await Uow.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await Uow.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = false;
                article.IsActive = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await Uow.Articles.UpdateAsync(article);
                await Uow.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Article.UndoDelete(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound);
        }
        public async Task<IDataResult<ArticleDto>> GetAsync(int articleId)
        {
            var article = await Uow.Articles.GetAsync(a => a.Id == articleId, a => a.User, a => a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Article.GetArticle
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.ArticleNotFound, null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllAsync()
        {
            var articles = await Uow.Articles.GetAllAsync(null, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Article.GetArticles
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.ArticlesNotFound, null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync()
        {
            var articles = await Uow.Articles.GetAllAsync(a => !a.IsDeleted, ar => ar.User, ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Article.GetArticles
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.ArticlesNotFound, null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var articles =
                await Uow.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, ar => ar.User,
                    ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Article.GetArticles
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.ArticlesNotFound, null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await Uow.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var articles = await Uow.Articles.GetAllAsync(
                    a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success,
                        Message = Messages.Article.GetArticles
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.ArticlesNotFound, null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.ArticlesNotFound, null);

        }

        public async Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = Mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;
            await Uow.Articles.AddAsync(article);
            await Uow.SaveAsync();
            return new Result(ResultStatus.Success, $"{articleAddDto.Title} "+Messages.Article.AddedArticle);
        }

        public async Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = Mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await Uow.Articles.UpdateAsync(article);
            await Uow.SaveAsync();
            return new Result(ResultStatus.Success, $"{articleUpdateDto.Title} "+Messages.Article.UpdatedArticle);
        }

        public async Task<IResult> DeleteAsync(int articleId, string modifiedByName)
        {
            var result = await Uow.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await Uow.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await Uow.Articles.UpdateAsync(article);
                await Uow.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} "+Messages.Article.DeletedArticle);
            }
            return new Result(ResultStatus.Error, Messages.Article.NotDeletedArticle);
        }

        public async Task<IResult> HardDeleteAsync(int articleId)
        {
            var result = await Uow.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await Uow.Articles.GetAsync(a => a.Id == articleId);
                await Uow.Articles.DeleteAsync(article);
                await Uow.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, Messages.Article.NotDeletedArticle);
        }
        public async Task<IDataResult<int>> CountAsync()
        {
            var articlesCount = await Uow.Articles.CountAsync();
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Article.NotFound, -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var articlesCount = await Uow.Articles.CountAsync(a => !a.IsDeleted);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Article.NotFound, -1);
            }
        }
        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsync(int articleId)
        {
            var result = await Uow.Articles.AnyAsync(c => c.Id == articleId);
            if (result)
            {
                var article = await Uow.Articles.GetAsync(c => c.Id == articleId);
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
            }
            else
            {
                return new DataResult<ArticleUpdateDto>(ResultStatus.Error, "Böyle bir makale bulunamadı.", null);
            }
        }
    }
}
