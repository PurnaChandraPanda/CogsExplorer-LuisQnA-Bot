using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogsExplorer_LuisQnA_Bot.Dialogs
{
    [Serializable]
    public class CogsLuisDialog: LuisDialog<object>
    {
        public CogsLuisDialog() : base(new LuisService(new LuisModelAttribute(
           ConfigurationManager.AppSettings["LuisAppId"],
           ConfigurationManager.AppSettings["LuisAPIKey"],
           domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("SearchAuthors")]
        public async Task SearchAuthorsIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("SearchCharacters")]
        public async Task SearchCharactersIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("SearchHouses")]
        public async Task SearchHousesIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("SearchSigils")]
        public async Task SearchSigilsIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            var response = string.Empty;
            var resultEntity = (result.Entities.Count != 0) ? result.Entities[0].Entity : "Not in list";
            response = $"Query: {result.Query} -> Intent: {result.Intents[0].Intent} -> Entity: {resultEntity}";

            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }
    }
}