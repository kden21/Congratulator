using Congratulator.Data.Interfaces;
using Congratulator.Data.Models;
using Congratulator.Data.Models.Enums;
using Congratulator.Data.Models.Responses;
using Congratulator.Data.Models.ViewModels;
using Congratulator.Data.Service.Interfaces;

namespace Congratulator.Data.Service.Implementations
{
    public class PersonService : IPersonService
    {
        public readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<BaseResponse<Person>> Congratulate(int id, int year)
        {
            var baseResponse = new BaseResponse<Person>();
            try
            {
                var person = await _personRepository.Get(id);
                if (person == null)
                {
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    baseResponse.Description = "Person not found";
                    return baseResponse;
                }
                person.YearLastCongratulations = year;
                await _personRepository.Update(person);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Person>()
                {
                    Description = $"[EditPerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> CreatePerson(PersonViewModel model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                byte[]? imageData = null;
                if (model.Avatar!=null)
                {
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                }
                var person = new Person()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth,
                    Avatar = imageData,
                    PhoneNumber = "+7" + model.PhoneNumber
            };
                await _personRepository.Create(person);
                baseResponse.Description = "Пользователь успешно добавлен";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreatePerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeletePerson(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var person = await _personRepository.Get(id);
                if (person == null)
                {
                    baseResponse.Description = "Person not found";
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    return baseResponse;
                }
                await _personRepository.Delete(person);
                baseResponse.Description = "Пользователь успешно удален";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[GetPersons] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Person>> EditPerson(int id, EditPersonViewModel model)
        {
            var baseResponse = new BaseResponse<Person>();
            try
            {
                var person = await _personRepository.Get(id);
                if (person == null)
                {
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    baseResponse.Description = "Person not found";
                    return baseResponse;
                }
                byte[]? imageData = null;
                if (model.Avatar != null)
                {
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                        person.Avatar = imageData;
                    }
                }
               
                person.Name = model.Name;
                person.Surname = model.Surname;
                person.DateOfBirth = model.DateOfBirth;
                if (model.YearLastCongratulations == null)
                    person.YearLastCongratulations = null;
                else
                    person.YearLastCongratulations = int.Parse(model.YearLastCongratulations);
                person.PhoneNumber = "+7" + model.PhoneNumber;
                await _personRepository.Update(person);
                baseResponse.Description = "Пользователь успешно изменен";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Person>()
                {
                    Description = $"[EditPerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Person>> GetPerson(int id)
        {
            var baseResponse = new BaseResponse<Person>();
            try
            {
                var person = await _personRepository.Get(id);
                if (person == null)
                {
                    baseResponse.Description = "Person not found";
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    return baseResponse;
                }
                baseResponse.Data = person;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Person>()
                {
                    Description = $"[GetPerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Person>>> GetPersonsByDate(DateTime date)
        {
            var baseResponse = new BaseResponse<IEnumerable<Person>>();
            try
            {
                var persons = await _personRepository.GetByDate(date);
                if (persons == null)
                {
                    baseResponse.Description = "Persons not found";
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    return baseResponse;
                }
                baseResponse.Data = persons;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Person>>()
                {
                    Description = $"[GetPersonByDate] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Person>>> GetPersons(StatusSorting statusSorting)
        {
            var baseResponse = new BaseResponse<IEnumerable<Person>>();
            try
            {
                var persons = await _personRepository.Select();
                if (persons.Count == 0)
                {
                    baseResponse.Data = persons;
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    return baseResponse;
                }
                IEnumerable<Person>? sortPersons = null;
                switch (statusSorting)
                {
                    case StatusSorting.Ascending:
                        sortPersons = persons.OrderBy(x => x.DateOfBirth.Day).OrderBy(x => x.DateOfBirth.Month);
                        break;
                    case StatusSorting.Descending:
                        sortPersons = persons.OrderByDescending(x => x.DateOfBirth.Day).OrderByDescending(x => x.DateOfBirth.Month);
                        break;
                    case StatusSorting.AscendingByName:
                        sortPersons = persons.OrderBy(x => x.Name);
                        break;
                    case StatusSorting.DescendingByName:
                        sortPersons = persons.OrderByDescending(x => x.Name);
                        break;
                    case StatusSorting.AscendingRelativeToToday:
                        sortPersons = persons.OrderBy(person => person.DateOfBirth.DayOfYear >= DateTime.Now.DayOfYear
                            ? person.DateOfBirth.DayOfYear - DateTime.Now.DayOfYear
                            : (DateTime.IsLeapYear(DateTime.Today.Year) ? 366 : 365) - DateTime.Now.DayOfYear + person.DateOfBirth.DayOfYear).ToList();
                        break;
                }
                baseResponse.Data = sortPersons;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Person>>()
                {
                    Description = $"[GetPersons] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<EditPersonViewModel>> GetPersonForEdit(int id)
        {
            var baseResponse = new BaseResponse<EditPersonViewModel>();
            EditPersonViewModel model = new();
            model.Person= await _personRepository.Get(id);
            try
            {
                var person = await _personRepository.Get(id);
                if (person == null)
                {
                    baseResponse.Description = "Person not found";
                    baseResponse.StatusCode = StatusCode.PersonNotFound;
                    return baseResponse;
                }
                model.Person = person;
                baseResponse.Data = model;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<EditPersonViewModel>()
                {
                    Description = $"[GetPersonForEdit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
