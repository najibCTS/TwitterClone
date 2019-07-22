using _20_MVC_Assignment_01.Models;
using log4net;
using System;
using System.Data.Entity;
using System.Linq;

namespace _20_MVC_Assignment_01
{
    public class TweetRepository
	{
        private PRODEntities2 pRODEntities = new PRODEntities2();
        private static readonly ILog Log = LogManager.GetLogger(typeof(TweetRepository));

        public void Tweet(Tweet tweet)
        {
            pRODEntities.Tweets.Add(tweet);
            try
            {
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Tweet Repositiry - Tweet", ex);
                throw;
            }  
        }

		public Person Search(string name)
        {
            try
            {
                return pRODEntities.People.Where(a => a.fullName.Contains(name)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Tweet Repositiry - Search", ex);
                throw;
            }
        } 

		public Tweet GetTweet(int id)
        {
            try
            {
                return pRODEntities.Tweets.Where(a => a.tweet_id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Tweet Repositiry - GetTweet", ex);
                throw;
            }
        }

		public void Edit(Tweet tweet)
        {
            pRODEntities.Entry<Tweet>(tweet).State = EntityState.Modified;

            try
            {
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Tweet Repositiry - Edit", ex);
                throw;
            }
        }

		public void Delete(int id)
        {
            var tweet = pRODEntities.Tweets.Find(id);
            pRODEntities.Tweets.Remove(tweet);

            try
            {
                pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Tweet Repositiry - Delete", ex);
                throw;
            }
        }
    }
}