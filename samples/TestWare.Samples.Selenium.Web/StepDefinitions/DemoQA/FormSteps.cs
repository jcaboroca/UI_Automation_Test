using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.DemoQA;

namespace TestWare.Samples.Selenium.Web.StepDefinitions.DemoQA;

[Binding]
public sealed class FormSteps
{
    private readonly IEnumerable<IHomePage> homePages;
    public FormSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var tags = featureContext.FeatureInfo.Tags.Concat(scenarioContext.ScenarioInfo.Tags);
        homePages = ContainerManager.GetTestWareComponents<IHomePage>(tags);
    }

    [Given(@"the user opens Chrome browser and navigates to demoQA webpage")]
    public static void GivenTheusernavigatestodemoQAwebpage()
    {
        var homePage = ContainerManager.GetTestWareComponent<IHomePage>();
        homePage.NavigateTo("https://demoqa.com/text-box");
    }

    [When(@"the user fulfills Full Name field with '([^']*)'")]
    public static void WhenTheuserfulfillsfullnamewithdata(string data)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();                
        formPage.SetUserName(data);                             
    }
    
    [When(@"the user fulfills Email field with '([^']*)'")]
    public static void WhenTheuserfulfillsemailwithdata(string data)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        formPage.SetEmail(data);
    }

    [When(@"the user fulfills Current Address field with '([^']*)'")]
    public static void WhenTheuserfulfillscurrentaddresswithdata(string data)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        formPage.SetCurrentAddress(data);
    }

    [When(@"the user fulfills Permanent Address field with '([^']*)'")]
    public static void WhenTheuserfulfillspermanentaddresswithdata(string data)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        formPage.SetPermanentAddress(data);
    }

    [When(@"the user clicks on submit button")]
    public static void ThenTheMessageFromAppearsOn()
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        formPage.ClickSubmitButton();
    }

    [Then(@"the output form FullName is '([^']*)'")]
    public static void ThenTheOutputFormContainsFullName(string desiredInfo)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        var FullNameOutput = formPage.GetUserNameOutput();
        StringAssert.Equals(FullNameOutput, desiredInfo);
    }

    [Then(@"the output form Email is '([^']*)'")]
    public static void ThenTheOutputFormContainsEMail(string desiredInfo)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        var EMailOutput = formPage.GetEmailOutput();
        StringAssert.Equals(EMailOutput, desiredInfo);
    }

    [Then(@"the output form Current Address is '([^']*)'")]
    public static void ThenTheOutputFormContainsCurrentAddress(string desiredInfo)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        var CurrentAddressOutput = formPage.GetCurrentAddressOutput();
        StringAssert.Equals(CurrentAddressOutput, desiredInfo);
    }

    [Then(@"the output form Permanent Address is '([^']*)'")]
    public static void ThenTheOutputFormContainsPermanentAddress(string desiredInfo)
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        var PermanentAddressOutput = formPage.GetPermanentAddressOutput();
        StringAssert.Equals(PermanentAddressOutput, desiredInfo);
    }

    [Then(@"format error in email is detected")]
    public static void ThenFormatErrorInEmailIsDetected()
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        Assert.IsTrue(formPage.CheckIfEmailTextBoxError());
    }

    [Then(@"no output information is appearing in the screen")]
    public void ThenNoOutputIsAppearingInScreen()
    {
        var formPage = ContainerManager.GetTestWareComponent<IFormPage>();
        Assert.IsFalse(formPage.CheckIfOutputArea());        
    }


}

