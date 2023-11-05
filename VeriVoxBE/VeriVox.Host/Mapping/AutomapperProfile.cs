using AutoMapper;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Host.Mapping
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Form, FormDto>().ReverseMap();
            CreateMap<ModifyFormDto, Form>().ReverseMap();
            CreateMap<FormQuestion, FormQuestionDto>().ReverseMap();
            CreateMap<ModifyFormQuestionDto, FormQuestion>().ReverseMap();
            CreateMap<ModifyQuestionOptionDto, QuestionOption>().ReverseMap();
            CreateMap<QuestionOption,QuestionOptionsDto>().ReverseMap();
            CreateMap<ActiveStateDto, Form>().ReverseMap();
            CreateMap<CompanyDto, Companies>().ReverseMap();
            CreateMap<ProductDto, Products>().ReverseMap();
            CreateMap<CompanyUpdateDto, Companies>().ReverseMap();
            CreateMap<ProductUpdateDto, Products>().ReverseMap();
            CreateMap<UserAddDto, User>().ReverseMap();
            CreateMap<UserGetDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();
            CreateMap<LinkDto, Link>().ReverseMap();
            CreateMap<ModifyLinkDto, Link>().ReverseMap();
        }
    }
}
