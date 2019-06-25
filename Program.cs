using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProjectCreator
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            Console.Write(">create ");
            string Prompt = Console.ReadLine();
            Console.Write(">Project Name ");
            string ProjName = Console.ReadLine();
            

            string path = @"c:\" + ProjName;

            switch (Prompt)
            {
                case "vscode github":
                    CreateFile(ProjName);
                    Console.Write(">Github E-Mail ");
                    string gitMail = Console.ReadLine();
                    Console.Write(">Github Passwort ");
                    string gitPassword = Console.ReadLine();
                    OpenGit(gitMail, gitPassword, ProjName);
                    OpenVsc(path);
                    break;
                case "vscode":
                    CreateFile(ProjName);
                    OpenVsc(path);
                    break;
                case "vsSln github":
                    CreateFile(ProjName);
                    Console.Write(">Github E-Mail ");
                    gitMail = Console.ReadLine();
                    Console.Write(">Github Passwort ");
                    gitPassword = Console.ReadLine();
                    OpenGit(gitMail, gitPassword, ProjName);
                    OpenVsSln(path);
                    break;
                case "vsSln":
                    CreateFile(ProjName);
                    OpenVsSln(path);
                    break;

                case "github":
                    CreateFile(ProjName);
                    Console.Write(">Github E-Mail ");
                    gitMail = Console.ReadLine();
                    Console.Write(">Github Passwort ");
                    gitPassword = Console.ReadLine();
                    OpenGit(gitMail, gitPassword, ProjName);
                    break;

                default:
                    MessageBox.Show("Ungültiger Input | Gebe einen der Commands ein. | Documentation: https://www.projectesel.tk/", "Input Error");
                    break;
            }

            

            Console.Read();
        }

        public static void CreateFile(string ProjName)
        {
            string path = @"c:\" + ProjName;
            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("Eine Datei mit dem selben Namen existiert bereits");
                    System.Threading.Thread.Sleep(3000);
                    Environment.Exit(0);
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                Directory.SetCurrentDirectory(path);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Error: " + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Es gab einen Fehler: " + e);
            }

            string filepath = path + "\\ReadMe.txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(ProjName + ".readMe");
                    sw.WriteLine("Erstellt durch ProjecteSeL.CreateMe");
                }
            }
        }

        public static void OpenGit(string GitMail, string GitPassword, string ProjName)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://github.com/login");
                IWebElement query = driver.FindElement(By.Id("login_field"));
                query.SendKeys(GitMail);
                query = driver.FindElement(By.Id("password"));
                query.SendKeys(GitPassword);
                query.Submit();
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.LinkText("New")).Click();
                System.Threading.Thread.Sleep(1000);
                query = driver.FindElement(By.Id("repository_name"));
                query.SendKeys(ProjName);
                driver.FindElement(By.Id("repository_auto_init")).Click();
                query.Submit();
            } catch(Exception e)
            {
                Console.WriteLine("\n\nError: " + e);
            }
        }

        public static void OpenVsc(string folderpath)
        {
            try
            {
                Process.Start("D:\\Microsoft VS Code\\Code.exe", folderpath);
            } catch(Exception e)
            {
                Console.WriteLine("\n\nError: " + e);
            }
        }

        public static void OpenVsSln(string folderpath)
        {
            try
            {
                Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.exe", folderpath);
            } catch(Exception e)
            {
                Console.WriteLine("\n\nError: " + e);
            }
        }
    }
}
