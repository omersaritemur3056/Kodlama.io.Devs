using Application.Features.SocialMedias.DTOs;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Models
{
    public class GetListSocialMediaModel : BasePageableModel
    {
        public List<GetListSocialMediaDto> Items { get; set; }
    }
}
