using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Commands
{
  public class EditActivity
  {
    public class EditActivityCommand : IRequest
    {
      public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<EditActivityCommand>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

      public async Task<Unit> Handle(EditActivityCommand request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities.FindAsync(request.Activity.Id);
        _mapper.Map(request.Activity, activity);

        await _context.SaveChangesAsync();
        return Unit.Value;
      }
    }
  }
}