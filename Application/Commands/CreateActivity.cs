using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Validation;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Commands
{
  public class CreateActivity
  {
    public class CreateActivityCommand : IRequest<Result<Unit>>
    {
      public Activity Activity { get; set; }
    }

    public class CommandValidator : AbstractValidator<CreateActivityCommand>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
      }
    }

    public class Handler : IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
      {
        _context.Activities.Add(request.Activity);

        var result = await _context.SaveChangesAsync() > 0;
        if(!result) return Result<Unit>.Failure("Failed to create activity");

        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}