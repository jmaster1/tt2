namespace Console_App;

internal class AbstractController
{
    internal string Header => "Tic-Tac-Two:" + GetType().Name.Replace("Controller", "");
}
