using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAInsaveWebMain.Models
{
    //public class Config
    //{
    //    public CognitiveServiceConfig cognitiveServiceConfig { get; set; }
    //    public BlobServiceConfig blobServiceConfig { get; set; }
    //}

    public class CognitiveServiceConfig
    {
        public string baseURI { get; set; }
        public string subscriptionKey { get; set; }
    }

    public class BlobServiceConfig
    {
        public string storageAccount { get; set; }
        public string subscriptionKey { get; set; }
        public string blobContainer { get; set; }
        public string connectionString { get; set; }
    }
}
