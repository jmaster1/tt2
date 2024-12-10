using DAL;
using Console_App;

IConfigRepository configRepository = new ConfigRepositoryJson();
IGameRepository gameRepository = new GameRepositoryJson();
var mainController = new MainController(configRepository, gameRepository);
mainController.Run();
