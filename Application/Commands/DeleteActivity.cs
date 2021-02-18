using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Commands
{
  public class DeleteActivity
  {
    public class DeleteActivityCommand : IRequest
    {
      public Guid Id {get; set;}
    }

    public class Handler : IRequestHandler<DeleteActivityCommand>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
      {
        var activityToDelete = await _context.Activities.FindAsync(request.Id);
        _context.Remove(activityToDelete);
        await _context.SaveChangesAsync();
        return Unit.Value;
      }
    }
  }
}