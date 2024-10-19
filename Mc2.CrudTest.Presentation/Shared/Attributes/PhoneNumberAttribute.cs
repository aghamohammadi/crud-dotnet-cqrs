using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using PhoneNumbers;

    public class PhoneNumberAttribute : ValidationAttribute
    {
        private readonly string _region;

        public PhoneNumberAttribute(string region = "US")
        {
            _region = region;
        }

       public override bool IsValid(object value)
        {
            var valueString = value as string;
            if (string.IsNullOrEmpty(valueString))
            {
                return true;
            }

            var util = PhoneNumberUtil.GetInstance();
            try
            {
                var number = util.Parse(valueString, "US");
                return util.IsValidNumber(number);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }

}
