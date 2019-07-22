using _20_MVC_Assignment_01.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace _20_MVC_Assignment_01.Controllers
{
    public class TwitterHomeController : Controller
    {
        TweetRepository tweetRepository = new TweetRepository();

        // GET: TwitterHome
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tweet(TweetViewModels tweetViewModels)
        {
            if (ModelState.IsValid)
            {
                Tweet tweet = new Tweet
                {
                    user_id = User.Identity.GetUserName(),
                    message = tweetViewModels.Message,
                    created = DateTime.Now
                };
                tweetRepository.Tweet(tweet);
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Search(string name)
        {
            return PartialView("_Follow", tweetRepository.Search(name));
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            int tweetId = Convert.ToInt32(id);
            return View(tweetRepository.GetTweet(tweetId));
        }

        [HttpPost]
        public ActionResult Edit(string id, Tweet tweet)
        {
            tweetRepository.Edit(tweet);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            int tweetId = Convert.ToInt32(id);
            return View(tweetRepository.GetTweet(tweetId));
        }

        [HttpPost]
        public ActionResult Delete(string id, Tweet tweet)
        {
            tweetRepository.Delete(Convert.ToInt32(id));
            return RedirectToAction("Index");
        }
    }
}