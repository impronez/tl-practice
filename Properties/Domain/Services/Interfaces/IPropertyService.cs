using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Domain.Services.Interfaces;

public interface IPropertyService
{
    Task<Result<Property>> CreateAsync(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude);

    Task<List<Property>> GetAllAsync();

    Task<Property?> GetByIdAsync(Guid id);

    Task<Result> UpdateAsync(
        Guid id,
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude);

    Task<Result> DeleteAsync(Guid id);
}