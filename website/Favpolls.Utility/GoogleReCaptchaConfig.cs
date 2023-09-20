using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Favpolls.Utility
{
    public class GoogleReCaptchaConfig
    {
        public string SiteKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
