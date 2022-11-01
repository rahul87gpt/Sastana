using System;
using System.Collections.Generic;
using System.Text;

namespace SastanaArt.APIModels.ResponseModel
{
   public class LoginResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DeviceData
    {
        public string user_id { get; set; }
        public string device_name { get; set; }
        public string type { get; set; }
        public string os { get; set; }
        public string app_version { get; set; }
        public string mac_id { get; set; }
        public string id { get; set; }
    }

    public class Data
    {
        public string username { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date { get; set; }
        public string id { get; set; }
        public string device_id { get; set; }
        public string device_number { get; set;}
        public string token { get; set; }
        public List<object> nft_data { get; set; }
        public List<DeviceData> device_data { get; set; }

    }
}
