using Application.Features.Basket;
using Application.Features.MilitaryJets;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MilitaryJet, MilitaryJetDto>();
            CreateMap<Domain.Entities.Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}
