using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Validation;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Commands
{
  public class EditActivity
  {
    public class EditActivityCommand : IRequest<Result<Unit>>
    {
      public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<EditActivityCommand, Result<Unit>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

    public class CommandValidator : AbstractValidator<EditActivityCommand>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
      }
    }

      public async Task<Result<Unit>> Handle(EditActivityCommand request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities.FindAsync(request.Activity.Id);
        if(activity == null) return null;

        _mapper.Map(request.Activity, activity);

        var result = await _context.SaveChangesAsync() > 0;
        if(!result) return Result<Unit>.Failure("Failed to update activity");
        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}