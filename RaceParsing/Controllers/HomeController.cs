using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.ParsingRaceStorage;
using Microsoft.AspNetCore.Mvc;
using ParserLogic;
using RaceParsing.Models;

namespace RaceParsing.Controllers
{
    public class HomeController : Controller
    {
        ParserWorker<RaceModel> _parser;
        private IRacingRepository _racingRepository;

        public HomeController(IRacingRepository repository)
        {
            _racingRepository = repository;
        }

        public IActionResult Index()
        {
            var model = new FormulaModel();
            return View(model);
        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(FormulaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _parser = new ParserWorker<RaceModel>(new FormulaParser());
            _parser.Settings = new ParserSettings() { Link = model.Link };

            RaceModel parsed = null;
            try
            {
                parsed = await _parser.Start();
                ViewBag.ResultTable = parsed.ResultTable;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(model.Link), $"Ошибка при парсинге: {ex.Message}");
                ViewBag.ResultTable = null;
            }

            if (parsed == null)
                return View(model);

            try
            {
                RacingInformation info = _racingRepository.Convert(parsed);
                await _racingRepository.Save(info);
            }
            catch
            {
                ModelState.AddModelError(nameof(model.Link), "Ошибка при сохранении данных");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Refresh(FormulaModel model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
