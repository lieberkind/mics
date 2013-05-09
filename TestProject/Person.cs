using MiCS;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TestProject
{
    public class Validator
    {
        [MixedSide]
        public bool IsStringValid(string str)
        {
            var stringLengthValidator = new StringLengthValidator();
            return stringLengthValidator.IsStringLengthValid(str);
        }
    }

    public class StringLengthValidator
    {
        [MixedSide]
        public bool IsStringLengthValid(string str)
        {
            return str.Length > 5 && str.Length < 128;
        }
    }
}





