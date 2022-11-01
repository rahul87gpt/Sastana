using System;
using System.Collections.Generic;
using System.Text;

namespace SastanaArt.APIModels.ResponseModel
{
    public class Datum
    {

        public UserDetails user_id { get; set; }
        public string contain_mode { get; set; }
        public int rotation { get; set; }
        public string device_name { get; set; }
        public string type { get; set; }
        public string os { get; set; }

        public string app_version { get; set; }
        public string mac_id { get; set; }

        public string resolution { get; set; }
        public string aspect_ratio { get; set; }
        public string last_login { get; set; }
        public string createdAt { get; set; }

        public string updatedAt { get; set; }
        public string is_login { get; set; }

        public NFTDetails nft_id { get; set; }
        public string id { get; set; }
    }

    public class NFTResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Logout
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }

    public class UserDetails
    {

        public string date { get; set; }
        public string wallet_details { get; set; }

        public List<PreferencesDetails> preferences { get; set; }
        public string contract_address { get; set; }
        public string token { get; set; }
        public string token_standard { get; set; }
        public string blockchain { get; set; }
        public string username { get; set; }

        public AvatarImg avatar_img { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string id { get; set; }
    }

    public class NFTDetails 
    {
        public string user_id { get; set; }
        public string nft_name { get; set; }
        public string nft_token_id { get; set; }
        public string nft_address { get; set; }
        public string description { get; set; }
        public string image_link { get; set; }
        public string date { get; set; }
        public string nft_categorys_id { get; set; }

        public string is_premium { get; set; }
        public string resolution { get; set; }
        public string aspect_ratio { get; set; }
        public string rent_price { get; set; }


        public string minimum_duration { get; set; }
        public string maximum_duration { get; set; }
        public string package_status { get; set; }
        public string status_plan_1 { get; set; }
        public string status_plan_2 { get; set; }
        public string status_plan_3 { get; set; }
        public string status_plan_4 { get; set; }
        public bool is_active { get; set; }


        public int? contract_itemid { get; set; }
        public string wallet_details { get; set; }
        public string expires_after { get; set; }
        public string available_rent { get; set; }

        public List<string> key_words { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string id { get; set; }

    }

    public class AvatarImg 
    {
        public string type { get; set; }
        public List<int> data { get; set; }

    }

    public class PreferencesDetails 
    {
        public string _id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string __v { get; set; }


    }
}
