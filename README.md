# CogsExplorer-LuisQnA-Bot
This C# sample application demonstrates integration of LUIS and QnA Maker services in BOT app.

<b>Set up your LUIS app as:</b>
<ol>
<li>Login to <a href="https://luis.ai/">https://luis.ai/</a></li>
<li>Import new app</li>
<li>Select the <b>LUIS_CogsExplorer.json</b></li>
<li>Publish</li>
</ol>

<b>Set up your QnAMaker app as:</b>
<ol>
<li>Login to <a href="https://qnamaker.ai/">https://qnamaker.ai/</a></li>
<li>Name it as Cogs Explorer</li>
<li>Create it</li>
<li>Replace knowledge base with <b>QnAMaker_CogsExplorer.tsv</b></li>
<li>Publish</li>
</ol>

Next step is to update web.config or application configuration with appropriate configuration file.

This application makes use of nuget package <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder.CognitiveServices">Microsoft.Bot.Builder.CognitiveServices</a>.

<pre>
                builder.RegisterModule(new QnAMakerModule(
                        ConfigurationManager.AppSettings["QnASubscriptionKey"],
                        ConfigurationManager.AppSettings["QnAKnowledgebaseId"],
                        "I don't understand this right now! Try another query!",
                        0.50));
</pre>

First, BOT would reach out to QnA Maker service. If answer is not satisfactory, it would go for LUIS app and fetch results. Again, a score check can also be applied, but I have not done in my sample app yet.

<pre>
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
        ..
        ..
    }
</pre>

<h2><b>Example queries</b></h2>
Possibly, the following queries can be hit (for QnA Maker):
1. Who was known as the Beggar King?
2. What are some of Daenerys Targaryen's nicknames?
3. Who is Lady Stoneheart?
4. Lady Stoneheart?
<br/>
And, the following queries can be hit (for LUIS):
1. What house does Arya belong to?
2. What house does Tyrion belong to?
3. What is the sigil of house greyjoy?
4. whose sigil displays stag?
