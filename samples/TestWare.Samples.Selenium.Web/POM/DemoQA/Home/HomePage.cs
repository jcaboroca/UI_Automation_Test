using OpenQA.Selenium;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.DemoQA;

public class HomePage : WebPage, IHomePage
{
    private const string HomeUrl = "https://demoqa.com/text-box";

    public HomePage(IBrowserDriver driver) : base(driver)
    {
        Url = HomeUrl;
        NavigateToUrl();
    }

    public void NavigateTo(string url)
    {
        Url = url;
        NavigateToUrl();
    }
}

