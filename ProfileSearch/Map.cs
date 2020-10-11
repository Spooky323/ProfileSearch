using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileSearch
{
    class Map
    {
        public int rank;
        public double pp;
        public string name;
        public string author_name;
        public string mapper;
        public string difficuly;
        public double score;
        public int max_score;

        public Map()
        {
            
        }

        public Map(JObject data, int i)
        {
            this.rank = (int)data["scores"][i]["rank"];
            this.pp = (double)data["scores"][i]["pp"];
            this.name = (string)data["scores"][i]["songName"];
            this.author_name = (string)data["scores"][i]["SongAuthorName"];
            this.mapper = (string)data["scores"][i]["levelAuthorName"];
            this.difficuly = (string)data["scores"][i]["difficultyRaw"];
            this.score = (int)data["scores"][i]["score"];
            this.max_score = (int) data["scores"][i]["maxScore"];
        }

        public double GetAcc()
        {
            return (this.score / this.max_score)*100;
        }
        public void Print()
        {
            Console.WriteLine("Map : " + name);
            Console.WriteLine("Mapper : " + mapper);
            Console.WriteLine("diffuclty : " + difficuly);
            Console.WriteLine("pp : " + pp);
            Console.WriteLine("rank : "+rank);
            Console.WriteLine("Acc : " + GetAcc());
        }
    }
}
