﻿using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IBaseRepository;

namespace Ecommerce.Infrastructure.Abstracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
}
