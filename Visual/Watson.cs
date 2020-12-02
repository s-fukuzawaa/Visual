using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.VisualRecognition.v3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Visual.Model.Img;

namespace Visual
{
    public class Watson
    {
        public ImageRootObject GetImage(string s)
        {
            IamAuthenticator authenticator = new IamAuthenticator(
   apikey: "nHTia0PXBMjC7O4KBeg9frVxU8x7UDhV9wGe3j3nnwe9"
   );

            VisualRecognitionService visualRecognition = new VisualRecognitionService("2018-03-19", authenticator);
            visualRecognition.SetServiceUrl("https://api.us-south.visual-recognition.watson.cloud.ibm.com/instances/d8c90f7d-3850-490e-aef2-7ed10d3dff46");
            var result2 = visualRecognition.ListClassifiers(verbose: true);
            
            FileStream fs = File.OpenRead(s);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);

            var result = visualRecognition.Classify(
             imagesFile: ms,
             imagesFilename: s.Substring(4));

                
            


            //Console.WriteLine(result.Response);
            ImageRootObject image =JsonConvert.DeserializeObject<ImageRootObject>(result.Response);
            return image;
        }
           

    }
}
