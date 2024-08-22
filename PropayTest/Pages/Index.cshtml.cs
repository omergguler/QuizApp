using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PropayTest.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Posted succesfully");
            
            if (ModelState.IsValid)
            {
                
                if(Username == null || Password == null)
                {
                    Console.WriteLine("fill all fields");
                }
                return null;
                
            }

            return Page(); 
        }
    }

}
