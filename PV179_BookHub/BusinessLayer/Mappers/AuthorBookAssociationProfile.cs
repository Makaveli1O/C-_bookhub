using AutoMapper;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Mappers;

public class AuthorBookAssociationProfile : Profile
{
    public AuthorBookAssociationProfile()
    {
        CreateMap<Tuple<long, long>, AuthorBookAssociation>()
            .ForMember(x => x.AuthorId, opt => opt.MapFrom(y => y.Item1))
            .ForMember(x => x.BookId, opt => opt.MapFrom(y => y.Item2));
    }
}
