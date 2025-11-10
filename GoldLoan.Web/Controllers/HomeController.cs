using GoldLoan.Application.Interfaces;
using GoldLoan.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoldLoan.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILoanService _loanService;
    private readonly IStatisticsService _statisticsService;

    public HomeController(ILogger<HomeController> logger, ILoanService loanService, IStatisticsService statisticsService)
    {
        _logger = logger;
        _loanService = loanService;
        _statisticsService = statisticsService;
    }

    public async Task<IActionResult> Index()
    {
        var defaulters = await _loanService.GetDefaulterLoansAsync();
        var upcomingRenewals = await _loanService.GetUpcomingRenewalsAsync(30);
        var totalLoans = await _statisticsService.GetTotalLoansAsync();
        var totalLoanAmount = await _statisticsService.GetTotalLoanAmountAsync();
        var activeLoans = await _statisticsService.GetActiveLoansAsync();

        var model = new DashboardViewModel
        {
            DefaulterLoans = defaulters.ToList(),
            UpcomingRenewals = upcomingRenewals.ToList(),
            TotalLoans = totalLoans,
            TotalLoanAmount = totalLoanAmount,
            ActiveLoans = activeLoans
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
