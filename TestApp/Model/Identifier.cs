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
        public int Type { get; set; }        
        public Guid Personid { get; set; }
        public string Value { get; set; }
    }

    public class IdentifierValidator : AbstractValidator<Identifier>
    {
        public IdentifierValidator()
        {
            RuleFor(x => x.id).NotNull();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Value).NotEmpty();            
        }
    }
}
