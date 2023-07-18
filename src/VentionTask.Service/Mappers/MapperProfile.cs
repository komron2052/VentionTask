using AutoMapper;
using Ventiontask.Domain.Entities;
using Ventiontask.Service.DTOs;

namespace Ventiontask.Service.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<User, UserCreationDto>().ReverseMap();
		CreateMap<User, UserResultDto>().ReverseMap();
		CreateMap<User, UserUpdateDto>().ReverseMap();

		CreateMap<Group, GroupCreationDto>().ReverseMap();
		CreateMap<Group, GroupResultDto>().ReverseMap();

		CreateMap<Subject, SubjectCreationDto>().ReverseMap();
		CreateMap<Subject, SubjectResultDto>().ReverseMap();

		CreateMap<GroupSubject, GroupSubjectCreationDto>().ReverseMap();
		CreateMap<GroupSubject, GroupSubjectResultDto>().ReverseMap();
	}
}
