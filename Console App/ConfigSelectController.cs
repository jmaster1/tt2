using DAL;
using GameBrain;
using MenuSystem;
using static MenuSystem.MenuBuilder;

namespace Console_App;

internal class ConfigSelectController(IConfigRepository configRepository) : AbstractController
{
    internal void SelectConfig(Action<GameConfiguration> action)
    {
        var items = configRepository.GetConfigurationNames()
            .Select((name, index) => MenuItem((index + 1).ToString(), name, 
                () => action(configRepository.Load(name))))
            .ToArray();
        new Menu(Header, [.. items]).Run();
    }
}
