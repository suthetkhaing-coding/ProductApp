﻿using Microsoft.AspNetCore.Mvc;
using ProductApp.Data;
using ProductApp.Models;
using System.Reflection;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index() => View(await _repository.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);
            product.Id = Guid.NewGuid();
            await _repository.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id) => View(await _repository.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);
            product.ModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(Guid id) => View(await _repository.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
