﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website_Selling_Computer.DataAccess;
using Website_Selling_Computer.Models;
using Website_Selling_Computer.Repositories.Interfaces;

namespace Website_Selling_Computer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
    public class ManufacturerManagementController : Controller
    {
        private readonly IManufacturer _manufacturerRepo;
        public ManufacturerManagementController(IManufacturer manufacturerRepo)
        {
            _manufacturerRepo = manufacturerRepo;
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/manufacturerPictures", image.FileName); //

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/manufacturerPictures/" + image.FileName;
        }
        public async Task<IActionResult> Index()
        {
            var manufacturers = await _manufacturerRepo.GetAllAsync();
            return View(manufacturers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    manufacturer.ManufacturerImage = await SaveImage(imageUrl);
                }
                await _manufacturerRepo.AddAsync(manufacturer);
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        // GET: Admin/ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _manufacturerRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Manufacturer manufacturer, IFormFile ManufacturerImage)
        {
            if (id != manufacturer.ManufacturerID)
            {
                return NotFound();
            }

              //  var existingManufacturer = await _manufacturerRepo.GetByIdAsync(id);

                if (ManufacturerImage != null)
                {
                    manufacturer.ManufacturerImage = await SaveImage(ManufacturerImage);
                }
  
                await _manufacturerRepo.UpdateAsync(manufacturer);
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _manufacturerRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _manufacturerRepo.GetByIdAsync(id);
            if (category != null)
            {
                await _manufacturerRepo.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
