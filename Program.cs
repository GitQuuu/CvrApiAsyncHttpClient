using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
// WebClient is no longer recommended for new development instead MS recommend System.Net.Http.HttpClient Class 
// This is asynchronous approach with HttpClient
namespace CvrApiAsyncHttpClient
{
    // To change between Denmark and Norwegian
    enum Country
    {
        dk,no
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var userAgent = " Scandinavian Software Solutions - CVRAPI - Qu Kops Le +45 13371337";    
            var cvrApiResponse = await CvrService.GetCompanyData("Scandinavian Software Solutions",userAgent,Country.dk);
        }
        
        public static class CvrService
        {
            // HttpClient is intended to be instantiated once and re-used throughout the life of an application.
            // Instantiating an HttpClient class for every request will exhaust the number of sockets available under heavy loads.
            // This will result in SocketException errors. Below is an example using HttpClient correctly according to MSDOCS .NET5
            
            private static readonly HttpClient HttpClient;
            static CvrService()
            {
                HttpClient = new HttpClient();
            }
            
            /// <summary>
            /// Fetch data from CVRAPI.dk
            /// </summary>
            /// <param name="vatOrName">CVR or Company name</param>
            /// <param name="userAgent">Your CompanyName, ProjektName and ContactInfo</param>
            /// Be aware that userAgent cannot contain "@ Æ Ø" may be a bug on their end 
            /// <param name="country">Enum to switch between Denmark and Norway</param>
            /// <returns>Task string</returns>
            public static async Task<Company> GetCompanyData(string vatOrName, string userAgent, Country country)
            {
                HttpClient.DefaultRequestHeaders.Add("User-Agent",userAgent);
                var resultContent = HttpClient.GetStringAsync(string.Format($"http://cvrapi.dk/api?search={vatOrName}&country={country}"));
                Company res = JsonConvert.DeserializeObject<Company>(await resultContent);
                    
                return res;
            }
        }
    
        public class Company
        {
            public string VAT { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Zipcode { get; set; }
            public string City { get; set; }
            public bool @protected { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Fax { get; set; }
            public string Startdate { get; set; }
            public string Enddate { get; set; }
            public string Employees { get; set; }
            public string Addressco { get; set; }
            public int Industrycode { get; set; }
            public string Industrydesc { get; set; }
            public int Companycode { get; set; }
            public string Companydesc { get; set; }
            public string Creditstartdate { get; set; }
            public int? Creditstatus { get; set; }
            public bool Creditbankrupt { get; set; }
            public ApipPoductionunits[] Productionunits { get; set; }
            public int T { get; set; }
            public int Version { get; set; } 
        }
        public class ApipPoductionunits
        {
            public string Pno { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Zipcode { get; set; }
            public string City { get; set; }
            public bool @protected { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Fax { get; set; }
            public string Startdate { get; set; }
            public string Enddate { get; set; }
            public string Employees { get; set; }
            public string Addressco { get; set; }
            public int Industrycode { get; set; }
            public string Industrydesc { get; set; }
            public int Companycode { get; set; }
            public string Companydesc { get; set; }
            public string Creditstartdate { get; set; }
            public int? Creditstatus { get; set; }
            public bool Creditbankrupt { get; set; }
            public bool MadeByQuKopsLe { get; set; }
        }
     
    }
}