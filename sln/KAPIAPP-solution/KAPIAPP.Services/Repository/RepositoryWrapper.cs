using KAPIAPP.Services.Entity;
using KAPIAPP.Services.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAPIAPP.Services.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationContext _applicationContext;
        private IBoutiqueRepository _boutique;
        private IEvaluationRepository _evaluation;

        public IBoutiqueRepository Boutique
        {
            get
            {
                if (_boutique == null)
                    _boutique = new BoutiqueRepository(_applicationContext);
                return _boutique;
            }
        }

        public IEvaluationRepository Evaluation
        {
            get
            {
                if (_evaluation == null)
                    _evaluation = new EvaluationRepository(_applicationContext);
                return _evaluation;
            }
        }

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
