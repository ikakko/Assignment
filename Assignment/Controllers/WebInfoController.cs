using Application.Common.Responses;
using Application.CQRS.Command;
using Application.CQRS.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebInfoController : ApiControllerBase
    {
        #region CRUD

        [HttpGet]
        public async Task<object> Get()
        {
            return await Mediator.Send(new GetWebSiteInformationQuery());
        }

        [HttpGet("{id}")]
        public async Task<object> GetById(int id)
        {
            return await Mediator.Send(new GetWebSiteInformationQuery { Id = id });
        }

        [HttpPost]
        public async Task<Result<string>> Post(WebSiteInformation webInfo)
        {
            return await Mediator.Send(new AddWebSiteInformationCommand { WebInfo = webInfo });
        }

        [HttpPut]
        public async Task<Result<WebSiteInformation>> Put(int id, WebSiteInformation webInfo)
        {
            return await Mediator.Send(new EditWebSiteInformationCommand { Id = id, WebInfo = webInfo });
        }

        [HttpDelete]
        public async Task<Result<string>> Delete(int id)
        {
            return await Mediator.Send(new DeleteWebSiteInformationCommand { Id = id });
        }

        #endregion

        [HttpGet("{url}/{dateTime}")]
        public async Task<Result<WebSiteInformation>> GetWebSiteInformationAtSpecificTime(string url, DateTime dateTime)
        {
            return await Mediator.Send(new GetWebsiteInformationAtSpecificTimeQuery { Url = url, Date = dateTime });
        }
    }
}
