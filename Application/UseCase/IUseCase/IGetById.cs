using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.IUseCase
{
    public interface IGetById
    {
        Task<Result<Product>> GetByIdAsync(int id);
    }
}
