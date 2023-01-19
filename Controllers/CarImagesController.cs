using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarMarket.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CarMarket.Controllers
{
    public class CarImagesController : Controller
    {
        private readonly CarDatabaseContext _context;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public CarImagesController(CarDatabaseContext context, IWebHostEnvironment iWebHostEnvironment)
        {
            _context = context;
            _iWebHostEnvironment = iWebHostEnvironment;
        }

        // GET: CarImages
        public async Task<IActionResult> Index()
        {
            var carDatabaseContext = _context.CarImages.Include(c => c.ImageNavigation);
            return View(await carDatabaseContext.ToListAsync());
        }

        // GET: CarImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImages = await _context.CarImages
                .Include(c => c.ImageNavigation)
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (carImages == null)
            {
                return NotFound();
            }

            return View(carImages);
        }

        // GET: CarImages/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Car, "CarId", "Model");
            return View();
        }

        // POST: CarImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,CarId,File")] CarImages carImages)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _iWebHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(carImages.File.FileName);
                string extension = Path.GetExtension(carImages.File.FileName);
                fileName += DateTime.Now.ToString("yymmssffff") + extension;
                carImages.Image = fileName;
                string path = Path.Combine(rootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await carImages.File.CopyToAsync(fileStream);
                }

                _context.Add(carImages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Car, "CarId", "Brand", carImages.ImageId);
            return View(carImages);
        }

        // GET: CarImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImages = await _context.CarImages.FindAsync(id);
            if (carImages == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Car, "CarId", "Brand", carImages.ImageId);
            return View(carImages);
        }

        // POST: CarImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,CarId,File")] CarImages carImages)
        {
            if (id != carImages.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarImagesExists(carImages.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Car, "CarId", "Brand", carImages.ImageId);
            return View(carImages);
        }

        // GET: CarImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImages = await _context.CarImages
                .Include(c => c.ImageNavigation)
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (carImages == null)
            {
                return NotFound();
            }

            return View(carImages);
        }

        // POST: CarImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carImages = await _context.CarImages.FindAsync(id);

            var imagePath = Path.Combine(_iWebHostEnvironment.WebRootPath, "image", carImages.Image);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.CarImages.Remove(carImages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarImagesExists(int id)
        {
            return _context.CarImages.Any(e => e.ImageId == id);
        }
    }
}
