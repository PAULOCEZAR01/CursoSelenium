using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CursoSeleinum.ST01Contato;

[TestFixture]
public class CT01ValidarLayoutTela
{
    private IWebDriver driver;
    private WebDriverWait webDriverWait;
    private StringBuilder verificationErrors;
    private bool acceptNextAlert = true;
    private readonly string baseURL = "https://livros.inoveteste.com.br/";

    [SetUp]
    public void SetupTest()
    {
        driver = new FirefoxDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(baseURL);

        webDriverWait = new(driver, TimeSpan.FromSeconds(15));

        verificationErrors = new StringBuilder();
    }

    [TearDown]
    public void TeardownTest()
    {
        try
        {
            driver.Quit();  
        }
        catch (Exception)   
        {
            // Ignore errors if unable to close the browser
        }
        Assert.That(verificationErrors.ToString(), Is.EqualTo(string.Empty));
    }

    [Test]
    public void TheCT01ValidarLayoutTelaTest()
    {
        // Acessa o menu Contato
        /*
        driver.FindElement(By.CssSelector("em.fa.fa-bars")).Click();
        driver.FindElement(By.CssSelector("div.sidr-inner > #nav-wrap > #primary_menu > #menu-item-80 > a > span")).Click();
        */

        webDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".lp-screen")));
        driver.FindElement(By.CssSelector("#menu-item-80 a")).Click();

        //// Valida o layout da tela
        Assert.That(driver.FindElement(By.CssSelector("h1")).Text, Is.EqualTo("Envie uma mensagem"));
        Assert.IsTrue(IsElementPresent(By.Name("your-name")));
        Assert.IsTrue(IsElementPresent(By.Name("your-email")));
        Assert.IsTrue(IsElementPresent(By.Name("your-subject")));
        Assert.IsTrue(IsElementPresent(By.Name("your-message")));
        Assert.IsTrue(IsElementPresent(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")));
    }
    private bool IsElementPresent(By by)
    {
        try
        {
            driver.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    private bool IsAlertPresent()
    {
        try
        {
            driver.SwitchTo().Alert();
            return true;
        }
        catch (NoAlertPresentException)
        {
            return false;
        }
    }

    private string CloseAlertAndGetItsText()
    {
        try
        {
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            if (acceptNextAlert)
            {
                alert.Accept();
            }
            else
            {
                alert.Dismiss();
            }
            return alertText;
        }
        finally
        {
            acceptNextAlert = true;
        }
    }
}
