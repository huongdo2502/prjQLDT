

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLDT.Service
{
    public class EntityValidationResult
    {
        public IList<ValidationResult> Errors { get; private set; }

        public bool HasError
        {
            get
            {
                return Errors.Count > 0;
            }
        }

        public string ErrorSummary()
        {
            string errorSummary = "";

            if (HasError)
            {
                foreach (var item in Errors)
                {
                    errorSummary += item.ErrorMessage + "\n";
                }
            }

            return errorSummary;
        }

        public EntityValidationResult(IList<ValidationResult> errors = null)
        {
            if (errors != null)
            {
                Errors = errors;
                throw new Exception(ErrorSummary());
            }
            else
            {
                Errors = new List<ValidationResult>();
            }
        }
    }

    public interface IValidationService
    {
        void Validate<T>(T entity) where T : class;
    }

    public class ValidationService : IValidationService
    {
        public void Validate<T>(T entity) where T : class
        {
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            var isValid = Validator.TryValidateObject
            (
                entity,
                vc,
                validationResults,
                true
            );

            if (isValid)
            {
                new EntityValidationResult(null);
            }
            else
            {
                new EntityValidationResult(validationResults);
            }
        }
    }
}