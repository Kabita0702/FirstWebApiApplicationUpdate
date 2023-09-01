﻿using FirstWebApiApplication.Models.Domain;
using Newtonsoft.Json;
using System.Net;

namespace FirstWebApiApplication.Repository
{
    public static class UserDataRepository
    {
        public static void WriteUserData(List<User> _jsonData)
        {
            //string json = JsonConvert.SerializeObject(_jsonData.ToArray());

            File.WriteAllText(@".\Json\User.json", JsonConvert.SerializeObject(_jsonData.ToArray(), Newtonsoft.Json.Formatting.Indented));
        }
        public static List<User> GetUserData()
        {
            var _jsonData = new WebClient().DownloadString(@".\Json\User.json");
            return JsonConvert.DeserializeObject<List<User>>(_jsonData);
        }
        public static void WriteUserDetailsData(List<UserDetails> _jsonData)
        {
            //string json = JsonConvert.SerializeObject(_jsonData.ToArray());

            File.WriteAllText(@".\Json\UserDetails.json", JsonConvert.SerializeObject(_jsonData.ToArray(), Newtonsoft.Json.Formatting.Indented));
        }
        public static List<UserDetails> GetUserDetailsData()
        {
            var _jsonData = new WebClient().DownloadString(@".\Json\UserDetails.json");
            return JsonConvert.DeserializeObject<List<UserDetails>>(_jsonData);
        }
        
    }
    public class GenricClass<T1>
    {
        public List<T1> ReadData(string path)
        {
            var _jsonData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T1>>(_jsonData); ;
        }

        public void WriteData(string path, List<T1> _jsonData)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(_jsonData.ToArray(), Formatting.Indented));
        }
    }
}
