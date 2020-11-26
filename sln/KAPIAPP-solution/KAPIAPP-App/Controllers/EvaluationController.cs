using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KAPIAPP.Services.Entity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KAPIAPP.Services.DTO.EvaluationDto;
using KAPIAPP.Services.Repository.Contracts;
using KAPIAPP.Services.Entity.Helpers;

namespace KAPIAPP_App.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class EvaluationController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;

        public EvaluationController(ApplicationContext context,
                                    IMapper mapper,
                                    UserManager<User> userManager,
                                    IRepositoryWrapper repository,
                                    ILoggerManager logger)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
            _logger = logger;
        }

        // GET: Evaluation
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, EvaluationParameters evaluationParameter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.DateSortParam = sortOrder == "date_desc" ? "" : "date_desc";

            if (evaluationParameter.BoutiqueName != null)
            {
                evaluationParameter.PageSize = 1;
            }
            else
            {
                evaluationParameter.BoutiqueName = currentFilter;
            }
            ViewBag.CurrentFilter = evaluationParameter.BoutiqueName;

            ViewBag.PageSize = evaluationParameter.PageSize;
            ViewBag.PageNumber = evaluationParameter.PageNumber;

            var listeEvaluation = await _repository.Evaluation.GetEvaluationPagined(evaluationParameter);

            if (listeEvaluation == null)
            {
                _logger.LogError($"Erreur la liste des évaluations n'a pas pu être récupérée du serveur");
                return BadRequest("la liste des évaluations n'a pas pu être retournée");
            }

            //error automapper null arg in constructor
            //var listeEvalToReturn = _mapper.Map<PagedList<EvaluationListeDto>>(listeEvaluation);

            return View(listeEvaluation);
        }

        // GET: Evaluation/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var evaluation = await _repository.Evaluation.GetEvaluationByIdAsync(id);
            if (evaluation == null)
            {
                _logger.LogError($"l'évaluation avec id : {id} n'existe pas");
                return NotFound();
            }

            var evaluationToReturn = _mapper.Map<EvaluationListeDto>(evaluation);
            return View(evaluationToReturn);
        }

        // GET: Evaluation/Create/idBoutique
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var boutique = await _repository.Boutique.GetBoutiqueByIdAsync(id);

            if (boutique == null)
            {
                _logger.LogError($"la boutique avec id: {id} n'existe pas dans la base");
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            //_logger.LogDebug($"user id: {user.Id}");

            var evaluation = new EvaluationCreationDto { BoutiqueId = id, UserId = user.Id, EvaluationDate = DateTime.Now };
            //_logger.LogDebug($" httpGet boutiqueId : {evaluation.BoutiqueId}");

            var evaluationToReturn = _mapper.Map<EvaluationCreationDto>(evaluation);

            return View(evaluationToReturn);
        }

        [HttpPost("{id}"), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int id, EvaluationCreationDto evaluationCreation)
        {
            if (!ModelState.IsValid)
            {
                return View(evaluationCreation);
            }
            var boutique = await _repository.Boutique.GetBoutiqueByIdAsync(id);

            if (boutique == null)
            {
                _logger.LogError($"la boutique avec id: {id} n'existe pas dans la base");
                return NotFound();
            }
            evaluationCreation.BoutiqueId = id;

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            evaluationCreation.UserId = user.Id;

            evaluationCreation.NoteGlobale = evaluationCreation.NoteProprete +
                                            evaluationCreation.NoteEquipe +
                                            evaluationCreation.NoteAgencement +
                                            evaluationCreation.NoteComportement;

            _logger.LogDebug($"action create : objet evaluation boutiqueId: {evaluationCreation.BoutiqueId}  evaluation user id {evaluationCreation.UserId}");
            var evaluation = _mapper.Map<Evaluation>(evaluationCreation);

            _repository.Evaluation.Create(evaluation);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Evaluation/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var evaluation = await _repository.Evaluation.GetEvaluationByIdAsync(id);
            if (evaluation == null)
            {
                _logger.LogError($"evalution avec id {id} non trouvée");
                return NotFound();
            }

            var evaluationToReturn = _mapper.Map<EvaluationToEditDto>(evaluation);

            return View(evaluationToReturn);
        }

        // TODO POST: Evaluation/Edit/5 !!!! créé un nouvel objet en écrasant l'ancien sans boutique ni user...
        //si httpput erreur 405 si httppost pas d'erreur mais id boutique et user ecrasé
        [HttpPut("{id}"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPut(int id, EvaluationToEditDto evaluationEdit)
        {
            if (evaluationEdit.Id != id)
            {
                _logger.LogError($"l'identifiant {id} envoyé ne correspond à aucune evaluations");
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Modèle d'évaluation non valide");
                return View(evaluationEdit);
            }
            evaluationEdit.NoteGlobale = evaluationEdit.NoteAgencement + evaluationEdit.NoteComportement
                                        + evaluationEdit.NoteEquipe + evaluationEdit.NoteProprete;

            var evalToUpdate = _mapper.Map<Evaluation>(evaluationEdit);

            _logger.LogDebug($"debug: objet evalToUpdate: {evalToUpdate}");

            _repository.Evaluation.UpdateEvaluation(evalToUpdate);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Evaluation/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var evaluation = await _repository.Evaluation.GetEvaluationByIdAsync(id);

            if (evaluation == null)
            {
                _logger.LogError($"evalution avec id {id} non trouvée");
                return NotFound();
            }
            var evaluationToReturn = _mapper.Map<EvaluationListeDto>(evaluation);
            return View(evaluationToReturn);
        }

        // POST: Evaluation/Delete/5
        //TODO BUG erreur 405 avec httpDelete aussi
        //[HttpDelete("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluation = await _repository.Evaluation.GetEvaluationByIdAsync(id);

            if (evaluation == null)
            {
                _logger.LogError($"evalution avec id {id} non trouvée");
                return NotFound();
            }

            _repository.Evaluation.DeleteEvaluation(evaluation);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }        
    }
}
