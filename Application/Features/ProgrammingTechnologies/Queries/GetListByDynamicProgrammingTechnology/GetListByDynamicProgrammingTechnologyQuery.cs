using Application.Features.ProgrammingTechnologies.Models;
using Application.Features.ProgrammingTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Queries.GetListByDynamicProgrammingTechnology
{
    public class GetListByDynamicProgrammingTechnologyQuery : IRequest<ProgrammingTechnologyListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
        public string[] Roles { get; } = new string[] {"Admin" };

        public class GetListByDynamicProgrammingTechnologyQueryHandler : IRequestHandler<GetListByDynamicProgrammingTechnologyQuery, ProgrammingTechnologyListModel>
        {
            private readonly IMapper mapper;
            private readonly IProgrammingTechnologyRepository programmingTechnologyRepository;
            //private readonly ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules;

            public GetListByDynamicProgrammingTechnologyQueryHandler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                this.mapper = mapper;
                this.programmingTechnologyRepository = programmingTechnologyRepository;
                //this.programmingTechnologyBusinessRules = programmingTechnologyBusinessRules;
            }

            public async Task<ProgrammingTechnologyListModel> Handle(GetListByDynamicProgrammingTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingTechnology> programmingTechnologies = await
                    programmingTechnologyRepository.GetListByDynamicAsync(request.Dynamic,
                    include: c => c.Include(c => c.ProgrammingLanguage),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                //Businessrules...
                ProgrammingTechnologyListModel programmingTechnologyListModel = 
                    mapper.Map<ProgrammingTechnologyListModel>(programmingTechnologies);
                return programmingTechnologyListModel;
            }
        }
    }
}
