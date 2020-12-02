using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.Web.Helpers;
using Microsoft.Extensions.Configuration;
using Visual.Utilities;
using static Visual.Model.Img;

namespace Visual.Pages
{
    public class IndexModel : PageModel
    {
        

        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".jpg",".txt" };
        private readonly string _targetFilePath;
        
        public IndexModel(IConfiguration config)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

            // To save physical files to the temporary files folder, use:
            //_targetFilePath = Path.GetTempPath();
        }

        [BindProperty]
        public BufferedSingleFileUploadPhysical FileUpload { get; set; }

        public string Result { get; private set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }

            var formFileContent =
                await FileHelpers.ProcessFormFile<BufferedSingleFileUploadPhysical>(
                    FileUpload.FormFile, ModelState, _permittedExtensions,
                    _fileSizeLimit);

            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }

            // For the file name of the uploaded file stored
            // server-side, use Path.GetRandomFileName to generate a safe
            // random file name.

            string help = FileUpload.FormFile.FileName;
            int a = help.IndexOf(".");
            var trustedFileNameForFile = Path.GetRandomFileName();
            var filePath = Path.Combine(
                _targetFilePath, "hey"+help.Substring(a));

            // **WARNING!**
            // In the following example, the file is saved without
            // scanning the file's contents. In most production
            // scenarios, an anti-virus/anti-malware scanner API
            // is used on the file before making the file available
            // for download or for use by other systems. 
            // For more information, see the topic that accompanies 
            // this sample.

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(formFileContent);

                // To work directly with a FormFile, use the following
                // instead:
                //await FileUpload.FormFile.CopyToAsync(fileStream);
            }

         Watson watson = new Watson();
            ImageRootObject img =  watson.GetImage("Imgs/"+ "hey" + help.Substring(a));
           string save= img.images[0].classifiers[0].classes[0].@class;
            double save2 = img.images[0].classifiers[0].classes[0].score;

            return Redirect("https://localhost:44311/Next?id="+save+"!"+save2+"!"+help.Substring(a)+"!0");
        }
    }

    public class BufferedSingleFileUploadPhysical
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }

        

        
    
}
