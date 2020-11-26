using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KAPIAPP.Services.Entity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using KAPIAPP.Services.DTO.BoutiqueDto;
using KAPIAPP.Services.Repository.Contracts;
using Newtonsoft.Json;

namespace KAPIAPP_App.Controllers
{
    [Authorize]
    public class BoutiqueController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public BoutiqueController(ILoggerManager logger,
                                    IMapper mapper,
                                    IRepositoryWrapper repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: Boutique
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, BoutiqueParameters boutiqueParameter)
        {
            //var boutiques = await _repository.Boutique.GetBoutiquesAsync();

            //if (boutiques == null)
            //{
            //    _logger.LogError($"une erreur est survenue pour récupérer la liste du serveur");
            //    return BadRequest($"la liste des boutiques n'a pas pu être retournée");
            //}            

            //var listeBoutiqueToReturn = _mapper.Map<IEnumerable<BoutiqueListeDto>>(boutiques);

            //_logger.LogInfo($"récupération des {boutiques} boutique depuis la base de données");
            //return View(listeBoutiqueToReturn);

            if (boutiqueParameter.BoutiqueName != null) 
            {
                boutiqueParameter.PageSize = 1;
            }
            else
            {
                boutiqueParameter.BoutiqueName = currentFilter;
            }

            ViewBag.CurrentFilter = boutiqueParameter.BoutiqueName;
            ViewBag.PageSize = boutiqueParameter.PageSize;
            ViewBag.PageNumber = boutiqueParameter.PageNumber;

            var listeBoutique = await _repository.Boutique.GetBoutiquePagined(boutiqueParameter);

            if (listeBoutique == null)
            {
                _logger.LogError("erreur la liste des boutique n'a pas pu être récupérée");
                return BadRequest("Liste des boutique non trouvée");
            }
            return View(listeBoutique);
        }

        // GET: Boutique/Details/5
        public async Task<IActionResult> Details(int id)
        {           
            var boutique = await _repository.Boutique.GetBoutiqueByIdAsync(id);

            if (boutique == null)
            {
                _logger.LogError($"Boutique non trouvée avec l'id: {id}");
                return NotFound();
            }
            _logger.LogInfo($"Boutique avec id :{id} retournée");
            var boutiqueToReturn = _mapper.Map<BoutiqueListeDto>(boutique);
            return View(boutiqueToReturn);
        }

        // GET: Boutique/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoutiqueCreationDto boutiqueCreation)
        {
            if (boutiqueCreation == null)
            {
                _logger.LogError("objet boutique null");

                return BadRequest($"Il n'y a pas d'information sur la boutique à créer");
            }
           
            if (!ModelState.IsValid)
            {
                _logger.LogError("objet boutique envoyé non valide");

                return View(boutiqueCreation);               
            }

            var boutique = _mapper.Map<Boutique>(boutiqueCreation);

            _repository.Boutique.CreateBoutique(boutique);
            await _repository.SaveAsync();
           
            return RedirectToAction(nameof(Index));
        }

        // GET: Boutique/Edit/5
        public async Task<IActionResult> Edit(int id)
        {           
            var boutique = await _repository.Boutique.GetBoutiqueByIdAsync(id);

            if (boutique == null)
            {
                _logger.LogError($"Boutique non trouvée avec l'id: {id}");
                return NotFound();
            }

            var boutiqueToReturn = _mapper.Map<BoutiqueEditDto>(boutique);
            
            return View(boutiqueToReturn);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoutiqueEditDto boutiqueEdit)
        {
            if (id != boutiqueEdit.Id)
            {
                _logger.LogError($"l'identifiant {id} envoyé ne correspond à aucune boutiques");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("objet boutique envoyé non valide");

                return View(boutiqueEdit);                
            }
           
            var boutique = _mapper.Map<Boutique>(boutiqueEdit);
            _repository.Boutique.UpdateBoutique(boutique);
            await _repository.SaveAsync();
                        
            return RedirectToAction(nameof(Index));
        }

        // GET: Boutique/Delete/5
        public async Task<IActionResult> Delete(int id)
        {           
            var boutique = await _repository.Boutique.GetBoutiqueByIdAsync(id);
                
            if (boutique == null)
            {
                _logger.LogError($"Boutique non trouvée avec l'id: {id}");

                return NotFound();
            }

            return View(boutique);
        }

        // POST: Boutique/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boutique = await _repository.Boutique.GetBoutiqueByIdAsync(id);

            if (boutique == null)
            {
                _logger.LogError($"Boutique non trouvée avec l'id: {id}");

                return NotFound();
            }

            _repository.Boutique.DeleteBoutique(boutique);
            await _repository.SaveAsync();
            
            return RedirectToAction(nameof(Index));
        }        
    }
}
