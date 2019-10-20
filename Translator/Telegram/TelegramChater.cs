//using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Translator.Models.Context;
using System.IO;
using Telegram.Bot.Types.Enums;
using Translator.Models.Data;

using Microsoft.EntityFrameworkCore;

namespace Translator.Telegram
{
    public class TelegramChater
    {
        private readonly GlossaryContext db;
        public TelegramChater(GlossaryContext context)
        {
            db = context;
        }

        public TelegramBotClient client = new TelegramBotClient(Configurations.ApiKEY);
        
        public void RunChat()
        {
            client.OnMessage += UserSendMessage;
            client.StartReceiving();
            
        }
        public async void UserSendMessage( object sender, MessageEventArgs e)
        {
            var UserId = e.Message.Chat.Id;
            var MessageId = e.Message.MessageId;
            var RusStr = e.Message.Text;
            string EngStr;
            if (e.Message.Type == MessageType.Text)
            {
                using (GlossaryContext db = new GlossaryContext())
                {
                    Glossary gl = db.glossary.FirstOrDefault(x => x.Russian == RusStr);
                    if (gl != null) { 
                    
                        EngStr = gl.English;
                        await client.SendTextMessageAsync(UserId, EngStr, replyToMessageId: MessageId);
                    }
                    else
                    {
                        var Translated = Translate.TranslateText(RusStr);
                        await client.SendTextMessageAsync(UserId, Translated.ToString(), replyToMessageId: MessageId);
                        if (Translated != Configurations.MessageError)
                        {
                            using (GlossaryContext glcontext = new GlossaryContext())
                            {
                                Glossary glossary = new Glossary();
                                glossary.Russian = e.Message.Text;
                                glossary.English = Translated.ToString();
                                
                                glcontext.Add(glossary);
                                glcontext.SaveChanges();
                                
                            }
                        }
                        
                    }
                }
            }
            else
            {
                await client.SendTextMessageAsync(UserId, "Ввод некоректный. Попробуйте текст.", replyToMessageId: MessageId);
            }
        }

    }
}
