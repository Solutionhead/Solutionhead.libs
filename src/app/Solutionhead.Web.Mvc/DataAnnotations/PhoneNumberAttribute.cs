using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Solutionhead.Libs.Mvc3.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PhoneNumberAttribute : DataTypeAttribute, IClientValidatable
    {
        #region fields

        private const string US_ALL = @"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$";
        private const string US_STANDARD = @"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$";

        private readonly string _pattern = string.Empty;

        #endregion

        #region constructors

        public PhoneNumberAttribute()
            : this(PhoneNumberType.USAll) { }

        public PhoneNumberAttribute(PhoneNumberType PhoneNumberFormat)
            : base(DataType.PhoneNumber)
        {
            ErrorMessage = "The {0} field is not a valid phone number.";

            switch (PhoneNumberFormat)
            {
                case PhoneNumberType.USStandard:
                    _pattern = US_STANDARD;
                    break;
                case PhoneNumberType.USAll:
                default:
                    _pattern = US_ALL;
                    break;
            }
        }

        #endregion

        #region properties

        private Regex Validator
        {
            get { return new Regex(_pattern, RegexOptions.IgnoreCase); }
        }

        #endregion

        #region methods

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Validator.ToString());
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var valueAsString = value as string;
            return valueAsString != null && Validator.Match(valueAsString).Length > 0;
        }

        #endregion
    }
}