using ProjectPierre.DTO.UserDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Mappers
{
    public static class UserMapper
    {
        public static LoggedInUserDTO ToLoggedInUserDTOFromUser (this User user, string token)
        {
            return new LoggedInUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
