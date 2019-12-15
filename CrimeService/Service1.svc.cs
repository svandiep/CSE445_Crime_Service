using CrimeService.Res;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

//Crime service takes a city name and state or zipcode as input and returns a crime level between 0 - 50
//Uses CDE API for crime records and hippotom.us API for zip code conversion. 
// ORI number is used by each agency that reports from to the CDE database
// City to ORI data is embedded in service

namespace CrimeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public static string cdeKey = " ";
        public static string zipKey = " ";

        public int crimeCity(string city, string state)
        {
            city = UppercaseFirst(city);//Check for format
            state = UppercaseFirst(state);//Check for format
            RootObject agencyObject = JsonConvert.DeserializeObject<RootObject>(ContentLoading.GetJsonContent());
            int size = agencyObject.agency.Count;
            string ori = agencyObject.agency[0].ori;
            for (int i = 0; i < size; i++)//Find ORI number for City and State
            {
                if (agencyObject.agency[i].agency == city)
                {
                    if (agencyObject.agency[i].state == state)
                    {
                        ori = agencyObject.agency[i].ori;
                        i = size;
                    }
                }
            }
            string url1 = "https://api.usa.gov/crime/fbi/sapi/api/summarized/agencies/" + ori;
            string url2 = "/offenses/2017/2017?api_key=" + cdeKey;
            string url = String.Concat(url1, url2);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//Querey CDE database according to ORI number
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            //Use algorithim to compare Violent crime to property crime and homicides
            CrimeObject crimeobject = JsonConvert.DeserializeObject<CrimeObject>(responsereader);
            int violent = crimeobject.results[11].actual;
            int property = crimeobject.results[7].actual;
            int homicide = crimeobject.results[3].actual;
            float rate = (float)violent / (float)property;
            rate = rate + (((float)homicide / (float)violent)*2);
            rate = rate * 100;
            return (int)rate;
        }

        public int crimeZip(int zipcode)
        {
            string url = "http://api.zippopotam.us/us/" + zipcode;
            Console.WriteLine(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // get City from zip code
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            zipObject zipobject = JsonConvert.DeserializeObject<zipObject>(responsereader);

            int rate = crimeCity(zipobject.places[0].placename, zipobject.places[0].state);

            return rate;
        }


        public class RootObject
        {
            public List<Agency> agency { get; set; }
        }

        public class Agency
        {
            public string ori { get; set; }
            public string state { get; set; }
            public string agency { get; set; }
        }

        public class CrimeObject
        {
            public List<Results> results { get; set; }
            public Pagnation pagnation { get; set; }
        }

        public class Pagnation
        {
            public int count { get; set; }
            public int page { get; set; }
            public int pages { get; set; }
            public int per_page { get; set; }
        }

        public class Results
        {
            public string ori { get; set; }
            public int data_year { get; set; }
            public string offense { get; set; }
            public string stat_abbr { get; set; }
            public int cleared { get; set; }
            public int actual { get; set; }
        }

        public class zipObject
        {

            [JsonProperty(PropertyName = "post code")]
            public string postcode { get; set; }

            public string country { get; set; }

            [JsonProperty(PropertyName = "country abbreviation")]
            public string countryabbreviation { get; set; }
            public List<Places> places { get; set; }
        }

        public class Places
        {
            [JsonProperty(PropertyName = "place name")]
            public string placename { get; set; }

            public decimal longitude { get; set; }
            public string state { get; set; }

            [JsonProperty(PropertyName = "state abbreviation")]
            public string stateabbreviation { get; set; }

            public decimal latitude { get; set; }
        }

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }

}
