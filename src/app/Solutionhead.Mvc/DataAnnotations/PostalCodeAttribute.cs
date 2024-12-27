using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Solutionhead.Libs.Mvc3;

namespace Solutionhead.Web.Mvc.DataAnnotations
{
    public class PostalCodeAttribute : RegularExpressionAttribute, IClientValidatable
    {
        private const string US_PATTERN = @"^[0-9]{5}([\\s-]{1}[0-9]{4})?$";

        public PostalCodeAttribute()
            : base(US_PATTERN) { }

        public PostalCodeAttribute(PostalCodeType Format)
            : base(getPattern(Format)) { }

        private static string getPattern(PostalCodeType Format)
        {
            string patern = string.Empty;

            switch (Format)
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
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), base.Pattern);
        }

        #endregion
    }
}
