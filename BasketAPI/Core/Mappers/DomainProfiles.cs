using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Core.Mappers
{
    /// <summary>
    /// Creating Mappers
    /// </summary>
    public class DomainProfile : Profile
    {
        /// <summary>
        /// Mapping Data Models to DTO
        /// </summary>
        public DomainProfile()
        {
            CreateMap<Models.Data.Basket, Models.DTO.Basket>();
            CreateMap<Models.Data.BasketItem, Models.DTO.BasketItem>();
        }
    }
}
