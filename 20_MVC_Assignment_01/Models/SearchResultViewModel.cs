using System.Collections.Generic;

namespace _20_MVC_Assignment_01.Models
{
    public class SearchResultViewModel
	{
		public List<Person> AvailableUsers { get; set; }
		public List<Person> Following { get; set; }
	}
}