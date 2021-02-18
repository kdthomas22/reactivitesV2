using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Queries
{
  public class GetActivityDetails
  {
    public class ActivityDetailsQuery : IRequest<Activity>
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<ActivityDetailsQuery, Activity>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Activity> Handle(ActivityDetailsQuery request, CancellationToken cancellationToken)
      {
        return await _context.Activities.FindAsync(request.Id);
      }
    }
  }
}