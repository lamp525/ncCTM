using System;

namespace CTM.Core
{
    public class BooleanFormatter : IFormatProvider, ICustomFormatter
    {
        private string _trueString, _falseString;

        public BooleanFormatter(string trueString, string falseString)
        {
            this._trueString = trueString;
            this._falseString = falseString;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            bool formatValue = Convert.ToBoolean(arg);

            return formatValue ? _trueString : _falseString;
        }

        public object GetFormat(Type formatType)
        {
            return this;
        }
    }
}