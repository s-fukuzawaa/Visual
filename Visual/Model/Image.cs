using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visual.Model
{
    public class Img
    {
        public class Class
        {
            public string @class { get; set; }
            public double score { get; set; }
            public string type_hierarchy { get; set; }
        }

        public class Classifier
        {
            public string classifier_id { get; set; }
            public string name { get; set; }
            public List<Class> classes { get; set; }
        }

        public class Image
        {
            public List<Classifier> classifiers { get; set; }
            public string source_url { get; set; }
            public string resolved_url { get; set; }
        }

        public class ImageRootObject
        {
            public List<Image> images { get; set; }
            public int images_processed { get; set; }
            public int custom_classes { get; set; }
        }
    }
}
