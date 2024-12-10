using GameBrain;

namespace DAL;

public interface IGameRepository
{
    GameSnapshot? LoadLastSnapshot();
    
    void SaveLastSnapshot(GameSnapshot snapshot);
    
    GameSnapshot? Load(string name);
    
    void Save(GameSnapshot snapshot);
}
