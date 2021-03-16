using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestProject.Captcha
{
    public class GoogleCaptcha
    {
        public bool Success { get; set; }
        public static bool Validate(string captchaResponse){
            try{
                if(string.IsNullOrEmpty(captchaResponse)){
                    return false;
                }
                var client = new System.Net.WebClient();
                var secret = "***";
                if(string.IsNullOrEmpty(secret)){
                    return false;
                }
                var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, captchaResponse)); 
                var captcha =  JsonConvert.DeserializeObject<GoogleCaptcha>(googleReply);
                return captcha.Success;

            }
            catch(Exception){
                throw;
            }
        }
    }
}