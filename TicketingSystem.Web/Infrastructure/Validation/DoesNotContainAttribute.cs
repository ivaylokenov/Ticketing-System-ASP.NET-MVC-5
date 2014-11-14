namespace TicketingSystem.Web.Infrastructure.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Property)]
    public class DoesNotContainAttribute : ValidationAttribute, IClientValidatable
    {
        private string word;

        public DoesNotContainAttribute(string word)
        {
            this.word = word;
            this.ErrorMessage = "{0} should not contain the word " + word;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString == null)
            {
                throw new ArgumentException("Does not contain attribute not set on string property");
            }

            if (!valueAsString.Contains(this.word))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ValidationType = "doesnotcontainword",
                ErrorMessage = this.ErrorMessage
            };
        }
    }
}