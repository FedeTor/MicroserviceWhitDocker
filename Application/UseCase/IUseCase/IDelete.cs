using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.IUseCase
{
    public interface IDelete
    {
        Task<Result<ProductResult>> DeleteAsync(int id);
    }
}
