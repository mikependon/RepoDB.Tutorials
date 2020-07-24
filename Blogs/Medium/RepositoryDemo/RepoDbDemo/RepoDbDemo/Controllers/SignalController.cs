using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepoDbDemo.Interfaces;
using RepoDbDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoDbDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignalController : ControllerBase
    {
        private readonly ILogger<SignalController> logger;
        private readonly ISignalRepository signalRepository;
        private readonly ISignalSourceRepository signalSourceRepository;
        private readonly ISignalTypeRepository signalTypeRepository;

        public SignalController(ILogger<SignalController> logger,
            ISignalRepository signalRepository,
            ISignalSourceRepository signalSourceRepository,
            ISignalTypeRepository signalTypeRepository)
        {
            this.logger = logger;
            this.signalRepository = signalRepository;
            this.signalSourceRepository = signalSourceRepository;
            this.signalTypeRepository = signalTypeRepository;
        }

        [HttpGet]
        public IEnumerable<DTO.Signal> GetAll()
        {
            var signals = (signalRepository.GetAllAsync()).Result;
            var sources = (signalSourceRepository.GetAllAsync()).Result;
            var types = (signalTypeRepository.GetAllAsync()).Result;
            return signals?.Select(signal => new DTO.Signal
            {
                Id = signal.Id,
                Value = signal.Value,
                Source = sources.FirstOrDefault(source => source.Id == signal.SignalSourceId)?.Name ?? "N/A",
                Type = types.FirstOrDefault(type => type.Id == signal.SignalTypeId)?.Name ?? "N/A"
            });
        }

        [HttpGet("{id}")]
        public Signal Get(int id)
        {
            return (signalRepository.GetAsync(id)).Result;
        }

        [HttpPost("save")]
        public int Save([FromBody] Signal signal)
        {
            return (signalRepository.SaveAsync(signal)).Result;
        }

        [HttpPost("saveall")]
        public int SaveAll([FromBody] IEnumerable<Signal> signals)
        {
            return (signalRepository.SaveAllAsync(signals)).Result;
        }

        [HttpPost("savemany")]
        public int SaveMany([FromBody] int count)
        {
            var signals = new List<Signal>();
            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                signals.Add(new Signal
                {
                    SignalSourceId = random.Next(1, 6),
                    SignalTypeId = random.Next(1, 7),
                    Value = (decimal)(random.NextDouble() * int.MaxValue)
                });
            }
            return (signalRepository.SaveAllAsync(signals)).Result;
        }
    }
}
