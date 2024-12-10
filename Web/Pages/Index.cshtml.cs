using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web2.Pages;

public class IndexModel : PageModel
{
    private readonly IConfigRepository _configRepository;
    
    private readonly ILogger<IndexModel> _logger;

    //public SelectList ConfigSelectList { get; set; } = default!;
    
    [BindProperty]
    public int ConfigId { get; set; }
    
    public IndexModel(ILogger<IndexModel> logger, IConfigRepository configRepository)
    {
        _logger = logger;
        _configRepository = configRepository;
    }

    public void OnGet()
    {
        var selectListData = _configRepository.GetConfigurationNames();
        var ConfigSelectList = new SelectList(selectListData, "id", "value");
    }
}
