using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;


namespace ZapBot
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;

        public Form1()

        {
            InitializeComponent();
            driver = new EdgeDriver();
            openPage();

        }

        private void openPage()
        {
            driver.Url = "https://web.whatsapp.com/";
            Thread.Sleep(150);
            
        }

        private void openChat(string contactName)
        {
            while (true)
            {
                try
                {
                    var newChatBox = driver.FindElement(By.CssSelector($"span[data-testid='chat']"));
                    newChatBox.Click();
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(50);
                    throw new Exception(ex.Message);
                }
            }
            while (true)
            {
                try
                {
                    var searchBar = driver.FindElement(By.CssSelector($"div[data-testid='chat-list-search']"));
                    searchBar.SendKeys(contactName);
                    Thread.Sleep(70);
                    searchBar.SendKeys(OpenQA.Selenium.Keys.Enter);
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(50);
                    throw new Exception(ex.Message);
                }
            }

        }

        private void sendMessage(string message)
        {   
            while (true)
            {
                try
                    {
                        IWebElement textBar = driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"));
                        textBar.SendKeys(message);

                        IWebElement sendButton = driver.FindElement(By.CssSelector("span[data-testid='send']"));
                        sendButton.Click();
                        break;
                    }
                catch (Exception ex)
                {
                    Thread.Sleep(50);
                }

            }

        }

        private void checksIfPageHasLoaded()
        {
            try
            {
                // Seleciona a barra de pesquisa. 
                driver.FindElement(By.XPath("//*[@id=\"side\"]/div[1]/div/div/div[2]/div/div[1]/p"));
                
            }
            catch (Exception)
            {
                try
                {
                    // Seleciona o botão de nova mensagem.
                    driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div/div[4]/header/div[2]/div/span/div[3]/div/span")).Click();
                }
                catch (Exception)
                {
                    driver.Navigate().Refresh();
                    throw new Exception("A página não foi carregada corretamente. Tentando recarregar a página...");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checksIfPageHasLoaded();
            string message = textBoxMensagem.Text;
            string contact = textBoxContato.Text;

            
            openChat(contact);
            
            sendMessage(message);
            
        }
    }
}
