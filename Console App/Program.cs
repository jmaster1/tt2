using DAL;
using Console_App;

IConfigRepository configRepository = new ConfigRepositoryJson();
var configController = new ConfigController(configRepository);
var mainController = new MainController(configRepository, configController);
mainController.Run();
