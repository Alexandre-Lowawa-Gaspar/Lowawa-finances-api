using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Dto.TransationDto
{
    public class GetTransactionDto
    {
        public int Id { get; set; }
        public int UserId {get;set;}
        public string? Description{get;set;}
        public decimal Amount { get; set; }
        public TypeTransaction Type { get; set; } = TypeTransaction.Receita;
        public DateTime Date { get; set; }
        public CategoryTransaction Category{get;set;} = CategoryTransaction.Salario;
    }
}