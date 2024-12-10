using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web2.Pages;

public class IndexModel : PageModel
{
    private readonly IConfigRepository _configRepository;

    public SelectList ConfigSelectList { get; set; } = default!;
    
    [BindProperty]
    public int ConfigId { get; set; }

    [BindProperty] public string GameId { get; set; } = null!;
    
    public IndexModel(IConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }

    public void OnGet()
    {
        var selectListData = _configRepository.GetConfigurationNames()
            .Select(name => new {id = name, value = name})
            .ToList();
        ConfigSelectList = new SelectList(selectListData, "id", "value");
    }
}
