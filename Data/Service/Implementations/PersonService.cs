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
                    Avatar = imageData
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
                if (model.Name!=null)
                    person.Name = model.Name;
                if (model.Surname != null)
                    person.Surname = model.Surname;
                if (model.DateOfBirth != null)
                    person.DateOfBirth = (DateTime)model.DateOfBirth;
                if (model.YearLastCongratulations != null)
                    person.YearLastCongratulations = int.Parse(model.YearLastCongratulations);
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

        public async Task<BaseResponse<IEnumerable<Person>>> GetPersonByName(string name)
        {
            var baseResponse = new BaseResponse<IEnumerable<Person>>();
            try
            {
                var persons = await _personRepository.GetByName(name);
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
                    Description = $"[GetPersonByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Person>>> GetPersons()
        {
            var baseResponse = new BaseResponse<IEnumerable<Person>>();
            try
            {
                var persons = await _personRepository.Select();
                if (persons.Count == 0)
                {
                    baseResponse.Data = persons;
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
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
                    Description = $"[GetPersons] : {ex.Message}",
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
                    baseResponse.StatusCode = StatusCode.OK;
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
                        //Переписать этот метод и swith под вопросом?
                        IEnumerable<Person>  sPersons = persons.OrderBy(x => x.DateOfBirth.Day).OrderBy(x => x.DateOfBirth.Month);
                        List<Person>? sortPersons1 = new List<Person>();
                        List<Person>? sortPersons2 = new List<Person>();
                        foreach (Person person in sPersons)
                        {
                            if((person.DateOfBirth.Month<DateTime.Today.Month)||((person.DateOfBirth.Month == DateTime.Today.Month) && (person.DateOfBirth.Day < DateTime.Today.Day)))
                                sortPersons1.Add(person);
                            else
                                sortPersons2.Add(person);
                        }
                        sortPersons = sortPersons2.Concat(sortPersons1).ToList();
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
    }
}
