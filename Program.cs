using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using SeleniumExtras.WaitHelpers;

namespace WebScreenshot
{
    internal class Program
    {

        public static string dir = GetSetting("dir").ToString() + DateTime.Now.ToString("dd" + " MMMM" + " yyyy");
        public static string W = GetSetting("W").ToString();
        public static string H = GetSetting("H").ToString();
        public static string nama = "";
        public static string link = "";
        public static string Y;

        static void Main(String[] args)
        {
            Console.Title = "Web Auto Screenshot By Zhafif  " + DateTime.UtcNow.ToString() + " UTC+ ";
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
            
            LOOPING(Y);
            Console.WriteLine(Y);
        }
        private static void LOOPING(string Y)
        {
            Console.WriteLine("Ketik Y untuk Screenshot Web Costume");
            Console.WriteLine("Ketik W untuk Screenshot Web "+GetSetting("judul"));
            Console.WriteLine("Ketik N Atau C Atau E untuk Keluar");
            Y = Console.ReadLine();

            if (Y == "y" || Y == "Y")
            {
                CaptureCosutmeLink();
            }
            else if (Y == "w" || Y == "W")
            {
                CaptureWebPage();
            }
            else if (Y == "n" || Y == "N" || Y == "c" || Y == "C" || Y == "e" || Y == "E")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Tolong Ketik Pilihan Yang Tersedia Saja");
                LOOPING(Y);
            }
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
            options.AddArgument("--window-size="+ W +"," + H);
            options.AddArgument("test-type=browser");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArguments("--disable-popup-blocking");
            options.AddArgument("--headless");


            IWebDriver driver = new ChromeDriver(options);

            Console.WriteLine("Sedang Menghidupkan Bot Aplikasinya ...", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("Jangan Diclose Sampai Selesai Ya Kalo Mau Beneran Ke Save", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);

            Console.WriteLine("");
            Console.WriteLine("Memproses Tampilan Website " + nama, Console.ForegroundColor = ConsoleColor.White);
            driver.Navigate().GoToUrl(link);
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Thread.Sleep(10000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("img")));
            
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(dir + "\\" + nama + ".png");
            
            Console.WriteLine(nama + " Telah Berhasil di Save");
            driver.Close();

            LOOPING(Y);
        }

        private static void CaptureWebPage()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-gpu");
            options.AddArgument("test-type");
            options.AddArgument("start-maximized");
            options.AddArgument("--window-size=" + W + "," + H);
            options.AddArgument("test-type=browser");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArguments("--disable-popup-blocking");
            options.AddArgument("--headless");

          
            IWebDriver driver = new ChromeDriver(options);  

            Console.WriteLine("");
            Console.WriteLine("Sedang Menghidupkan Bot Aplikasinya ...", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("Jangan Diclose Sampai Selesai Ya Kalo Mau Beneran Ke Save", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);

            driver.Navigate().GoToUrl(GetSetting("link0"));

            try
            {
                for (int i = 0; i < int.Parse(GetSetting("jumlah")); i++)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Sedang Memproses Tampilan Halaman " + GetSetting(i.ToString()), Console.ForegroundColor = ConsoleColor.White);


                    driver.Navigate().GoToUrl(GetSetting("link" + i.ToString()));
                    ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                    wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("img")));
                    Thread.Sleep(10000);

                    Screenshot screenshot = screenshotDriver.GetScreenshot();
                    screenshot.SaveAsFile(dir + "\\" + GetSetting(i.ToString()) + ".png");

                    Console.WriteLine(GetSetting(i.ToString()) + " Telah Berhasil di Save");
                    Console.WriteLine("");
                }
            }
            catch (Exception e)
            {
                if(e.ToString() == "System.ArgumentNullException: Argument 'url'")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Screenshot Website "+ GetSetting("judul") + " Berhasil");
                    driver.Close();

                    LOOPING(Y);
                }
            }

            Console.WriteLine(GetSetting("Screenshot Website "+ GetSetting("judul") + " Berhasil"));
            driver.Close();

            LOOPING(Y);
        }
        
        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static string GetLinks(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }

}
