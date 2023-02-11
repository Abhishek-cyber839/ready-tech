using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
public class CoffeeController: ControllerBase {

    private readonly ILogger<CoffeeController> _logger;
    private static int _apiCalls = 0;
    public CoffeeController(ILogger<CoffeeController> logger){
        _logger = logger;
    }


    [HttpGet("/brew-coffee")]
    public IActionResult Get(){
        _apiCalls += 1;
        if(_apiCalls > 4) 
            return StatusCode(503);
        string todaysDate = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
        string month = todaysDate.Split("T")[0].Split("-")[1];
        string day =  todaysDate.Split("T")[0].Split("-")[2];
        if(month == "04" && day == "01")
             return StatusCode(418);
        return Ok(new OkResponse{
                message = "Your piping hot coffee is ready",  
                prepared = todaysDate
         });
    }
}