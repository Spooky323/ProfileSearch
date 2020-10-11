using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Reflection.Emit;

namespace ProfileSearch
{
    class Program
    {
        static Stack<Map> stack = new Stack<Map> { };
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter ScoreSaber URL : ");
            string url = Console.ReadLine();
            string userID = url.Split('/')[url.Split('/').Length - 1];
            Console.WriteLine("Please Enter a song's name : ");
            string search_name = Console.ReadLine();
            Console.WriteLine("Enter Max pages to search : ");
            int max_pages = int.Parse(Console.ReadLine());
            string endpoint = "https://new.scoresaber.com/api/player/" + userID + "/scores/top/";
            StackAndFilter(endpoint, userID, search_name, max_pages);
            print();
        }
        static JObject ApiCall(string endpoint)
        {
            WebRequest request = WebRequest.Create(endpoint);
            WebResponse response = request.GetResponse();
            Stream response_stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(response_stream);
            string data = reader.ReadToEnd();
            response.Close();
            response_stream.Close();
            reader.Close();
            return JObject.Parse(data);
        }
        static void StackAndFilter(string endpoint, string userID, string search_name, int max_pages )
        {
            int i = 1;
            string temp_name = "";
            Console.WriteLine($"Searching {search_name} in user {userID} ");
            while (i <= max_pages) // Looping each page of scores in the user's profile.
            {
                Console.WriteLine($"Fetching Songs from Page {i}");
                JObject tempdata = ApiCall(endpoint+i);
                for (int z = 0; z < 8; z++) // Looping each song in the Json array of songs.
                {
                    temp_name = (string)tempdata["scores"][z]["songName"];
                    if (temp_name.Contains(search_name))
                    {
                        Map tempmap = new Map(tempdata, z);
                        stack.Push(tempmap);
                    }
                }
                i++;
            }
        }
        static void print()
        {
            Map map;
            if (stack.Count == 0)
            {
                Console.WriteLine("Did not find any songs! ");
            }
            else
            {
                while(stack.Count != 0)
                {
                    map = stack.Pop();
                    map.Print();
                }
            }
        }
    }
}
