﻿using FlowReader.Core.Entities;

namespace FlowReader.DataAccess.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<List<Category>> GetAllIncludedAsync();
    }
}
