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
            driver = new EdgeDriver("Path to Chrome Driver");
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
                catch (Exception ex){ Thread.Sleep(50); }
            }
            while (true)
            {
                try
                {
                    var searchBar = driver.FindElement(By.CssSelector($"div[data-testid='chat-list-search']"));
                    searchBar.SendKeys(contactName);
                    searchBar.SendKeys(OpenQA.Selenium.Keys.Enter);
                    break;
                }
                catch (Exception ex) { Thread.Sleep(50); }
             }

        }

        private void sendMessage(string message)
        {   
            while (true)
            {
                try
                    {

                        var textBar = driver.FindElement(By.ClassName(("p3_M1")));
                        textBar.SendKeys(message);
                        var sendButton = driver.FindElement(By.CssSelector("span[data-testid='send']"));
                        sendButton.Click();
                        break;
                    }
            catch (Exception ex) { Thread.Sleep(50); }

            }

        }



        private void button1_Click(object sender, EventArgs e)
        {

            string message = textBoxMensagem.Text;
            string contact = textBoxContato.Text;

            
            openChat(contact);
            sendMessage(message);
            
        }
    }
}
