using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries
{
  public class GetActivityList
  {
    public class ActivityQuery : IRequest<Result<List<Activity>>>
    {
    }

    public class Handler : IRequestHandler<ActivityQuery, Result<List<Activity>>>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Result<List<Activity>>> Handle(ActivityQuery request, CancellationToken cancellationToken)
      {
        return Result<List<Activity>>.Success(await _context.Activities.ToListAsync());
      }
    }
  }
}