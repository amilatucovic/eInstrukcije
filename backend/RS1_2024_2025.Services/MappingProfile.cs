using AutoMapper;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Requests;

namespace RS1_2024_2025.Services
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<StudentInsertRequest, MyAppUser>();
			CreateMap<StudentUpdateRequest, MyAppUser>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
			
			CreateMap<StudentInsertRequest, Student>();
			CreateMap<StudentUpdateRequest, Student>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
		}
	}
}
