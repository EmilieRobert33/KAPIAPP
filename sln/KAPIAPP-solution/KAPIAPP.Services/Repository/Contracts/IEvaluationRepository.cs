using KAPIAPP.Services.Entity;
using KAPIAPP.Services.Entity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPIAPP.Services.Repository.Contracts
{
    public interface IEvaluationRepository : IRepositoryBase<Evaluation>
    {
        Task<IEnumerable<Evaluation>> GetEvaluationsAsync();
        Task<PagedList<Evaluation>> GetEvaluationPagined(EvaluationParameters evaluationParameters);
        Task<Evaluation> GetEvaluationByIdAsync(int Id);
        void CreateEvaluation(Evaluation evaluation);
        void UpdateEvaluation(Evaluation evaluation);
        void DeleteEvaluation(Evaluation evaluation);
    }
}
