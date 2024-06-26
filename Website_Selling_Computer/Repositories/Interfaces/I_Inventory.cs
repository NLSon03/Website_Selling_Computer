﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Website_Selling_Computer.Models;

namespace Website_Selling_Computer.Repositories.Interfaces
{
    public interface I_Inventory
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetByIdAsync(int id);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteAsync(int id);
        Task<ICollection<Product>> FindByNameAsync(string name);
        Task<ICollection<Product>> FindByCategoryAsync(int CategoryID);
        Task<ICollection<Product>> FindByManufacturerAsync(int ManufacturerID);
    }
}
