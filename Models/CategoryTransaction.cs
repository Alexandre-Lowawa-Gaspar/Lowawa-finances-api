using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Models
{
    public enum CategoryTransaction
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Salario = 1,
        Transporte = 2,
        Saude = 3,
        Moradia = 4,
        Alimentacao = 5
    }
}