using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordCyberBot
{
    public class MyBot : ModuleBase
    {
        // define the bot's commands
        [Command("hello")]
        [Summary("say hello")]
        [Alias("Hi")]
        public async Task RegisterHelloCommand()
        {
            await ReplyAsync("hello, " + Context.Message.Author.Mention);
        }


        [Command("purge")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [Summary("purge chat messages")]
        [Alias("clear")]
        public async Task RegisterPurgeCommand([Remainder] int num = 0)
        {
            if (num <= 100)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(num + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messagesToDelete);

                // let us know who deleted messaged and how many
                if (num == 1)
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username + " deleted 1 message.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username + " deleted " + num + " messages.");
                }
            }
            else
            {
                await ReplyAsync(Context.User.Username + ": you cannot purge more than a 100 messages!");
            }
        }
    }
}
