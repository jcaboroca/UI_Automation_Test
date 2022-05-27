using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.DemoQA;

public interface IFormPage : ITestWareComponent
{
    void ClickSubmitButton();
    void SetUserName(string userName);
    void SetEmail(string Email);
    void SetCurrentAddress(string CurrentAddress);
    void SetPermanentAddress(string PermanentAddress);

    string GetUserNameOutput();
    string GetEmailOutput();
    string GetCurrentAddressOutput();
    string GetPermanentAddressOutput();
    bool CheckIfEmailTextBoxError();
    bool CheckIfOutputArea();
}
