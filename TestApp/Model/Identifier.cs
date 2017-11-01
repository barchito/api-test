using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public class Identifier
    {
        [Key]
        public Guid id { get; set; }
        [NotMapped]
        public IdentificationType idenityType { get; set; }
        public Guid Personid { get; set; }
        public string Value { get; set; }
    }

    public enum IdentificationType
    {
        email = 1,
        accesscard = 2,
        licenseplate = 3
    }

    public class IdentifierValidator : AbstractValidator<Identifier>
    {
        public IdentifierValidator()
        {
            RuleFor(x => x.id).NotNull();
            RuleFor(x => x.Value).NotEmpty();            
        }
    }
}
