using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Visual.Pages
{
    public class NextModel : PageModel
    {
        [BindProperty]
        public string classifier { get; set; }
        public string obj { get; set; }
        public string score { get; set; }
        public int upload { get; set; }
        public string type { get; set; }
        public void OnGet()
        {
            string s = Request.QueryString.Value.Substring(4);
            string[] temp = s.Split("!");
            string[] temp2 = temp[0].Split("%20");
            upload = int.Parse(temp[temp.Length - 1]);
            if(upload==0)
            {
                type = temp[temp.Length - 2];
            }
            obj = "";
            
            for (int i=0; i<temp2.Length;i++)
            {
                obj += temp2[i];
            }
            score = Double.Parse(temp[1])*100+"%";


        }
        public void OnPost()
        {
            
        }
    }
}