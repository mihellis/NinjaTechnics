using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Net;
using Technico.Dtos;
using Technico.Models;
using Technico.Repositories;

namespace Technico.Services;

public class RepairService
{
    private readonly RepairRepository _repairRepository;

    public RepairService(RepairRepository repairRepository)
    {
        _repairRepository = repairRepository;
    }

    public async Task<Repair?> CreateAsync(RepairDTO repairDTO)
    {
        var repair = new Repair
        {
            Id = repairDTO.Id,
            ScheduledDate = repairDTO.ScheduledDate,
            Type = repairDTO.Type,
            Description = repairDTO.Description,
            Address = repairDTO.Address,
            Cost = repairDTO.Cost,
            PropertyId = repairDTO.PropertyId,
        };

        await _repairRepository.CreateAsync(repair);
        return repair;
    }

    public async Task<bool> DeleteAsync(Guid repairId)
    {
        return await _repairRepository.DeleteAsync(repairId);
    }

    public async Task<List<Repair?>> GetAllAsync()
    {
        return await _repairRepository.GetAllAsync();
    }

    public async Task<List<Repair?>> GetRepairsByPropertyIdAsync(Guid propertyId)
    {
        return await _repairRepository.GetRepairsByPropertyIdAsync(propertyId);
    }

    public async Task<Repair?> GetAsync(Guid repairId)
    {
        return await _repairRepository.GetAsync(repairId);
    }

    public async Task<Repair?> UpdateAsync(RepairDTO repairDTO)
    {
        var repair = new Repair
        {
            Id = repairDTO.Id,
            ScheduledDate = repairDTO.ScheduledDate,
            Type = repairDTO.Type,
            CurrentStatus = repairDTO.CurrentStatus,
            Description = repairDTO.Description,
            Address = repairDTO.Address,
            Cost = repairDTO.Cost,
            PropertyId = repairDTO.PropertyId,
        };
        return await _repairRepository.UpdateAsync(repair);
    }
}
