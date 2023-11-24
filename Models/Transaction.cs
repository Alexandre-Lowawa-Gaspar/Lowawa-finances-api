using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Lowawa_finances_api.Models
{
    public class Transaction
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