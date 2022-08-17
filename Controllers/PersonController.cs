
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

        //Скорее всего нужно удалить
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
            //DateTime date1 = new DateTime(2000, 1, 10);
            //DateTime date2 = new DateTime(2020, 1, 7);
            //var date3 = (date2.Day - DateTime.Today.Day);
            //var date4 = date1 - DateTime.Today;
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
                var response = await _personService.EditPerson(id, model); 
                if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                    return RedirectToAction("GetPersons", "Person");
                ModelState.AddModelError("EditPerson", response.Description);
            }
            else
            {
                var response = await _personService.GetPerson(id);
                if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                {
                    model.Person = response.Data;
                    //return RedirectToAction("GetPersons", "Person");
                }
                   
                //ModelState.AddModelError("EditPerson", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditPerson(int id)
        {
            var response = await _personService.GetPersonForEdit(id);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }
    }
}
