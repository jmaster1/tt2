
using DAL;
using GameBrain;
using MenuSystem;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class ConfigController(IConfigRepository configRepository) : AbstractController
{
    private GameConfiguration _configuration = new();

    internal GameConfiguration SelectConfig()
    {
        string? chosenName = null;
        var items = configRepository.GetConfigurationNames()
            .Select((name, index) => MenuItem(index++.ToString(), name, () => chosenName = name))
            .ToArray();
        new Menu("Tic-Tac-Two - choose configuration", [.. items])
            .Run();
        return chosenName == null ? default : configRepository.GetConfigurationByName(chosenName);
    }

    public void Run()
    {
        Menu(Header,
                MenuItem("+", "Create config", CreateConfig)
            )
            .BeforeDraw(() =>
            {
                //1Console.WriteLine(JsonStringSerializer.ToString(_configuration));
            })
            .RunUntilExit();
    }

    private void CreateConfig()
    {
        new ConfigEditController(configRepository, new GameConfiguration()).Run();
    }
}
