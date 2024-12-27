using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Solutionhead.Web.Mvc.DataAnnotations
{
    public class PostalCodeAttribute : RegularExpressionAttribute, IClientValidatable
    {
        private const string US_PATTERN = @"^[0-9]{5}([\\s-]{1}[0-9]{4})?$";

        public PostalCodeAttribute()
            : this(PostalCodeType.US) { }

        public PostalCodeAttribute(PostalCodeType format)
            : base(getPattern(format)) { }

        private static string getPattern(PostalCodeType format)
        {
            string patern;

            switch (format)
            {
                case PostalCodeType.US:
                default:
                    patern = US_PATTERN;
                    break;
            }

            return patern;
        }

        #region IClientValidatable Members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Pattern);
        }

        #endregion
    }
}
