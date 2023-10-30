using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Text.RegularExpressions;

namespace fixTwitterEmbedsBotCS;

public class MessageHandler {

    // Delay to allow embed to load in messages
    private const int embedLoadDelay = 1000;
    
    // Tweet URL regex string
    private const string tweetUrlRegexString = @"http(s)?:\/\/(www.)?((twitter)|(x)).com\/(@)?[A-Za-z0-9_-]+\/status\/[0-9]+((\?)([st]=[A-Za-z0-9_-]+&?)+)?";
    
    // Twitter Main Site URL regex string
    private const string twitterUrlRegexString = @"http(s)?:\/\/(www.)?((twitter)|(x)).com\/";
    
    // Fix Tweet Service URL (trailing slash required)

    // Uncomment this to use fxtwitter
    private const string fixTweetServiceUrl = "https://fxtwitter.com/";

    // Uncomment this to use vxtwitter
    // private const string fixTweetServiceUrl = "https://vxtwitter.com/";
    

    public static async Task messageCreateEventHandler(DiscordClient client, MessageCreateEventArgs eventArgs) {
        

        // await Task.Delay(embedLoadDelay); // wait for embed to load
        DiscordMessage message = eventArgs.Message;
        // Console.WriteLine(message.ToString()); // for debug only
        
        var tweetUrlRegex = new Regex(tweetUrlRegexString);
        var matches = tweetUrlRegex.Matches(message.Content);

        if (matches.Count == 0) {
            return;
        }

        var twitterUrlRegex = new Regex(twitterUrlRegexString);

        List<string> replyUrls = new List<string>();

        foreach (var item in matches) {
            replyUrls.Add(twitterUrlRegex.Replace(item.ToString(), fixTweetServiceUrl));
        }

        string finalMessageText = string.Join('\n', replyUrls);

        await eventArgs.Channel.SendMessageAsync(finalMessageText);
    }
}