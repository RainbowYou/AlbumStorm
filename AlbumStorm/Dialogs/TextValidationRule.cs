using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

namespace AlbumStorm.Dialogs
{
    class TextValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //throw new NotImplementedException();
            if(((string)value).Equals(""))
            {
                return new ValidationResult(false, "Haven't enter a name!");
            }

            //Name is valid
            return new ValidationResult(true, null);
        }
    }
}
