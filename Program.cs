﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BrotherBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}