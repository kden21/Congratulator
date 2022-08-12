using Congratulator.Data.Models;
using Congratulator.Data.Models.Enums;
using Congratulator.Data.Models.ViewModels;
using Congratulator.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Congratulator.Controllers
{
    public class PersonController : Controller
    {
        public readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult> Congratulate(string contr, string act,int id, Person model, int year)
        {
            var response = await _personService.Congratulate(id, year);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return RedirectToAction(act, contr);
            return RedirectToAction("Error");
        }

        [HttpGet, ActionName("SortPersons")]
        
        public async Task<ActionResult> GetPersons()
        { 
            
            var response = await _personService.GetPersons();
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return View(response.Data.ToList());
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<ActionResult> GetPersons(StatusSorting statusSorting)
        {

            var response = await _personService.GetPersons(statusSorting);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return View(response.Data.ToList());
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<ActionResult> GetPerson(int id)
        {

            var response = await _personService.GetPerson(id);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var response = await _personService.DeletePerson(id);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return RedirectToAction("GetPersons");
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _personService.CreatePerson(model);
                if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                    return RedirectToAction("GetPersons", "Person");
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreatePerson() => View();

        [HttpPost]
        public async Task<ActionResult> EditPerson(int id, EditPersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _personService.EditPerson(id, model); ;
                if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                    return RedirectToAction("GetPersons", "Person");
                ModelState.AddModelError("EditPerson", response.Description);
            }
            return View(model);
            //if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
            //return RedirectToAction("GetPersons");
            //return RedirectToAction("Error");
        }
        [HttpGet]
        public ActionResult EditPerson(int id) =>View();
    }
}
