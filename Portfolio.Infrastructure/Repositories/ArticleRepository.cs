using Portfolio.Domain.Entities;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public class ArticleRepository : Repository<Article>,IArticleRepository
{
    public ArticleRepository(AppDbContext context) : base(context)
    {
    }

    
}