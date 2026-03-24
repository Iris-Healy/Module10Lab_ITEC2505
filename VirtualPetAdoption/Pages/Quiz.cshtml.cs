using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualPetAdoption.Data;
using VirtualPetAdoption.Models;

namespace VirtualPetAdoption.Pages
{
    public class QuizModel : PageModel
    {
        private readonly PetAdoptionContext _context;
        //Add the page model with the database context - so the page can be updated with data from the database
        public QuizModel(PetAdoptionContext context)
        {
            _context = context;
        }
        
        //Bind properties persist the form data after the user adds their name and their energy preference
        [BindProperty]
        public string Name {get; set;}

        [BindProperty]
        public int EnergyPreference { get; set; }

        public void Onget()
        {
        }

        //On post method is called when the form submit button is clicked displaying a pet to the user
        public async Task<IActionResult> OnPostAsync()
        {
            var pets = await _context.Pets.ToListAsync();

            //Declare a variable to store the pet that is the best match and a variable to calculate the best pet
            Pet bestMatch = null;
            int smallestDifference = int.MaxValue;

            foreach (var pet in pets)
            {
                int difference = Math.Abs(pet.EnergyLevel - EnergyPreference);
                if (difference < smallestDifference)
                {
                    smallestDifference = difference;
                    bestMatch = pet;
                }
            }

            return RedirectToPage("./Results", new { petId = bestMatch.Id, userName = Name});

        }
    }
}