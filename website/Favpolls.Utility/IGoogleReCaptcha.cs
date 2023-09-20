using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Utility
{
    public interface IGoogleReCaptcha
    {
        bool Success { get; set; }
        List<string>? ErrorCodes { get; set; }

        bool ValidateResponse(string token);
    }
}
