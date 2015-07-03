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
            if (value.ToString() == "")
            {
                return new ValidationResult(false, "Haven't enter a name!");
            }

            if (((string)value)[0] < 65 || ((string)value)[0] > 122)
            {
                return new ValidationResult(false, "First character must be word or underline");
            }

            else
            {
                foreach (char c in (string)value)
                {
                    if (c == ' ')
                    {
                        return new ValidationResult(false, "Album name cannot contain blank space!");
                    }
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
