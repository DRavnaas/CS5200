﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Globalization;

namespace Fish360Project
{
    /// <summary>
    /// Summary description for FishService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FishService : System.Web.Services.WebService
    {
        [WebMethod]
        public FishTO GetFishForId(int fishId, string token)
        {
            using (var db = new Fish360Project.f360Entities())
            {
                var guid = new Guid(token);
                var fishDB = (from fishLQ in db.Fish
                             where fishLQ.id == fishId
                             select fishLQ).FirstOrDefault();
                if (fishDB == null)
                    return null;

                FishTO fishTO = new FishTO();
                fishTO.id = fishDB.id;
                fishTO.tripId = (int)fishDB.tripId;
                fishTO.name = fishDB.name;
                fishTO.weight = (double)fishDB.weight;
                fishTO.length = (double)fishDB.length;
                fishTO.species = fishDB.species;
//                fishTO.notes = System.Text.Encoding.UTF8.GetString(fishDB.notes);
                fishTO.notes = fishDB.notes;
                if (fishDB.caughtDate != null)
                {
                    fishTO.caughtDate = ((DateTime)fishDB.caughtDate).ToString("yyyy-MM-dd");
                    fishTO.caughtTime = ((DateTime)fishDB.caughtDate).TimeOfDay.ToString("HH:mm:ss tt");
                }
                return fishTO;
            }
        }

        [WebMethod]
        public string DeleteFishForId(int fishId, string token)
        {
            using (var db = new Fish360Project.f360Entities())
            {
                var guid = new Guid(token);
                var query = (from fish in db.Fish
                             where fish.id == fishId
                             select fish).FirstOrDefault();
                if (query == null)
                    return null;

                db.Fish.Attach(query);
                db.Fish.Remove(query);
                db.SaveChanges();
            }
            return null;
        }

        [WebMethod]
        public string UpdateFish(FishTO fishTO, string token)
        {
            using (var db = new Fish360Project.f360Entities())
            {
                var guid = new Guid(token);
                var fishDB = (from fish in db.Fish
                             where fish.id == fishTO.id
                             select fish).FirstOrDefault();
                if (fishDB == null)
                    return null;

                db.Fish.Attach(fishDB);

                fishDB.name = fishTO.name;
                fishDB.species = fishTO.species;
                fishDB.length = fishTO.length;
                fishDB.weight = fishTO.weight;
                fishDB.notes = fishTO.notes;
                string date = fishTO.caughtDate + " " + fishTO.caughtTime;
                fishDB.caughtDate = DateTime.ParseExact(fishTO.caughtDate, "yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);

                db.SaveChanges();
            }
            return null;
        }

        [WebMethod]
        public string CreateFish(FishTO fishTO, string token)
        {
            using (var db = new Fish360Project.f360Entities())
            {
                Fish fishDB = new Fish();
                fishDB.name = fishTO.name;
                fishDB.species = fishTO.species;
                fishDB.length = fishTO.length;
                fishDB.weight = fishTO.weight;
                fishDB.notes = fishTO.notes;
                string date = fishTO.caughtDate + " " + fishTO.caughtTime;
                fishDB.caughtDate = DateTime.ParseExact(fishTO.caughtDate, "yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);
                fishDB.tripId = fishTO.tripId;

                db.Fish.Add(fishDB);
                db.SaveChanges();
            }
            return null;
        }

        [WebMethod]
        public List<FishTO> GetAllFishForTripId(int tripId, string token)
        {
            using (var db = new Fish360Project.f360Entities())
            {
                var guid = new Guid(token);
                var query = (from fish in db.Fish
                             join trip in db.Trips on fish.tripId.Value equals trip.id
                             join user in db.Users on trip.userId.Value equals user.id
                             where user.token == guid && trip.id == tripId
                             select fish);

                List<FishTO> fishes = new List<FishTO>();

                foreach (var f in query)
                {
                    FishTO fish = new FishTO();
                    fish.id = f.id;
                    fish.tripId = (int)f.tripId;
                    fish.name = f.name;
                    fish.notes = f.notes;
                    fish.weight = (double)f.weight;
                    fish.length = (double)f.length;
                    fish.species = f.species;
                    if (f.caughtDate != null)
                    {
                        fish.caughtDate = ((DateTime)f.caughtDate).ToString("yyyy-MM-dd");
                        fish.caughtTime = ((DateTime)f.caughtDate).TimeOfDay.ToString("HH:mm:ss tt");
                    }
                    fishes.Add(fish);
                }

                return fishes;
            }
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
