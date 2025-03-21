﻿using Portfolio.Domain.Entities;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>,ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}