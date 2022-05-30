using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FinalExam_CPM
{
    internal class Class1
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://candidatex:qa-is-cool@qa-task.backbasecloud.com";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void SignInMethod()
        {
            string Email = "bronnie@email.com";
            string Password = "Password1";
            driver.FindElement(By.CssSelector("a[href*='/login']")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='email']")).SendKeys(Email);
            driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys(Password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [Test, Order(1), Category("Smoke Test")]
        public void Logout()
        {
            SignInMethod();
            driver.FindElement(By.CssSelector("a[href*='/settings']")).Click();
            driver.FindElement(By.XPath("//button[@class='btn btn-outline-danger']")).Click();
            Assert.IsTrue(driver.FindElements(By.XPath("//a[contains(text(),'Sign up')]")).Count >= 1);
        }
        
        [Test, Order(0), Category("Smoke Test")]
        public void CreatePost()
        {
            string ArticleTitle = "New Article";
            string ArticleDescription = "Description of new Article";
            string ArticleBody = "Creating a new article test";
            string ArticleTag = "Cthulhu";
            SignInMethod();
            driver.FindElement(By.CssSelector("a[href*='/editor']")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='title']")).SendKeys(ArticleTitle);
            driver.FindElement(By.XPath("//input[@formcontrolname='description']")).SendKeys(ArticleDescription);
            driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).SendKeys(ArticleBody);
            driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys(ArticleTag);
            driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys(Keys.Enter);
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            IWebElement postcreation = driver.FindElement(By.XPath("//a[@class='author']"));
            Assert.AreEqual(postcreation.Text, "bronnieapony");
            Console.Write(postcreation.Text);
        }
        
        [Test,Order(3), Category("Smoke Test")]
        public void AddaFavoriteArticle()
        {
            SignInMethod();
            driver.FindElement(By.XPath("//a[contains(text(),'Aragog')]")).Click();
            driver.FindElement(By.XPath("//button[@class='btn btn-sm btn-outline-primary']")).Click();
            IWebElement like = driver.FindElement(By.XPath("//button[@class='btn btn-sm btn-primary']"));
            Assert.AreEqual(like.Text, "1");
            Console.Write(like.Text);
        }

        [Test, Order(4), Category("Smoke Test")]
        public void CreateComment()
        {
            string CommentText = "Check it out https://agilethought.com/careers/";
            SignInMethod();
            driver.FindElement(By.XPath("//a[contains(text(),'pixel')]")).Click();
            driver.FindElement(By.CssSelector("a[href*='/article/dolorem-in-saepe-illum-esse-perspiciatis-accusamus-voluptas-et-a74i6g']")).Click();
            driver.FindElement(By.XPath("//textarea[@placeholder='Write a comment...']")).SendKeys(CommentText);
            driver.FindElement(By.XPath("//button[contains(text(),'Post Comment')]")).Click();
            IWebElement newcomment = driver.FindElement(By.XPath("//p[@class='card-text']"));
            Assert.AreEqual(newcomment.Text, "Check it out https://agilethought.com/careers/");
            Console.Write(newcomment.Text);
        }

        [Test, Order(2), Category("Smoke Test")]
        public void UpdatePost()
        {
            String UpdateId = DateTime.Now.ToString();
            String UpdateTotal = "Test this article can be updated " + UpdateId;
            SignInMethod();
            driver.FindElement(By.CssSelector("a[href*='/profile/bronnieapony']")).Click();
            driver.FindElement(By.CssSelector("a[href*='/article/updatable-article-9b1e5a']")).Click();
            driver.FindElement(By.XPath("//a[@class='btn btn-sm btn-outline-secondary']")).Click();
            driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).Clear();
            driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).SendKeys(UpdateTotal);
            driver.FindElement(By.XPath("//button[@class='btn btn-lg pull-xs-right btn-primary']")).Click();
            IWebElement updatecomment = driver.FindElement(By.XPath("//p[contains(text(),UpdateTotal)]"));
            Assert.AreEqual(updatecomment.Text, "Test this article can be updated " + UpdateId);
            Console.Write(updatecomment.Text);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();

        }
    }
}
