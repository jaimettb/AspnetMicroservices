using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviors
{
    public class ValidationBehavior<Trequest, Tresponse> : IPipelineBehavior<Trequest, Tresponse>
    {
        private readonly IEnumerable<IValidator<Trequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<Trequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<Tresponse> Handle(Trequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<Trequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }
        }
    }
}
