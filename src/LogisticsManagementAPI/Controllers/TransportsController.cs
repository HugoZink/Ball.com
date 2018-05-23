using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogisticsManagementAPI.Commands;
using LogisticsManagementAPI.DataAccess;
using LogisticsManagementAPI.Events;
using LogisticsManagementAPI.Models;
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
        [HttpGet]
        [Route("{transportId}", Name = "GetTransportById")]
        public async Task<IActionResult> GetAsync(string transportId)
        {
            var transport = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == transportId);

            if (transport != null)
            {
                return Ok(transport);
            }

            return NotFound();
        }

        // POST api/transports
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterTransport command)
        {
            if (ModelState.IsValid)
            {
                // Insert Transport
                Transport transport = Mapper.Map<Transport>(command);
                _dbContext.Transports.Add(transport);
                await _dbContext.SaveChangesAsync();

                // Send Event
                TransportRegistered e = Mapper.Map<TransportRegistered>(command);
                await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                return CreatedAtRoute("GetTransportById", new { transportId = transport.TransportId }, transport);
            }

            return BadRequest();
        }

        // PUT api/transports/5
        [HttpPut("{transportId}")]
        public async Task<IActionResult> PutAsync(string transportId, [FromBody] UpdateTransport command)
        {
            if (ModelState.IsValid)
            {
                //Transport transport = Mapper.Map<Transport>(command);
                var transport = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == transportId);

                if (transport != null)
                {
                    // Update Transport
                    transport.CompanyName = command.CompanyName;
                    transport.TypeOfShipment = command.TypeOfShipment;
                    transport.CityOfDestination = command.CityOfDestination;
                    transport.Description = command.Description;
                    transport.WeightInKgMax = command.WeightInKgMax;
                    transport.ShippingCost = command.ShippingCost;
                    await _dbContext.SaveChangesAsync();

                    // Send Event
                    UpdateTransport c = Mapper.Map<UpdateTransport>(transport);
                    TransportUpdated e = Mapper.Map<TransportUpdated>(c);
                    await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                    return CreatedAtRoute("GetTransportById", new { transportId = transport.TransportId }, transport);
                }

                return NotFound();
            }

            return BadRequest();
        }

        // DELETE api/transports/5
        [HttpDelete("{transportId}")]
        public async Task<IActionResult> DeleteAsync(string transportId)
        {
            var transport = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == transportId);

            if (transport != null)
            {
                // Delete Transport
                _dbContext.Transports.Attach(transport);
                _dbContext.Transports.Remove(transport);
                await _dbContext.SaveChangesAsync();

                // Send Event
                RemoveTransport c = Mapper.Map<RemoveTransport>(transport);
                TransportRemoved e = Mapper.Map<TransportRemoved>(c);
                await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                return CreatedAtRoute("GetTransportById", new { transportId = transport.TransportId }, transport);
            }

            return NotFound();
        }
    }
}
