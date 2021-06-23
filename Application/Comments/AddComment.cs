using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using reactivitiesV2.Domain;

namespace Application.Comments
{
  public class AddComment
  {
    public class AddCommentCommand : IRequest<Result<CommentDto>>
    {
      public string Body { get; set; }
      public Guid ActivityId { get; set; }
    }

    public class CommandValidator : AbstractValidator<AddCommentCommand>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Body)
        .NotEmpty();
      }
    }

    public class Handler : IRequestHandler<AddCommentCommand, Result<CommentDto>>
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

      public async Task<Result<CommentDto>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities.FindAsync(request.ActivityId);

        if (activity == null) return null;

        var user = await _context.Users
                                .Include(p => p.Photos)
                                .SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

        var comment = new Comment
        {
          Author = user,
          Activity = activity,
          Body = request.Body
        };

        activity.Comments.Add(comment);

        var succeess = await _context.SaveChangesAsync() > 0;

        if (succeess) return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));

        return Result<CommentDto>.Failure("Failed to add comment");
      }
    }
  }
}