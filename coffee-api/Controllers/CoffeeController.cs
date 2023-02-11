using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
public class CoffeeController: ControllerBase {

    private readonly ILogger<CoffeeController> _logger;
    private static int _apiCalls = 0; // assuming request are only coming from same Ip address
    public CoffeeController(ILogger<CoffeeController> logger){
        _logger = logger;
    }


    [HttpGet("/brew-coffee")]
    public IActionResult Get(){
        _apiCalls += 1; // incremenet for every single call
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

// just add below lines of code if we wanna do this for every ip address we're getting requests from for requirement 2
// step 1 - get the remote ip where requests are coming from
// step 2 - store that ip address into the hash_Map or dictionary
// step 3 - increment request count i.e value in dictionary or hash_map for that specific ip address, if the entry doesn't exists create a new one
// step 4 - check if this is fifth request for that ip if so just return 503 - service unavailable
/** 
 private static Dictionary<string, int> apiCalls = new Dictionary<string, int>(); -- goes above contructor
 IPAddress? remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress; -- goes into the method
 string result = "";
 if (remoteIpAddress != null)
    // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
    // This usually only happens when the browser is on the same machine as the server.
    if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        remoteIpAddress.ToString();
  if(apiCall[remoteIpAddress] != null && apiCall[remoteIpAddress] > 4)
     return StatusCode(503);
  if(apiCall[remoteIpAddress] == null)
      apiCall[remoteIpAddress] = 1;
  if(apiCall[remoteIpAddress] != null && apiCall[remoteIpAddress] < 5)
     apiCall[remoteIpAddress] += 1;    
*/

// Extra Credit - I've not implemented it coz it requires payment details but here's how i would've implemented if it would've been free
/**
step 1 - create an interface let's say IWeatherService with a method which will return int temp by hitting that 3rd party service;
step 2 - implement that interaface WeatherService : IWeatherService
step 3 - add that intp the dependency injection container through IServiceColletion in program.cs, add it as singleton
for ex - service.AddSingleton<IWeatherService, IWeatherService>
step 4 - inject that into the CoffeeController's contructor -
         private readonly IWeatherService _weatherService;
         public CoffeeController(ILogger<CoffeeController> logger, IWeatherService weatherService)
              _weatherService = weatherService;
step 5 - later call that method we've implemented as _weatherService.getTemp();
step 6 - then where we're returning json reponse check 
   {
     message = temp > 30 ? "Your refreshing iced coffee is ready"  : "Your piping hot coffee is ready"
     prepared = todaysDate
   }
*/

// Non-functional Requirements
// for this i don't really have much to implement as we're not using any repository
// but we can test 3 cases
// test case 1 - for today's date we get 200 ok response
// test case 2 - run a for loop/ while loop and send requests until we get 503 (as we should get it after 4 requests)
// test case 3 - which can only only be tested when the date is 1st april  - 418 status code