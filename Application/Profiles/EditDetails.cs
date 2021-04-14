using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
  public class EditDetails
  {
    public class EditDetailsCommand : IRequest<Result<Unit>>
    {
      public string DisplayName { get; set; }
      public string Bio { get; set; }
    }
    public class Handler : IRequestHandler<EditDetailsCommand, Result<Unit>>
    {
      private readonly DataContext _context;
      private readonly IUserAccessor _userAccessor;
      public Handler(DataContext context, IUserAccessor userAccessor)
      {
        _userAccessor = userAccessor;
        _context = context;
      }

      public async Task<Result<Unit>> Handle(EditDetailsCommand request, CancellationToken cancellationToken)
      {
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

        userToUpdate.Bio = request.Bio ?? userToUpdate.Bio;
        userToUpdate.DisplayName = request.DisplayName ?? userToUpdate.DisplayName;

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return Result<Unit>.Failure("Failed to update user");
        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}