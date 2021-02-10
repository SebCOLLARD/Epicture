using System;
using System.Collections.Generic;
using System.Text;

namespace Epicture.Services
{
    class DataJsonApiToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public object scope { get; set; }
        public string refresh_token { get; set; }
        public int account_id { get; set; }
        public string account_username { get; set; }
    }

    public class MetaDataImage
    {
        public string id { get; set; }
        public object title { get; set; }
        public object description { get; set; }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public int bandwidth { get; set; }
        public object vote { get; set; }
        public bool favorite { get; set; }
        public object nsfw { get; set; }
        public object section { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public bool is_ad { get; set; }
        public bool in_most_viral { get; set; }
        public bool has_sound { get; set; }
        public IList<object> tags { get; set; }
        public int ad_type { get; set; }
        public string ad_url { get; set; }
        public string edited { get; set; }
        public bool in_gallery { get; set; }
        public string deletehash { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class DataJsonApiGallery
    {
        public IList<MetaDataImage> data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }



    public class ImageApi
    {
        public string id { get; set; }
        public object title { get; set; }
        public string description { get; set; }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public object bandwidth { get; set; }
        public object vote { get; set; }
        public bool favorite { get; set; }
        public object nsfw { get; set; }
        public object section { get; set; }
        public object account_url { get; set; }
        public object account_id { get; set; }
        public bool is_ad { get; set; }
        public bool in_most_viral { get; set; }
        public bool has_sound { get; set; }
       
        public int ad_type { get; set; }
        public string ad_url { get; set; }
        public string edited { get; set; }
        public bool in_gallery { get; set; }
        public string link { get; set; }
        public object comment_count { get; set; }
        public object favorite_count { get; set; }
        public object ups { get; set; }
        public object downs { get; set; }
        public object points { get; set; }
        public object score { get; set; }
        public int? mp4_size { get; set; }
        public string mp4 { get; set; }
        public string gifv { get; set; }
        public string hls { get; set; }
        public bool? looping { get; set; }
    }

    public class CompomentImage
    {
        public string id { get; set; }
        public string title { get; set; }
        public object description { get; set; }
        public int datetime { get; set; }
        public string cover { get; set; }
        public int cover_width { get; set; }
        public int cover_height { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public string privacy { get; set; }
        public string layout { get; set; }
        public int views { get; set; }
        public string link { get; set; }
        public int ups { get; set; }
        public int downs { get; set; }
        public int points { get; set; }
        public int score { get; set; }
        public bool is_album { get; set; }
        public object vote { get; set; }
        public bool favorite { get; set; }
        public bool nsfw { get; set; }
        public string section { get; set; }
        public int comment_count { get; set; }
        public int favorite_count { get; set; }
        public string topic { get; set; }
        public int images_count { get; set; }
        public bool in_gallery { get; set; }
        public bool is_ad { get; set; }
        public int ad_type { get; set; }
        public string ad_url { get; set; }
        public bool in_most_viral { get; set; }
        public bool include_album_ads { get; set; }
        public IList<ImageApi> images { get; set; }
        public string type { get; set; }
        public bool? animated { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? size { get; set; }
        public long? bandwidth { get; set; }
        public bool? has_sound { get; set; }
        public int? edited { get; set; }
        public string mp4 { get; set; }
        public string gifv { get; set; }
        public string hls { get; set; }
        public int? mp4_size { get; set; }
        public bool? looping { get; set; }
    }

    public class DataJsonApiAllGallery
    {
        public IList<CompomentImage> data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }



    public class DataImg
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string cover { get; set; }
        public int cover_width { get; set; }
        public int cover_height { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public string privacy { get; set; }
        public int views { get; set; }
        public string link { get; set; }
        public int ups { get; set; }
        public int downs { get; set; }
        public int points { get; set; }
        public int score { get; set; }
        public bool is_album { get; set; }
        public string vote { get; set; }
        public bool favorite { get; set; }
        public bool nsfw { get; set; }
        public object section { get; set; }
        public int comment_count { get; set; }
        public int favorite_count { get; set; }
        public string topic { get; set; }
        public string topic_id { get; set; }
        public int images_count { get; set; }
        public int datetime { get; set; }
        public bool in_gallery { get; set; }
        public bool in_most_viral { get; set; }
        public object tags { get; set; }
        public object images { get; set; }
        public bool has_sound { get; set; }
        public bool animated { get; set; }
        public string type { get; set; }
        public int size { get; set; }
    }

    public class FavoriteClass
    {
        public int status { get; set; }
        public bool success { get; set; }
        public IList<DataImg> data { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public object title { get; set; }
        public object description { get; set; }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public int bandwidth { get; set; }
        public object vote { get; set; }
        public bool favorite { get; set; }
        public object nsfw { get; set; }
        public object section { get; set; }
        public object account_url { get; set; }
        public int account_id { get; set; }
        public bool is_ad { get; set; }
        public bool in_most_viral { get; set; }
        public IList<object> tags { get; set; }
        public int ad_type { get; set; }
        public string ad_url { get; set; }
        public bool in_gallery { get; set; }
        public string deletehash { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class UploadImage
    {
        public Data data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }

    public class UploadImage2
    {
        public IList<Data> data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }
}
