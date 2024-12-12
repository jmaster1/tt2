using GameBrain;

namespace DAL;

public interface IConfigRepository
{
    List<string> GetConfigurationNames();

    GameConfiguration Load(string name);

    void Save(GameConfiguration gameConfig);
}
