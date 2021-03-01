using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries
{
  public class GetActivityList
  {
    public class ActivityQuery : IRequest<Result<List<ActivityDto>>>
    {
    }

    public class Handler : IRequestHandler<ActivityQuery, Result<List<ActivityDto>>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

      public async Task<Result<List<ActivityDto>>> Handle(ActivityQuery request, CancellationToken cancellationToken)
      {
        var activities = await _context.Activities.ProjectTo<ActivityDto>(_mapper.ConfigurationProvider).ToListAsync();

        return Result<List<ActivityDto>>.Success(activities);
      }
    }
  }
}