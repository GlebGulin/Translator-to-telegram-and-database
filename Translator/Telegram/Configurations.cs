using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Telegram
{
    public static class Configurations
    {
        public static string NameChanelOne { get; set; } = "@simpletranslator_bot";
        public static string ApiKEY { get; set; } = "some key";
        public static string MessageError { get; } = "Сервис временно недоступен. Попробуйте позже";

    }
}
