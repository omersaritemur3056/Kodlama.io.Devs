using Application.Features.ProgrammingTechnologies.Models;
using Application.Features.ProgrammingTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology
{
    public class GetListProgrammingTechnologyQuery : IRequest<ProgrammingTechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListProgrammingTechnologyQueryHnadler : IRequestHandler<GetListProgrammingTechnologyQuery, ProgrammingTechnologyListModel>
        {
            private readonly IMapper mapper;
            private readonly IProgrammingTechnologyRepository programmingTechnologyRepository;
            //private readonly ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules;

            public GetListProgrammingTechnologyQueryHnadler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                this.mapper = mapper;
                this.programmingTechnologyRepository = programmingTechnologyRepository;
                //this.programmingTechnologyBusinessRules = programmingTechnologyBusinessRules;
            }

            public async Task<ProgrammingTechnologyListModel> Handle(GetListProgrammingTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingTechnology> programmingTechnologies = await
                    programmingTechnologyRepository.GetListAsync(
                        include: c => c.Include(c => c.ProgrammingLanguage),
                        index: request.PageRequest.Page,
                        size: request.PageRequest.PageSize);
                //Businessrules...
                ProgrammingTechnologyListModel mappedProgrammingTechnologyListModel =
                    mapper.Map<ProgrammingTechnologyListModel>(programmingTechnologies);
                return mappedProgrammingTechnologyListModel;
            }
        }
    }
}
