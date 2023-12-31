using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Transaction,GetTransactionDto>();
            CreateMap<AddTransactionDto,Transaction>();
            CreateMap<UpdateTransactionDto,Transaction>();
        }
    }
}