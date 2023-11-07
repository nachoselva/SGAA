﻿namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class CancelarContratoPutModel : IPutModel<Contrato>, IPutModel<Unidad>
    {
        public DateOnly FechaCancelacion { get; set; }
    }
}
