using Congratulator.Data.Interfaces;
using Congratulator.Data.Models;
using Congratulator.Data.Models.Enums;
using Congratulator.Data.Models.Responses;
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

        public async Task<BaseResponse<bool>> CreatePerson(Person model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var person = new Person()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth
                };
                await _personRepository.Create(person);
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

        public async Task<BaseResponse<Person>> EditPerson(int id, Person model)
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
                person.Name = model.Name;
                person.Surname = model.Surname;
                person.DateOfBirth = model.DateOfBirth;
                await _personRepository.Update(person);
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
    }
}
