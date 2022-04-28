using Application.Models.Activity;
using Application.Models.Faction;
using Application.Models.Role;
using Application.Models.User;
using AutoMapper;
using Domain;

namespace Application.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ActivityPostRequest, Activity>();
        CreateMap<ActivityUpdateRequest, Activity>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        CreateMap<FactionPostRequest, Faction>();
        CreateMap<FactionUpdateRequest, Faction>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        CreateMap<RolePostRequest, Role>();
        CreateMap<RoleUpdateRequest, Role>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // User -> AuthenticateResponse
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

        // UpdateRequest -> User
        CreateMap<UserUpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}
