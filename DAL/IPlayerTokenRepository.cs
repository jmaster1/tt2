using GameBrain;

namespace DAL;

public interface IPlayerTokenRepository
{
    void Save(PlayerToken entity);
    
    PlayerToken? Load(string token);
}
