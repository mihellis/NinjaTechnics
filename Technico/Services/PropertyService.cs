﻿using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;

namespace Technico.Services;

public class PropertyService 
{
    private readonly PropertyRepository _propertyRepository;

    public PropertyService(PropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PropertyDTO?> CreateAsync(PropertyDTO propertyDTO)
    {
        Console.WriteLine($"Input PropertyDTO - Address: {propertyDTO.Address}, OwnerID: {propertyDTO.OwnerID}");

        var properties = await _propertyRepository.GetAllAsync();
        bool PropertyExists = properties.Any(u => u.Address == propertyDTO.Address);
        if (PropertyExists) return null;

        var property = new Property
        {
            Address = propertyDTO.Address,
            YearOfConstruction = propertyDTO.YearOfConstruction,
            OwnerID = propertyDTO.OwnerID
        };

        Console.WriteLine($"Created Property Object - Address: {property.Address}, OwnerID: {property.OwnerID}");

        var result = await _propertyRepository.CreateAsync(property);
        return result == null ? null : new PropertyDTO
        {
            PropertyIDNumber = result.PropertyIDNumber,
            Address = result.Address,
            YearOfConstruction = result.YearOfConstruction,
            OwnerID = result.OwnerID
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _propertyRepository.DeleteAsync(id);
    }

    public async Task<List<PropertyDTO?>> GetAllAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();
        var propertyDTOs = properties.Select(property => new PropertyDTO
        {
            PropertyIDNumber = property.PropertyIDNumber,
            Address = property.Address,
            YearOfConstruction = property.YearOfConstruction,
            OwnerID = property.OwnerID
        }).ToList();
        return propertyDTOs;
    }

    public async Task<PropertyResponseDTO?> GetAsync(Guid id)
    {
        var property = await _propertyRepository.GetAsync(id);
        if (property == null) return null;

        var propertyDTO = new PropertyResponseDTO
        {
            PropertyIDNumber = property.PropertyIDNumber,
            Address = property.Address,
            YearOfConstruction = property.YearOfConstruction,
            OwnerID = property.OwnerID,

            Repairs = property.Repairs.Select(repair => new RepairDTO
            {
                Id = repair.Id,
                ScheduledDate = repair.ScheduledDate,
                Address = repair.Address,
                Type = repair.Type,
                Description = repair.Description,
                Cost = repair.Cost,
                PropertyId = property.PropertyIDNumber,
            }).ToList()
        };

        return propertyDTO;//

    }

    public async Task<PropertyDTO?> UpdateAsync(PropertyDTO propertyDTO)
    {
        var existingProperty = await _propertyRepository.GetAsync(propertyDTO.PropertyIDNumber);
        if (existingProperty == null) return null;

        var properties = await _propertyRepository.GetAllAsync();
        //var existingAdress = properties.Single(x => x.Address == propertyDTO.Address);

        //if (existingAdress != null) return null;

        existingProperty.Address = propertyDTO.Address;
        existingProperty.YearOfConstruction = propertyDTO.YearOfConstruction;

        var result = await _propertyRepository.UpdateAsync(existingProperty);

        return new PropertyDTO
        {
            PropertyIDNumber = result.PropertyIDNumber,
            Address = result.Address,
            OwnerID = result.OwnerID,
            YearOfConstruction= result.YearOfConstruction,
        };
    }
}
