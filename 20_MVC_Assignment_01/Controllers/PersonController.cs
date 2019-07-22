using _20_MVC_Assignment_01.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _20_MVC_Assignment_01.Controllers
{
    public class PersonController : Controller
    {
        PersonRepository personRepository = new PersonRepository();

        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchPerson()
        {
            return PartialView("_Follow");
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            if (query != null)
            {
                SearchResultViewModel searchResultViewModel = personRepository.Search(query, User.Identity.Name);
                return PartialView("_SearchResultsPartial", searchResultViewModel);
            }
            return PartialView("Error");
        }


        public ActionResult Tweets()
        {
            List<Tweet> tweets = personRepository.GetTweets(User.Identity.Name);
            return PartialView("_Tweets", tweets);
        }

        public ActionResult Statisctis()
        {
            Person person = personRepository.GetPersonById(User.Identity.Name);
            return PartialView("_Statisctis", person);
        }

        public ActionResult Follow(string id, string currentUser)
        {
            personRepository.Follow(id, currentUser);
            return RedirectToAction("Index", "TwitterHome");
        }

        public ActionResult UnFollow(string id, string currentUser)
        {
            personRepository.UnFollow(id, currentUser);
            return RedirectToAction("Index", "TwitterHome");
        }

        public ActionResult Edit(string id)
        {
            return View(personRepository.GetPersonById(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, Person person)
        {
            personRepository.Edit(person);
            return RedirectToAction("Index", "TwitterHome");
        }

        public ActionResult Delete(string id)
        {
            return View(personRepository.GetPersonById(id));
        }

        [HttpPost]
        public ActionResult Delete(string id, Person per)
        {
            personRepository.Delete(id, per);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult FollowDetails()
        {
            Person person = personRepository.GetPersonById(User.Identity.Name);
            return View(person);
        }
    }
}