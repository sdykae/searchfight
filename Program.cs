using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
namespace searchfight
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length==0) {
        Console.WriteLine("This cmd line app requires at least one string argument");
        Environment.Exit(0);
        
      }
      // string[] languages = {".net", "java"};
      string[] languages = args;
      ChromeOptions options = new ChromeOptions();
      options.AddArgument("--headless");
      // //  options.AddArgument("--no-sandbox");
      options.AddArgument("--disable-gpu");
      options.AddArgument("--window-size=1280x1696");
      // //  options.AddArgument("--user-data-dir=/tmp/user-data");
      options.AddArgument("--hide-scrollbars");
      // //  options.AddArgument("--enable-logging");
      // //  options.AddArgument("--log-level=0");
      // //  options.AddArgument("--v=99");
      // //  options.AddArgument("--single-process");
      // //  options.AddArgument("--data-path=/tmp/data-path");
      // //  options.AddArgument("--ignore-certificate-errors");
      // //  options.AddArgument("--homedir=/tmp");
      // //  options.AddArgument("--disk-cache-dir=/tmp/cache-dir");
      // //  options.AddArgument("user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");


      IWebDriver driver = new ChromeDriver(options);
      // driver.Navigate().GoToUrl("https://google.com/ncr");
      // var element = driver.FindElement(By.XPath("/html/body/div/div[2]/form/div[2]/div[1]/div[1]/div/div[2]/input"));
      // element.SendKeys("keys");
      // element.Submit();

      // // var results = driver.FindElement(By.XPath("//*[@id=\"result-stats\"]"));
      // // Console.WriteLine(results.Text);
      // ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
      // ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
      // ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
      // driver.SwitchTo().Window(driver.WindowHandles[2]);
      // Console.WriteLine("\nWhat is your name? ");
      // var name = Console.ReadLine();
      // var date = DateTime.Now;
      // Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}! xd ___ {args[0]}");
      // Console.Write("\nPress any key to exit...");
      // Console.ReadKey(true);
      // Regex rx = new Regex(@"About(.*?)");
      // Task.Run(async () => {
      //   var res = await google_result_count_by_query_string(languages[0]);
      //   MatchCollection matches = rx.Matches(res);

      //   // Console.WriteLine(res);
      //   foreach (var match in matches) {
      //     Console.WriteLine(match.ToString());
      //   }
      // }).GetAwaiter().GetResult();
      // var result  = google_get_result_count_from_string("java",ref driver);
      // driver.Close();
      // Console.WriteLine(result);
      // repeat(ref driver);
      // repeat(ref driver);
      // repeat(ref driver);
      // repeat2(ref driver);

      List<(string google_result, string bing_result)> results = new List<(string google_result,string bing_result)>();

      foreach (var language in languages){
        (string bing,string google) result = search_engines_results(language,ref driver);
        results.Add(result);
      }
      foreach (var (item,index) in results.Select((value,i)=>(value,i))){
        Console.WriteLine($"{languages[index]} Google {item.google_result} Bing {item.bing_result}");
      }

      
    }
    public static (string bing,string google) search_engines_results(string language, ref IWebDriver driver){
      return (google_search_results(language,ref driver),bing_search_results(language,ref driver));
    }
    public static string google_search_results(string query,ref IWebDriver driver){
      var result  = google_get_result_count_from_string(query,ref driver);
      driver.Close();
      driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
      // Console.WriteLine(result);
      return result;
    }
    public static string bing_search_results(string query,ref IWebDriver driver){
      var result  = bing_get_result_count_from_string(query,ref driver);
      driver.Close();
      driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
      return result;
      // Console.WriteLine(result);
    }

    // static string google_get_result_count(string word, IWebDriver driver)
    // {
    //   ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
    //   driver.Navigate().GoToUrl("https://google.com/ncr");
    //   var element = driver.FindElement(By.XPath("/html/body/div/div[2]/form/div[2]/div[1]/div[1]/div/div[2]/input"));
    //   element.SendKeys("keys");
    //   element.Submit();
    //   var results = driver.FindElement(By.XPath("//*[@id=\"result-stats\"]"));
    //   // Console.WriteLine(results.Text);
    //   return results.Text;
    // }
    public static string google_get_result_count_from_string(string query, ref IWebDriver driver){
      ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
      driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
      driver.Navigate().GoToUrl($"https://www.google.com/search?q={query}&hl=en");
      var results = driver.FindElement(By.XPath("//*[@id=\"result-stats\"]"));
      //*[@id="result-stats"]
      // ((IJavaScriptExecutor)driver).ExecuteScript("window.close();");
      // driver.Close();
      return trim_seconds.Replace(results.Text,"");
    }
    public static Regex trim_seconds = new Regex("(?=\\()(.*)(?<=\\))|About",RegexOptions.Compiled);
    public static string bing_get_result_count_from_string(string query, ref IWebDriver driver){
      ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
      driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
      driver.Navigate().GoToUrl($"https://www.bing.com/search?q={query}&setlang=en-US");
      // var results = driver.FindElement(By.XPath("/html/body/div[1]/main/div/span[1]"));
      var results = driver.FindElement(By.XPath("//*[@class=\"sb_count\"]"));
      //*[@id="b_tween"]
      //*[@id="result-stats"]
      // ((IJavaScriptExecutor)driver).ExecuteScript("window.close();");
      // driver.Close();
      return results.Text;
    }




    public static async Task<string> google_result_count_by_query_string(string query)
    {
      HttpClient client = new HttpClient();
      var response = await client.GetAsync($"https://www.google.com/search?q={query}&hl=en");
      return await response.Content.ReadAsStringAsync();
    }
  }
}
