using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Visual.Model.Img;

namespace Visual.Pages
{
    public class FirstModel : PageModel
    {
        [BindProperty]
        public String SignatureDataUrl { get; set; }
        [BindProperty]
        public String color { get; set; } 
        public List<SelectListItem> Colors { get; set; }
        public void OnGet()
        {
            Colors = new List<SelectListItem>
            {
                new SelectListItem{Value="1", Text="blue"},
                new SelectListItem{Value="2", Text="green"},
                new SelectListItem{Value="3", Text="yellow"},
                new SelectListItem{Value="4", Text="orange"},
                new SelectListItem{Value="5", Text="red"},
                new SelectListItem{Value="6", Text="brown"},
            };
        }

       
        public IActionResult OnPost()
        {
            String hey = color;
            if (String.IsNullOrWhiteSpace(SignatureDataUrl)) return Page();

            var base64Signature = SignatureDataUrl.Split(",")[1];
            var binarySignature = Convert.FromBase64String(base64Signature);
            System.IO.File.WriteAllBytes("Imgs/Signature.png", binarySignature);

            Watson watson = new Watson();
            ImageRootObject img = watson.GetImage("Imgs/Signature.png");
            string save = img.images[0].classifiers[0].classes[0].@class;
            double save2 = img.images[0].classifiers[0].classes[0].score;


            return Redirect("https://localhost:44311/Next?id=" + save + "!" + save2+"!none!1");
        }
    }
}