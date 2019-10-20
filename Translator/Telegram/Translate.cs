using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Translator.Telegram
{
    public static class Translate
    {
        
        public static string TranslateText(string inputMessage)
        {
            try
            {
                var client = TranslationClient.Create();
                var response = client.TranslateText(inputMessage, LanguageCodes.English);
                return response.TranslatedText.ToString();
            }
            catch
            {
                return String.Format(Configurations.MessageError);
            }
        }
       


    }
}
