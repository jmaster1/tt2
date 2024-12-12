using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web2.Pages;

public class IndexModel(
    IConfigRepository configRepository, 
    IGameRepository gameRepository, 
    IPlayerTokenRepository playerTokenRepository) : PageModel
{
    public SelectList ConfigSelectList { get; set; } = default!;

    [BindProperty]
    public string ConfigId { get; set; } = null!;
    
    public PlayerToken? XPlayerToken { get; set; }
    
    public PlayerToken? OPlayerToken { get; set; }

    private void Load()
    {
        var selectListData = configRepository.GetConfigurationNames()
            .Select(name => new {id = name, value = name})
            .ToList();
        ConfigSelectList = new SelectList(selectListData, "id", "value");
    }
    
    public void OnGet()
    {
        Load();
    }
    
    public IActionResult OnPostCreateGame()
    {
        var gameId = Guid.NewGuid().ToString();
        var config = configRepository.Load(ConfigId);
        var brain = new TicTacTwoBrain().LoadConfig(config);
        var snapshot = brain.CreateSnapshot(gameId);
        gameRepository.Save(snapshot);
        
        XPlayerToken = CreatePlayerToken(EGamePiece.X, gameId);
        OPlayerToken = CreatePlayerToken(EGamePiece.O, gameId);

        Load();
        return Page();
    }

    private PlayerToken CreatePlayerToken(EGamePiece type, string gameId)
    {
        var playerToken = new PlayerToken()
        {
            Type = type,
            GameId = gameId,
            Token = Guid.NewGuid().ToString()
        };
        playerTokenRepository.Save(playerToken);
        return playerToken;
    }
}
