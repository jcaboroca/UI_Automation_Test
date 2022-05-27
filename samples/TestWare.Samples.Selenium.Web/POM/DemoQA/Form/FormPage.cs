using OpenQA.Selenium;
using TestWare.Core.Libraries;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.DemoQA;

public class FormPage : WebPage, IFormPage
{
    [FindsBy(How = How.XPath, Using = "//*[@id='submit']")]
    private IWebElement SubmitButton { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='userName']")] 
    private IWebElement FullNameInput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='userEmail']")]
    private IWebElement EMailInput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'mr-sm-2 field-error') and contains(@id, 'userEmail')]")]
    private IWebElement EMailError { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'form-control') and contains(@id, 'currentAddress')]")] 
    private IWebElement CurrentAddressInput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'form-control') and contains(@id, 'permanentAddress')]")]
    private IWebElement PermanentAddressInput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='output']")]
    private IWebElement OutputArea { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'mb-1') and contains(@id, 'currentAddress')]")]
    private IWebElement CurrentAddressOutput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'mb-1') and contains(@id, 'permanentAddress')]")]
    private IWebElement PermanentAddressOutput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='name']")]
    private IWebElement FullNameOutput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='email']")]
    private IWebElement EMailOutput { get; set; }


    public FormPage(IBrowserDriver driver) : base(driver)
    {
    }
    public void ClickSubmitButton()
        => ClickElement(this.SubmitButton);
    public void SetUserName(string userName)
        => SendKeysElement(this.FullNameInput, userName);
    public void SetEmail(string Email)
        => SendKeysElement(this.EMailInput, Email);
    public void SetCurrentAddress(string CurrentAddress)
        => SendKeysElement(this.CurrentAddressInput, CurrentAddress);
    public void SetPermanentAddress(string PermanentAddress)
        => SendKeysElement(this.PermanentAddressInput, PermanentAddress);

    public string GetUserNameOutput() 
    {
        var username = FullNameOutput.Text.Split(':');
        return username[1]; 
    } 
    public string GetEmailOutput() 
    {
        var eMail = EMailOutput.Text.Split(':');
        return eMail[1];
    }
    public string GetCurrentAddressOutput() 
    {
        var currentAddress = CurrentAddressOutput.Text.Split(':');
        return currentAddress[1];
    }
    public string GetPermanentAddressOutput() 
    {
        var permanentAddress = PermanentAddressOutput.Text.Split(':');
        return permanentAddress[1];
    }

    public bool CheckIfEmailTextBoxError()
    {
        var EmailTextBoxerror = EMailError.Displayed;
        return EmailTextBoxerror;
    }
    public bool CheckIfOutputArea()
    {
        var OutputAreaVisible = OutputArea.Displayed;
        return OutputAreaVisible;
    }



}
