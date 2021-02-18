using AutoMapper;
using Domain;

namespace Application.Core.Mapping
{
  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<Activity, Activity>();
    }
  }
}