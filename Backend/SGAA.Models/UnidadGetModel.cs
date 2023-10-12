﻿namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class UnidadGetModel : IGetModel<Unidad>
    {
        public required int Id { get; set; }
    }
}
