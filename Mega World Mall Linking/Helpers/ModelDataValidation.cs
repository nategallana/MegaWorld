using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Helpers
{
    public class ModelDataValidation
    {
        // This method validates the model object based on the data annotations applied to its properties.
        public void Validate(object model)
        {
            string errorMessage = ""; // Initialize an empty string for error messages.
            List<ValidationResult> results = new List<ValidationResult>(); // List to hold validation results.
            ValidationContext context = new ValidationContext(model); // Context for the validation based on the model object.

            // Validate the model object and populate the results list.
            bool isValid = Validator.TryValidateObject(model, context, results, true);

            // If the model is not valid, compile the error messages and throw an exception.
            if (!isValid)
            {
                foreach (var item in results)
                {
                    errorMessage += "- " + item.ErrorMessage + "\n";
                }
                throw new Exception(errorMessage); // Throw an exception with the compiled error messages.
            }
        }
    }
}
