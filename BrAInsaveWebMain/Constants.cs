using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAInsaveWebMain
{
    public static class Constants
    {
        public static class CognitiveService
        {
            public const string BASE_URI = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/";
            public const string SUBSCRIPTION_KEY = "1617d8c5cf1145fcabe716e600b6b6ae";
        }

        public static class Blob
        {
            public const string STORAGE_ACCOUNT = "brainsavemain";
            public const string SUBSCRIPTION_KEY = "g7jAnP7SQ+7BO0va9ILro5R/Z38ocDtZDxoKk+Ta9lhwKtjuv8BkX8ZM13dnNpGscGMdOlZ/KzbornAfbq36XA==";
            public const string JSON_CONTAINER = "****undefined****";
        }
    }
}
