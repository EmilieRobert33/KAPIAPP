using KAPIAPP.Services.Entity;
using KAPIAPP.Services.Entity.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAPIAPP.Services.Repository
{
    public interface IBoutiqueRepository : IRepositoryBase<Boutique>
    {
        Task<IEnumerable<Boutique>> GetBoutiquesAsync();
        Task<PagedList<Boutique>> GetBoutiquePagined(BoutiqueParameters boutiqueParameters);
        Task<Boutique> GetBoutiqueByIdAsync(int Id);        
        void CreateBoutique(Boutique boutique);
        void UpdateBoutique(Boutique boutique);
        void DeleteBoutique(Boutique boutique);
    }
}
