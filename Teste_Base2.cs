using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace MeuProjetoSelenium
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://mantis-prova.base2.com.br/login_page.php";

            // Inserir usuário
            driver.FindElement(By.Id("username")).SendKeys("raizasoares");
            driver.FindElement(By.XPath("//input[@value='Entrar']")).Click();

            // Inserir senha
            driver.FindElement(By.Id("password")).SendKeys("Raiza@1404");
            driver.FindElement(By.XPath("//input[@value='Entrar']")).Click();

            // Verificar se está na Home
            string pageTitle = driver.Title;
            string expectedTitle = "Minha Visão - MantisBT";
            Assert.IsTrue(pageTitle.Contains(expectedTitle), "O título da página não corresponde ao título esperado");
        }

        [Test]
        public void TestCriarTarefa()
        {
            // Criar Tarefa
            driver.FindElement(By.LinkText("Criar Tarefa")).Click();
            driver.FindElement(By.XPath("//input[@value='Selecionar Projeto']")).Click();
            driver.FindElement(By.Id("summary")).SendKeys("Teste Raiza");
            driver.FindElement(By.Id("description")).SendKeys("Realizando testes - Raiza Oliveira Soares");
            driver.FindElement(By.XPath("//input[@value='Criar Nova Tarefa']")).Click();

            // Verificar criação de tarefa
            int numeroUltimaTarefa = CapturarNumeroUltimaTarefa();
            Assert.Greater(numeroUltimaTarefa, 0, "Número da última tarefa não foi capturado corretamente ou é menor ou igual a zero");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        private int CapturarNumeroUltimaTarefa()
        {
           
            IWebElement numeroUltimaTarefaElement = driver.FindElement(By.CssSelector("td.bug-id"));
            string numeroUltimaTarefaTexto = numeroUltimaTarefaElement.Text;
            int numeroUltimaTarefa = int.Parse(numeroUltimaTarefaTexto);

            return numeroUltimaTarefa;
        }
    }
}
