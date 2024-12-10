using GameBrain;

namespace DAL;

public interface IGameRepository
{
    GameSnapshot? LoadLastSnapshot();
    
    void SaveLastSnapshot(GameSnapshot snapshot);
}
