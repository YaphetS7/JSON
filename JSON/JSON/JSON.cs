using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Helpers;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
  
    public class Track
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            WebRequest reqGET = WebRequest.Create(@"https://jsonplaceholder.typicode.com/todos");
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
            s = @"{ 'responseData': { 'results':" + s + @"} }";
            JObject obj = JObject.Parse(s);
            List<JToken> JSONResponse = obj["responseData"]["results"].Children().ToList();
            List<Track> list = new List<Track>();
            Dictionary<int, int> luck = new Dictionary<int, int>();
            foreach(JToken k in JSONResponse)
            {
                Track track = k.ToObject<Track>();
                list.Add(track);
            }
            foreach(Track par in list)
            {
                if (!dict.ContainsKey(par.userId))
                {
                    luck.Add(par.userId, 0);
                    dict.Add(par.userId, 1);
                    if (par.completed)
                        luck[par.userId]++;
                }
                else
                {
                    dict[par.userId]++;
                    if (par.completed)
                        luck[par.userId]++;
                }
            }
            ICollection<int> keys = dict.Keys;
            Console.WriteLine("Кол-во уникальных пользователей = " + dict.Count);
            foreach (int q in keys)
                Console.WriteLine("ID пользователя - " + q + " Кол-во задач пользователя = " + dict[q] + " Кол-во выполненных задач пользователя = " + luck[q]);
            Console.ReadLine();
           





        }
    }
}
