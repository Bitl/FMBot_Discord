using Discord;
using Discord.Commands;
using FMBot.Bot.Resources;
using FMBot.Domain;
using FMBot.LastFM.Domain.Enums;
using IF.Lastfm.Core.Api.Enums;

namespace FMBot.Bot.Services
{
    public static class ErrorService
    {
        public static void UsernameNotSetErrorResponse(this EmbedBuilder embed, string prfx)
        {
            embed.WithTitle("Error while attempting get Last.FM information");
            embed.WithDescription("Your Last.FM username has not been set. \n" +
                                        $"Please use the `{prfx}set` command to connect your Last.FM account to .fmbot. \n" +
                                        $"Example: `{prfx}set lastfmusername`\n \n" +
                                        $"For more info, use `{prfx}set help`.");

            embed.WithUrl($"{Constants.DocsUrl}/commands/");

            embed.WithColor(DiscordConstants.WarningColorOrange);
        }

        public static void SessionRequiredResponse(this EmbedBuilder embed, string prfx)
        {
            embed.WithTitle("Error while attempting get Last.FM information");
            embed.WithDescription("You have not authorized .fmbot yet. \n" +
                                $"Please use the `{prfx}login` command to authorize .fmbot.");

            embed.WithUrl($"{Constants.DocsUrl}/commands/");

            embed.WithColor(DiscordConstants.WarningColorOrange);
        }

        public static void NoScrobblesFoundErrorResponse(this EmbedBuilder embed, LastResponseStatus apiResponse, string prfx)
        {
            embed.WithTitle("Error while attempting get Last.FM information");
            switch (apiResponse)
            {
                case LastResponseStatus.Failure:
                    embed.WithDescription("Can't retrieve scrobbles because Last.FM is having issues. Please try again later. \n" +
                                          "Please note that .fmbot isn't affiliated with Last.FM.");
                    break;
                case LastResponseStatus.MissingParameters:
                    embed.WithDescription("You or the user you're searching for has no scrobbles/artists on their profile, or Last.FM is having issues. Please try again later. \n \n" +
                                          $"Recently changed your Last.FM username? Please change it here too using `{prfx}set`. \n" +
                                          $"For more info on your settings, use `{prfx}set help`.");
                    break;
                default:
                    embed.WithDescription(
                        "You or the user you're searching for has no scrobbles/artists on their profile, or Last.FM is having issues. Please try again later.");
                    break;
            }

            embed.WithThumbnailUrl("https://www.last.fm/static/images/marvin.e51495403de9.png");
            embed.WithColor(DiscordConstants.WarningColorOrange);
        }

        public static void ErrorResponse(this EmbedBuilder embed, ResponseStatus apiResponse, string message, ICommandContext context, Logger.Logger logger)
        {
            embed.WithTitle("Error while attempting get Last.FM information");
            switch (apiResponse)
            {
                case ResponseStatus.Failure:
                    embed.WithDescription("Can't retrieve data because Last.FM is having issues. Please try again later. \n" +
                                          "Please note that .fmbot isn't affiliated with Last.FM.");
                    break;
                default:
                    embed.WithDescription(
                        message);
                    break;
            }

            embed.WithColor(DiscordConstants.WarningColorOrange);
            logger.LogError($"Last.fm returned error: {message}, error code {apiResponse}", context.Message.Content, context.User.Username, context.Guild?.Name, context.Guild?.Id);
        }
    }
}
