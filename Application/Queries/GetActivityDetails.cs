using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries
{
  public class GetActivityDetails
  {
    public class ActivityDetailsQuery : IRequest<Result<ActivityDto>>
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<ActivityDetailsQuery, Result<ActivityDto>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      private readonly IUserAccessor _userAccessor;
      public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
      {
        _userAccessor = userAccessor;
        _mapper = mapper;
        _context = context;
      }

      public async Task<Result<ActivityDto>> Handle(ActivityDetailsQuery request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities.ProjectTo<ActivityDto>(_mapper.ConfigurationProvider, new {currentUsername = _userAccessor.GetUsername()}).FirstOrDefaultAsync(x => x.Id == request.Id);

        return Result<ActivityDto>.Success(activity);
      }
    }
  }
}