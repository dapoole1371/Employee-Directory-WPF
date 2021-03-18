using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee_Directory_WPF.Models;
using FluentValidation;

namespace Employee_Directory_WPF.Validation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            //Error Handling First Name
            RuleFor(e => e.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure) //Cascade causes the error checker to stop on the first failure instead of going through each check.
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 30).WithMessage("Length of {PropertyName} must be 2-30 characters.")
                .Must(IsValidName).WithMessage("{PropertyName} contains invalid characters.");

            //Error handling Last Name
            RuleFor(e => e.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 30).WithMessage("Length of {PropertyName} must be 2-30 characters.")
                .Must(IsValidName).WithMessage("{PropertyName} contains invalid characters.");

            //Error handling Job Title
            RuleFor(e => e.JobTitle)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 30).WithMessage("Length of {PropertyName} must be 2-30 characters.")
                .Must(IsValidJob).WithMessage("{PropertyName} cannot contain special characters.");
        }

        
        //Method to check validity of a user input for first and last name
        protected bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }
        //Method to check validity of a user input for job title
        protected bool IsValidJob(string jobTitle)
        {
            jobTitle = jobTitle.Replace(" ", "");
            jobTitle = jobTitle.Replace("-", "");
            return jobTitle.All(Char.IsLetterOrDigit);
        }
    }

    public class EmployeeExists : AbstractValidator<Employee>
    {
        public EmployeeExists()
        {
            RuleFor(e => e.ID)
                .NotEmpty();
        }
    }
}
