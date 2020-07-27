using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sharpshooter
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;


        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "AToJPXL0zGB45CZHv7aP1_K2b0Jyp2z7OfLpvsI6-hL9ajGEEPqdk7rmIvYFOOiVa9pKY1oFgfIfoM_m";
            clientSecret = "ENXshspXBiWsRJ3_bT2OYTRNpidzNniPuR8I_xrBIxHFp9PCaRQ0nDkXFlbw_p_9vvD7-Zgk0w_JbRqm";
        }

        private static Dictionary<string, string> getconfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}