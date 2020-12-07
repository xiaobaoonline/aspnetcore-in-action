using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Options_Sample
{
    public class MyOptionValidation : IValidateOptions<MyOption>
    {
        public ValidateOptionsResult Validate(string name, MyOption options)
        {
            if (options.MinAge < 0)
            {
                return ValidateOptionsResult.Fail("MinAge不能小于0");
            }
            return ValidateOptionsResult.Success;
        }
    }
}
