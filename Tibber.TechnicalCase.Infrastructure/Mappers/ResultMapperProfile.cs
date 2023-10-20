using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Infrastructure.Entities;

namespace Tibber.TechnicalCase.Infrastructure.Mappers;

internal class ResultMapperProfile : Profile
{
    public ResultMapperProfile()
    {
        CreateMap<ResultEntity, ResultDto>().ReverseMap();
    }
}
