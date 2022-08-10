using Congratulator.Data.Models;
using Congratulator.Data.Models.Responses;
using Congratulator.Data.Models.ViewModels;

namespace Congratulator.Data.Service.Interfaces
{
    public interface IPersonService
    {
		Task<BaseResponse<IEnumerable<Person>>> GetPersons();
		Task<BaseResponse<Person>> GetPerson(int id);
		Task<BaseResponse<bool>> CreatePerson(PersonViewModel product);
		Task<BaseResponse<bool>> DeletePerson(int id);
		Task<BaseResponse<IEnumerable<Person>>> GetPersonByName(string name);
		Task<BaseResponse<IEnumerable<Person>>> GetPersonsByDate(DateTime date);
		Task<BaseResponse<Person>> EditPerson(int id, Person model);
		Task<BaseResponse<Person>> Congratulate(int id, int year);
	}
}
