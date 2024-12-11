using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web2.Pages;

public class IndexModel(IConfigRepository configRepository, IGameRepository gameRepository) : PageModel
{
    public SelectList ConfigSelectList { get; set; } = default!;

    [BindProperty]
    public string ConfigId { get; set; } = null!;

    [BindProperty] 
    public string GameId { get; set; } = null!;

    public void OnGet()
    {
        var selectListData = configRepository.GetConfigurationNames()
            .Select(name => new {id = name, value = name})
            .ToList();
        ConfigSelectList = new SelectList(selectListData, "id", "value");
    }
    
    public IActionResult OnPostCreateGame()
    {
        var config = configRepository.GetConfigurationByName(ConfigId);
        TicTacTwoBrain brain = new();
        brain.LoadConfig(config);
        var snapshot = brain.CreateSnapshot();
        snapshot.Name = GameId;
        gameRepository.Save(snapshot);
        return RedirectToPage("./Game", new {GameId });
    }
}
