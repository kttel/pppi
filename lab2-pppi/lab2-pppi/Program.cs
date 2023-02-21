using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using IniFile;
using Pastel;
using SlugGenerator;
using StringMath;
using YoutubeExplode;
using YoutubeExplode.Common;

namespace lab2_pppi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // First library
            var iniStr = File.ReadAllText("D:\\testdata.ini");
            Ini ini = Ini.Load(iniStr);
            Console.WriteLine("\tFILE CONTENT:\n" + ini);

            var newIni = new Ini {
                new Section("Monday") {
                    new Property("LecturesAmount", 3),
                    new Property("DayOfWeek", 1),
                    new Property("Mood", "Not that bad")
                }
            };
            int sectionCount = newIni.Count;
            Console.WriteLine("Current section count:" + sectionCount);

            var section = new Section("New section");
            newIni.Add(section);
            Console.WriteLine("New section count:" + newIni.Count);

            section.AddComment("This is a comment");
            section.AddBlankLine();

            var section2 = new Section("SectionName", null, "This is a comment surrounded by blank lines", null);
            newIni.Add(section2);

            newIni.SaveTo("D:\\testdata2.ini");
            Console.WriteLine("\tNEW INI FILE:\n" + newIni);

            // Second library
            MathExpr.AddVariable("PI", 3.1415926535897931);
            double result = "1 + {PI}".Eval();
            Console.WriteLine("A + PI = " + result);

            MathExpr.AddOperator("max", (a, b) => a > b ? a : b, Precedence.Power);
            result = new MathExpr("2 * 3 max 4").Result;
            Console.WriteLine("2 * 3 max 4 = " + result);

            var expr = "{a} + {b} + {PI}".ToMathExpr();
            var variables = expr.Variables;
            var localVariables = expr.LocalVariables;
            Console.WriteLine("Variables: ", variables);
            Console.WriteLine("Local variables: ", variables);

            expr = "{PI} + 1";
            expr.SetOperator("+", (a, b) => Math.Pow(a, b));
            expr = "3 + 2".ToMathExpr(expr.Context);
            result = "1 + 2 + 3".Eval(expr.Context);
            Console.WriteLine("Result = " + result);

            // Third library
            Console.WriteLine("\n==========================================".Pastel(Color.FromArgb(165, 229, 250)));
            var spectrum = new (string color, string letter)[] {
                ("#124542", "1"),
                ("#185C58", "2"),
                ("#1E736E", "3"),
                ("#248A84", "4"),
                ("#20B2AA", "5"),
                ("#3FBDB6", "6"),
                ("#5EC8C2", "7"),
                ("#7DD3CE", "8"),
                ("#9CDEDA", "9"),
                ("#BBE9E6", "0")
            };

            Console.WriteLine(string.Join("", spectrum.Select(s => s.letter.Pastel(s.color))));
            Console.WriteLine("\tSimple text with yellow background color\t".Pastel(Color.Black).PastelBg("FFD000"));

            // Fourth library
            var youtube = new YoutubeClient();
            String url = "https://www.youtube.com/watch?v=_Q9UEDvG5PU";
            var video = await youtube.Videos.GetAsync(url);

            String title = video.Title;
            String author = video.Author.ChannelTitle;
            var duration = video.Duration;
            Console.WriteLine($"\n\tYT video information:\nTitle: {title}\nAuthor: {author}\nDuration: {duration}");

            String playlistUrl = "https://www.youtube.com/playlist?list=PLo7TNe_pEoMXcsc1dOaFyVGcZK8o-6CCW";
            var playlist = await youtube.Playlists.GetAsync(playlistUrl);
            Console.WriteLine("\nPlaylist title: " + playlist.Title);
            Console.WriteLine("\nPlaylist author: " + playlist.Author);

            var videos = await youtube.Playlists.GetVideosAsync(playlistUrl).CollectAsync(10);
            Console.WriteLine("\n\tPlaylist Videos ");
            foreach (var videoOfPlaylist in videos)
            {
                Console.WriteLine("Title: " + videoOfPlaylist.Title);
                Console.WriteLine("Duration: " + videoOfPlaylist.Duration);
            }

            String channelUrl = "https://www.youtube.com/channel/UCgvlITfNYjZPkw7cz4yvMBg";
            var channel = await youtube.Channels.GetAsync(channelUrl);
            Console.WriteLine("\nChannel title: " + channel.Title);
            Console.WriteLine("Channel id: " + channel.Id);

            // Fifth library
            var itemsList = new List<ConcreteSlug>
            {
                new ConcreteSlug
                {
                    Slug = "test"
                },
                new ConcreteSlug
                {
                    Slug = "test2"
                }
            };
            var uniqueSlug = "test".GenerateUniqueSlug(itemsList);
            Console.WriteLine("\n\tSLUGS:\n" + uniqueSlug);

            var underscores = "my test text".GenerateSlug("_");
            var withoutUnderscores = "my test text".GenerateSlug();
            Console.WriteLine(underscores);
            Console.WriteLine(withoutUnderscores);

            var ukrainian_slug = "ЧНУ Петра Могили".GenerateSlug();
            Console.WriteLine(ukrainian_slug);

            Console.ReadKey();
        }
        public class ConcreteSlug : ISlug
        {
            public string Slug { get; set; }
        }
    }
}
