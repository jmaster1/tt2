using DAL;
using Console_App;

IConfigRepository configRepository = new ConfigRepositoryJson();
IGameRepository gameRepository = new GameRepositoryJson();
new MainController(configRepository, gameRepository).Run();
