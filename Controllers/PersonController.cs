using Congratulator.Data.Models;
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
        public async Task<ActionResult> GetPersons()
        {
            var response = await _personService.GetPersons();
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

        [HttpGet]
        public async Task<ActionResult> CreatePerson(Person model)
        {
            var response = await _personService.CreatePerson(model);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return RedirectToAction("GetPersons");
            return RedirectToAction("Error");
        }
    }
}
