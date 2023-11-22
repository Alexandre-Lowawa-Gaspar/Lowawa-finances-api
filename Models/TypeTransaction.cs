using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Models
{
    public enum TypeTransaction
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Receita = 1,
        Despesa = 2
    }
}