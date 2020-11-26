using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAPIAPP.Services.Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IBoutiqueRepository Boutique { get; }
        IEvaluationRepository Evaluation { get; }
        Task SaveAsync();
    }
}
