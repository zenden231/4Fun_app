using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;


/*
 * вытягивает рандомные фотки с сервера быстрых фотографий, иногда бывает что-то интересное
 * 
 */
namespace _4Fun_app
{
    class Program
    {
        static void Main()
        {
            NavigateResource();
        }     
        static string GenerateResource()
        {
            Random range = new Random();
            var str = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for(var i = 0; i < 6; i++)
            {
                str = str + chars[range.Next(chars.Length)]; 
            }
            return str.ToLower();
        }
        static void NavigateResource()
        {
            IWebDriver driver = new FirefoxDriver();
            //IWebDriver driver = new ChromeDriver();
            driver.Url = ("https://prnt.sc/" + GenerateResource());
            //Console.ReadLine();
            Thread.Sleep(2000);
            while (true)
            {
                IWebElement body = driver.FindElement(By.TagName("body"));
                body.Click();
                body.SendKeys(Keys.F11);
                ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                driver.SwitchTo().Window(windowHandles[1]);
                driver.Url = ("https://prnt.sc/" + GenerateResource());
                Console.Write("Press Enter ");
                Thread.Sleep(800);
                Console.ReadLine();
                driver.Close();
                driver.SwitchTo().Window(windowHandles[0]);
                Console.Write("Press q to exit or Enter to continue: ");
                if (Console.ReadLine() == "q")
                {
                    driver.Quit();
                    break;
                }
            }
        }
    }
}
