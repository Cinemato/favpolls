using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Utility
{
    public class GoogleReCaptcha : IGoogleReCaptcha
    {
        public bool Success { get; set; }
        public List<string>? ErrorCodes { get; set; }

        public IOptions<GoogleReCaptchaConfig> _config;

        public GoogleReCaptcha(IOptions<GoogleReCaptchaConfig> config)
        {
            _config = config;
        }

        public bool ValidateResponse(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            var client = new HttpClient();
            var secret = _config.Value.SecretKey;

            if (string.IsNullOrEmpty(secret)) return false;

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, token));
            var response = client.Send(request);

            var reader = new StreamReader(response.Content.ReadAsStream());
            var responseBody = reader.ReadToEnd();
            reader.Close();

            var result = JsonConvert.DeserializeObject<GoogleReCaptcha>(responseBody);

            return result.Success;
        }
    }
}
