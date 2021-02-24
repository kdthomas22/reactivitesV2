using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Commands
{
  public class DeleteActivity
  {
    public class DeleteActivityCommand : IRequest<Result<Unit>>
    {
      public Guid Id {get; set;}
    }

    public class Handler : IRequestHandler<DeleteActivityCommand, Result<Unit>>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
      {
        var activityToDelete = await _context.Activities.FindAsync(request.Id);
        //if(activityToDelete == null) return null;

        _context.Remove(activityToDelete);

        var result = await _context.SaveChangesAsync() > 0;

        if(!result) return Result<Unit>.Failure("Failed to delete the activity");
        
        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}