﻿using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordCyberBot
{
    public class MyBot : ModuleBase
    {
        // define the bot's commands
        public async Task UserJoined(SocketGuildUser user)
        {
            var channel = await Context.Client.GetChannelAsync(352563644711829516) as SocketTextChannel;
            await channel.SendMessageAsync("Welcome to Valhalla " + user.Mention + "! Don't act like an idiot.");
        }

        [Command("hello")]
        [Summary("say hello")]
        [Alias("Hi")]
        public async Task RegisterHelloCommand()
        {
            await ReplyAsync("hello, " + Context.Message.Author.Mention + " would you like to see my tits?");
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
