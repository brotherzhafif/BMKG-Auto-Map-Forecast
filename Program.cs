﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Threading;
using SeleniumExtras.WaitHelpers;
using System.IO;

namespace WebScreenshot
{ 
    internal class Program
    {

        public static string dir = DateTime.Now.ToString("dd"+" MM"+" yyyy");
        public static string nama = "";
        public static string link = "";

        static void Main(String[] args)
        {
            Console.Title = "BMKG - Web Auto Screenshot By Zhafif Sekarang Jam "+ DateTime.UtcNow.ToString() +" UTC+ ";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            if (Directory.Exists(dir))
            {
                Console.WriteLine("Folder Tempat Save Sudah Ada");
            }
            else
            {
                DirectoryInfo directory = Directory.CreateDirectory(dir);
                Console.WriteLine("Folder Tempat Save Dibuat");
            }

            Console.WriteLine("Jadi Anda Mau Ngescreenshot Web Apa Hari Ini ?");
            Console.WriteLine("");

            while (true)
            {
                

                //PIIHAN MENU

                Console.WriteLine("Ketik Y untuk Screenshot Web Costume");
                Console.WriteLine("Ketik W untuk Screenshot Web Windy.com Untuk Arsip BMKG");
                Console.WriteLine("Ketik N Atau C Atau E untuk Keluar");

                string Y = Console.ReadLine();

                if (Y == "y" || Y == "Y")
                {
                    CaptureCosutmeLink();
                    return;
                }
                else if (Y == "w" || Y == "W")
                {
                    CaptureWebPage();
                    return;
                }
                else if (Y == "a")
                {
                    return;
                }
                else if (Y == "n" || Y == "N" || Y == "c" || Y == "C" || Y == "e" || Y == "E")
                {
                    Environment.Exit(0);
                }
            }
        }
        

        private static string GetLinks(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
        private static void CaptureCosutmeLink()
        {
            Console.WriteLine("Anda Ingin Melakukan Screenshot Lainnya ? Silahkan Masukan Nama Filenya");

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            nama = Console.ReadLine();
            Console.WriteLine("Oke Namanya " + nama + ", Baiklah Sekarang Silahkan Kopas Linknya");
            link = Console.ReadLine();
            Console.WriteLine("Baiklah Tunggu Sebentar Sedang Membuka Websitenya ...");

            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-gpu");
            options.AddArgument("test-type");
            options.AddArgument("start-maximized");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("test-type=browser");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArguments("--disable-popup-blocking");
            options.AddArgument("--headless");
            options.AddArgument("user-agent-Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36");


            IWebDriver driver = new ChromeDriver(options);

            Console.WriteLine("Sedang Menghidupkan Bot Aplikasinya ...", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("Jangan Diclose Sampai Selesai Ya Kalo Mau Beneran Ke Save", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);

            Console.WriteLine("");
            Console.WriteLine("Memproses Tampilan Website " + nama, Console.ForegroundColor = ConsoleColor.White);
            driver.Navigate().GoToUrl(link);
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElements(By.TagName("div")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("img")));
            
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(dir + "\\" + nama + ".png");
            
            Console.WriteLine(nama + " Telah Berhasil di Save");

            return;
        }

        private static void CaptureWebPage()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-gpu");
            options.AddArgument("test-type");
            options.AddArgument("start-maximized");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("test-type=browser");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArguments("--disable-popup-blocking");
            options.AddArgument("--headless");
            options.AddArgument("user-agent-Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36");

          
            IWebDriver driver = new ChromeDriver(options);  

            string[] Links = new string[]
            {
                GetSetting("Baamang"),
                GetSetting("Antang Kalang"),
                GetSetting("Bukit Sentuai"),
                GetSetting("Cempaga"),
                GetSetting("Cempaga Hulu"),
                GetSetting("Kota Besi"),
                GetSetting("Mentawa Baru Ketapang"),
                GetSetting("Mentaya Hilir Selatan"),
                GetSetting("Mentaya Hilir Utara"),
                GetSetting("Mentaya Hulu"),
                GetSetting("Parenggean"),
                GetSetting("Pulau Hanaut"),
                GetSetting("Seranau"),
                GetSetting("Telaga Antang"),
                GetSetting("Telawang"),
                GetSetting("Teluk Sampit"),
                GetSetting("Tualan Hulu"),
                GetSetting("Kuala Pembuang"),
                GetSetting("Seruyan Hilir")
            };

            string[] Names = new string[]
            {
                "Baamang",
                "Antang Kalang",
                "Bukit Sentuai",
                "Cempaga",
                "Cempaga Hulu",
                "Kota Besi",
                "Mentawa Baru Ketapang",
                "Mentaya Hilir Selatan",
                "Mentaya Hilir Utara",
                "Mentaya Hulu",
                "Parenggean",
                "Pulau Hanaut",
                "Seranau",
                "Telaga Antang",
                "Telawang",
                "Teluk Sampit",
                "Tualan Hulu",
                "Kuala Pembuang",
                "Seruyan Hilir"
            };
 
            Console.WriteLine("");
            Console.WriteLine("Sedang Menghidupkan Bot Aplikasinya ...", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("Jangan Diclose Sampai Selesai Ya Kalo Mau Beneran Ke Save", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);

            driver.Navigate().GoToUrl(Links[0]);

            for (int i = 0; i < Links.Count(); i++)
            {
                Console.WriteLine("");
                Console.WriteLine("Sedang Memproses Tampilan Cuaca " + Names[i], Console.ForegroundColor = ConsoleColor.White);

                
                driver.Navigate().GoToUrl(Links[i]);
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(driver => driver.FindElement(By.Id("detail-data-table")));
                wait.Until(driver => driver.FindElement(By.Id("detail-box")));
                wait.Until(driver => driver.FindElements(By.TagName("td")));
                wait.Until(driver => driver.FindElements(By.TagName("div")));
                wait.Until(driver => driver.FindElements(By.TagName("img")));

                wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("img")));

                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(dir + "\\" + Names[i] + ".png");

                Console.WriteLine(Names[i] + " Telah Berhasil di Save");
                Console.WriteLine("");
            }

            Console.WriteLine("Screenshot Windy.com Berhasil");

            return;
        }
        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }

}