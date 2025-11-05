
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.User.Queries.GetUserByUserNameAndPassword
{
    public class GetUserByUserNameAndPasswordQuery: IGetUserByUserNameAndPasswordQuery
    {
        private readonly IDataBaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUserByUserNameAndPasswordQuery(IDataBaseService dataBaseService, IMapper mapper)
        {
            _databaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<GetUserByUserNameAndPasswordModel> Execute(string userName, string password) { 
            var entity = await _databaseService.Users
                .FirstOrDefaultAsync(u => u.Username == userName && u.Password == password);
            return _mapper.Map<GetUserByUserNameAndPasswordModel>(entity);
        }
    }
}
