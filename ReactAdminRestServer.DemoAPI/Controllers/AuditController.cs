using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminRestServer.DemoAPI.Data;
using ReactAdminRestServer.DemoAPI.Domain.Models;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.Definitions;
using ReactAdminRestServer.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReactAdminRestServer.DemoAPI.Controllers
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
