using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.DemoQA;

public interface IHomePage : ITestWareComponent
{
    void NavigateTo(string url);
}
