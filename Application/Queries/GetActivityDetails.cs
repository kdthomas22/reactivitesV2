using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Queries
{
  public class GetActivityDetails
  {
    public class ActivityDetailsQuery : IRequest<Result<Activity>>
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<ActivityDetailsQuery, Result<Activity>>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task <Result<Activity>> Handle(ActivityDetailsQuery request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities.FindAsync(request.Id);

        return Result<Activity>.Success(activity);
      }
    }
  }
}