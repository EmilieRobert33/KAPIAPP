using KAPIAPP.Services.Entity;
using KAPIAPP.Services.Entity.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPIAPP.Services.Repository
{
    public class BoutiqueRepository : RepositoryBase<Boutique>, IBoutiqueRepository
    {
        public BoutiqueRepository(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }

        public async Task<IEnumerable<Boutique>> GetBoutiquesAsync()
        {
            return await FindAll().OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<PagedList<Boutique>> GetBoutiquePagined(BoutiqueParameters boutiqueParameters)
        {
            return await PagedList<Boutique>.ToPagedList(FindAll().OrderBy(b => b.Name),
                boutiqueParameters.PageNumber,
                boutiqueParameters.PageSize);
        }

        public async Task<Boutique> GetBoutiqueByIdAsync(int idBoutique)
        {
            return await FindByCondition(boutique => boutique.Id.Equals(idBoutique))
                .Include(e => e.Evaluations)
                .FirstOrDefaultAsync();
        }       

        public void CreateBoutique(Boutique boutique)
        {
            Create(boutique);
        }

        public void UpdateBoutique(Boutique boutique)
        {
            Update(boutique);
        }

        public void DeleteBoutique(Boutique boutique)
        {
            Delete(boutique);
        }
    }
}
