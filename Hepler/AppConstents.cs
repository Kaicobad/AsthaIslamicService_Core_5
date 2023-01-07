namespace AsthaIslamicService.Hepler
{
    public class AppConstents
    {
        public static string NOOR_API = "http://27.131.15.12:801";
        public static string Ibadat_API = "http://43.240.103.34/ebadattest";
        public static string MYGPHttps_API = "https://myrobi.noorsawab.com/api";



        public static string Auth = "Gakkmedia:GaramD@nC0k@";


        public static string IbadatAuthentication()
        {

            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Auth));
        }
    }
}
