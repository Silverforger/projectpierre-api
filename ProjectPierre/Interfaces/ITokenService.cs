using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        //void SetRefreshToken(User user, RefreshToken refreshToken);
    }
}
