﻿namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UnidadController : ControllerBase
    {
        private readonly IUnidadService _unidadService;

        public UnidadController(IUnidadService unidadService)
        {
            _unidadService = unidadService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolType.Propietario))]
        public async Task<ActionResult<UnidadGetModel>> AddUnidad([FromBody] UnidadPostModel model)
        {
            UnidadGetModel unidad = await _unidadService.AddUnidad(model);
            return CreatedAtAction(nameof(GetUnidad), new { unidadId = unidad.Id }, model);
        }

        [HttpGet]
        [Route("{unidadId}")]
        [Authorize(Roles = $"{nameof(RolType.Propietario)},{nameof(RolType.Administrador)}")]
        public async Task<UnidadGetModel> GetUnidad([FromRoute] int unidadId)
            => await _unidadService.GetUnidad(unidadId);
    }
}
