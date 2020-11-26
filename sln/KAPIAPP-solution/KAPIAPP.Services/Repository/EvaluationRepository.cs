using KAPIAPP.Services.Entity;
using KAPIAPP.Services.Entity.Helpers;
using KAPIAPP.Services.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace KAPIAPP.Services.Repository
{
    public class EvaluationRepository : RepositoryBase<Evaluation> , IEvaluationRepository
    {
        public EvaluationRepository(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }

        public async Task<IEnumerable<Evaluation>> GetEvaluationsAsync()
        {
            return await FindAll().Include(b => b.Boutique)
                .Include(u => u.User)
                .OrderBy(e => e.EvaluationDate).ToListAsync();
        }
        
        public async Task<PagedList<Evaluation>> GetEvaluationPagined(EvaluationParameters evaluationParameters)
        {
            var evaluations = FindAll();
                

            if (String.IsNullOrWhiteSpace(evaluationParameters.BoutiqueName))
            {
                return await PagedList<Evaluation>.ToPagedList(FindAll()
                .Include(b => b.Boutique)
                .Include(u => u.User)
                .OrderBy(e => e.NoteGlobale), evaluationParameters.PageNumber, evaluationParameters.PageSize);
            }
            else
            {
                return await PagedList<Evaluation>.ToPagedList(FindAll()
               .Where(e => e.Boutique.Name.ToLower().Contains(evaluationParameters.BoutiqueName.Trim().ToLower()))
               .Include(b => b.Boutique)
               .Include(u => u.User)               
               .OrderBy(e => e.NoteGlobale), evaluationParameters.PageNumber, evaluationParameters.PageSize);
            }                        
        }

        public async Task<Evaluation> GetEvaluationByIdAsync(int IdEval)
        {
            return await FindByCondition(eval => eval.Id.Equals(IdEval))
                .Include(b => b.Boutique)
                .Include(u => u.User)
                .FirstOrDefaultAsync();
        }

        public void CreateEvaluation(Evaluation evaluation)
        {
            Create(evaluation);
        }

        public void UpdateEvaluation(Evaluation evaluation)
        {
            Update(evaluation);
        }

        public void DeleteEvaluation(Evaluation evaluation)
        {
            Delete(evaluation);
        }

        //private void SearchByBoutiqueName(ref IQueryable<Evaluation> evaluations, string boutiqueName)
        //{
        //    if (!evaluations.Any() || string.IsNullOrWhiteSpace(boutiqueName))
        //        return;
        //    evaluations = evaluations.Where(e => e.Boutique.Name.ToLower().Contains(boutiqueName.Trim().ToLower()));
        //}

        private void ApplySort(ref IQueryable<Evaluation> evaluations, string orderByQueryString )
        {
            if (!evaluations.Any())
                return;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                evaluations = evaluations.OrderBy(e => e.EvaluationDate);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
        }

    } 
}
