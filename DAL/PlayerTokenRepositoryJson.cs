using GameBrain;

namespace DAL;

public class PlayerTokenRepositoryJson : AbstractRepositoryJson<PlayerToken>, IPlayerTokenRepository
{
    
    protected override string GetExtension()
    { 
        return ".playerToken.json";
    }
    
    public void Save(PlayerToken entity)
    {
        Save(entity, entity.Token);
    }

    PlayerToken? IPlayerTokenRepository.Load(string token)
    {
        return LoadByName(token);
    }
}
