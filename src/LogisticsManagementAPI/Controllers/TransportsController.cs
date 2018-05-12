using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsManagementAPI.Commands;
using LogisticsManagementAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;

namespace LogisticsManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class TransportsController : Controller
    {
        IMessagePublisher _messagePublisher;
        LogisticsManagementDbContext _dbContext;

        public TransportsController(LogisticsManagementDbContext dbContext, IMessagePublisher messagePublisher)
        {
            _dbContext = dbContext;
            _messagePublisher = messagePublisher;
        }

        // GET api/transports
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dbContext.Transports.ToListAsync());
        }

        // GET api/transports/5
        [HttpGet("{transportId}")]
        public async Task<IActionResult> GetAsync(string transportId)
        {
            var transport = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == transportId);

            if (transport == null)
            {
                return NotFound();
            }

            return Ok(transport);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterTransport command)
        {
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
