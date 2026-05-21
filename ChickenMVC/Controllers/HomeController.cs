using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChickenMVC.Models;

namespace ChickenMVC.Controllers;

public class HomeController : Controller
{

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HomeController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var apiBaseUrl = _configuration["ChickenApi:BaseUrl"]
            ?? _configuration["CHICKEN_API_BASE_URL"]
            ?? "http://localhost:5232";
        var chickensEndpoint = $"{apiBaseUrl.TrimEnd('/')}/api/chickens";

        try
        {
            var response = await _httpClient.GetStringAsync(chickensEndpoint);
            ViewBag.Chickens = response;
        }
        catch (Exception ex)
        {
            // Handle error (e.g., log it)
            ViewBag.Chickens = $"Error fetching chickens: {ex.Message}";
        }
        return View();
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
