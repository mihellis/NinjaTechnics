﻿using Microsoft.EntityFrameworkCore;
using Technico.Context;
using Technico.Models;

namespace Technico.Repositories;

public class RepairRepository
{
    private readonly TechnicoDBContext _dbContext;

    public RepairRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Repair?> CreateAsync(Repair repair)
    {
        _dbContext.Repairs.Add(repair);
        await _dbContext.SaveChangesAsync();
        return repair;
    }

    public async Task<bool> DeleteAsync(Guid repairId)
    {
        var repair = await GetAsync(repairId);
        if (repair == null)
            return false;

        _dbContext.Repairs.Remove(repair);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Repair>> GetAllAsync()
    {
        return await _dbContext.Repairs.ToListAsync();
    }

    public async Task<List<Repair>> GetRepairsByPropertyIdAsync(Guid propertyId)
    {
        return await _dbContext.Repairs
                               .Where(r => r.PropertyId == propertyId)
                               //.Include(r => r.Property) // Include Property if needed
                               .ToListAsync();
    }

    public async Task<Repair?> GetAsync(Guid repairId)
    {
        return await _dbContext.Repairs
                               .FirstOrDefaultAsync(r => r.Id == repairId);
    }

    public async Task<Repair?> UpdateAsync(Repair updatedRepair)
    {
        var repair = await GetAsync(updatedRepair.Id);
        if (repair == null)
        {
            return null;
        }
        repair.ScheduledDate = updatedRepair.ScheduledDate;
        repair.Description = updatedRepair.Description;
        repair.Address = updatedRepair.Address;
        repair.Cost = updatedRepair.Cost;
        repair.Type = updatedRepair.Type;
        repair.CurrentStatus = updatedRepair.CurrentStatus;

        _dbContext.Repairs.Update(repair);
        await _dbContext.SaveChangesAsync();
        return repair;
    }
}
