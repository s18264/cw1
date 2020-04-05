using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if(args.Length == 0)
            {
                throw new ArgumentNullException("nie podano argumentu");
            }
            
            Uri uriResult;
            bool result = Uri.TryCreate(args[0], UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if(result == false)
            {
                throw new ArgumentException("niepoprawny url");
            }


            var httpClient = new HttpClient();
            //Console.WriteLine("podaj input");
            //var input = Console.ReadLine();

            var response = await httpClient.GetAsync(args[0]);

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-zA-Z0-9]+@[a-zA-Z.]+");

                MatchCollection matches = regex.Matches(html);
                if (matches.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono adresów email");
                }
                else { 
                foreach(var i in matches)
                {
                    Console.WriteLine(i);
                }
                }
            }


            httpClient.Dispose();
            Console.WriteLine("Koniec!");
        }
    }
}
