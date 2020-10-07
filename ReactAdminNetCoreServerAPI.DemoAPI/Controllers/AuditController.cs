using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminNetCoreServerAPI.DemoAPI.Data;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.Common.Definitions;
using ReactAdminNetCoreServerAPI.Common.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Controllers
{
    /// <summary>
    /// This controller demonstrates:
    /// - use of EntityWithGuidIdController  
    /// - overriding the create method to augment the data.
    /// </summary>
    [Route("api/[controller]")]
    public class AuditController : EntityWithGuidIdController<Audit, AuditReadModel, AuditCreateModel, AuditUpdateModel>
    {
        public AuditController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }


        // example of overriding to adjust the inbound properties.
        [HttpPost("")]
        public override async Task<ActionResult<AuditReadModel>> Create(CancellationToken cancellationToken, 
            AuditCreateModel createModel)
        {
            createModel.Date = DateTime.Now;
            var readModel = await CreateModel(createModel, cancellationToken);

            return readModel;
        }

    }
}
