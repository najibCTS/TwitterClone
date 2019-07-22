using _20_MVC_Assignment_01.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _20_MVC_Assignment_01
{
    public class PersonRepository
    {
        private PRODEntities2 pRODEntities = new PRODEntities2();
        private static readonly ILog Log = LogManager.GetLogger(typeof(PersonRepository));

        public SearchResultViewModel Search(string query, string user_id)
        {
            try
            {
                List<Person> searchlist = pRODEntities.People.Where(a => a.fullName.Contains(query) && a.user_id != user_id).ToList();
                Person person = pRODEntities.People.Include("Tweets").Include("Person1").Include("People").Where(a => a.user_id == user_id).First();
                SearchResultViewModel searchResultViewModel = new SearchResultViewModel
                {
                    AvailableUsers = searchlist,
                    Following = person.People.ToList()
                };
                return searchResultViewModel;
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - Search", ex);
                throw;
            }  
        }

        public List<Tweet> GetTweets(string user_id)
        {
            try
            {
                Person person = pRODEntities.People.Include("Tweets").Include("Person1").Include("People").Where(a => a.user_id == user_id).First();
                List<Tweet> tweets = person.People.SelectMany(a => a.Tweets).ToList().Union(pRODEntities.Tweets.Where(b => b.user_id == person.user_id).ToList()).ToList();
                return tweets;
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - GetTweets", ex);
                throw;
            }
        }

        public Person GetPersonById(string user_id)
        {
            try
            {
                Person person = pRODEntities.People.Include("Tweets").Include("Person1").Include("People").Where(a => a.user_id == user_id).First();
                return person;
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - GetPersonById", ex);
                throw;
            }
        }

        public void Follow(string id, string currentUser)
        {
            try
            {
                Person person = pRODEntities.People.Find(id);
                pRODEntities.People.Find(currentUser).People.Add(person);
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - Follow", ex);
                throw;
            }
        }

        public void UnFollow(string id, string currentUser)
        {
            try
            {
                Person person = pRODEntities.People.Find(id);
                pRODEntities.People.Find(currentUser).People.Remove(person);
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - UnFollow", ex);
                throw;
            }
        }

        public void Edit(Person person)
        {
            try
            {
                person.password = Helper.EncodePasswordMd5(person.password);
                pRODEntities.Entry<Person>(person).State = System.Data.Entity.EntityState.Modified;
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - Edit", ex);
                throw;
            }  
        }

        public void Delete(string id, Person per)
        {
            try
            {
                Person person = pRODEntities.People.Include("Tweets").Include("Person1").Include("People").Where(a => a.user_id == id).First();
                pRODEntities.People.Remove(person);
                pRODEntities.Tweets.RemoveRange(pRODEntities.Tweets.Where(a => a.user_id == id).ToList());
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Person Repositiry - Delete", ex);
                throw;
            }
        }
    }
}