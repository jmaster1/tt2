
using DAL;
using GameBrain;
using static MenuSystem.MenuBuilder;

namespace Console_App
{
    internal class ConfigController
    {
        private readonly IConfigRepository configRepository;

        public ConfigController(IConfigRepository configRepository)
        {
            this.configRepository = configRepository;
        }

        internal GameConfiguration SelectConfig()
        {
            string? chosenName = null;
            subMenu("Tic-Tac-Two - choose configuration",
                configRepository.GetConfigurationNames()
                    .Select((name, index) => menuItem(index++.ToString(), name, () => chosenName = name))
                    .ToArray())
            .Run();
            return chosenName == null ? default : configRepository.GetConfigurationByName(chosenName);
        }
    }
}
