using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualPetAdoption.Data;
using VirtualPetAdoption.Models;

namespace VirtualPetAdoption.Pages
{
    public class ResultsModel : PageModel
    {
        // Add the database context
        private readonly PetAdoptionContext _context;
        //Create the page model using the database context
        public ResultsModel(PetAdoptionContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public int petId {get; set;}

        [BindProperty(SupportsGet = true)]
        public string UserName {get; set;}

        public Pet Pet {get; set;}

        public async Task<IActionResult> OnGetAsync()
        {
            if (petId <= 0)
            {
                return RedirectToPage(".Index");
            }
            //Get the pet from the database context and populate the  Pet Object we created earlier 
            Pet = await _context.Pets.FindAsync(petId);

            if(Pet == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

    }
}