using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Entities;
using おマチ.API.Models.Activity;
using おマチ.API.Models.User;

namespace おマチ.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticationResponse
            CreateMap<User, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(String) && String.IsNullOrEmpty((String)prop)) return false;

                        return true;
                    }
                ));

            CreateMap<ActivityAddRequest, Activity>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(String) && String.IsNullOrEmpty((String)prop)) return false;

                        return true;
                    }
                ));

            CreateMap<ActivityUpdateRequest, Activity>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(String) && String.IsNullOrEmpty((String)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
