using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article>,IArticleRepository
    {
        public ArticleRepository(AppDbContext context) : base(context)
        {
        }
    }
}



