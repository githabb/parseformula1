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
            _parser = new ParserWorker<RaceModel>(new FormulaParser());
            _parser.Settings = new ParserSettings() { Link = model.Link };

            RaceModel parsed = await _parser.Start();


            RacingInformation info = _racingRepository.Convert(parsed);
            await _racingRepository.Save(info);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Refresh(FormulaModel model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
