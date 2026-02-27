using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanosSaude.API.DTOs.Planos;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Mappings
{
    public static class PlanoMapping
    {
        public static PlanoResponseDto ToResponseDto(this Plano plano)
        {
            return new PlanoResponseDto(
                plano.Id,
                plano.Nome,
                plano.Codigo,
                plano.CustoMensal,
                plano.Cobertura
            );
        }
    }
}