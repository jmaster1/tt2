using GameBrain;

namespace DAL;

public class GameRepositoryJson : AbstractRepositoryJson<GameSnapshot>, IGameRepository
{
    private const string LastSnapshotName = "lastGameSnapshot";
    
    protected override string GetExtension()
    { 
        return ".game.json";
    }

    public GameSnapshot? LoadLastSnapshot()
    {
        return LoadByName(LastSnapshotName);
    }

    public void SaveLastSnapshot(GameSnapshot snapshot)
    {
        Save(snapshot, LastSnapshotName);
    }

    public GameSnapshot? Load(string name)
    {
        return LoadByName(name);
    }

    public void Save(GameSnapshot snapshot)
    {
        Save(snapshot, snapshot.Name);
    }
}
