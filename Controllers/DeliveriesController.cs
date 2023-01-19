using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarMarket.Models;

namespace CarMarket.Controllers
{
   /* [Route("api/[controller]/[action]")]
    [ApiController]*/
    public class DeliveriesController : Controller
    {
        private readonly CarDatabaseContext _context;

        public DeliveriesController(CarDatabaseContext context)
        {
            _context = context;
        }

        // GET: Deliveries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delivery.ToListAsync());
        }

        // GET: Deliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery
                .FirstOrDefaultAsync(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryId,Provider,Amount,Price,Date")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        [HttpPost]
        public JsonResult TestCreate([Bind("DeliveryId,Provider,Amount,Price,Date")] Delivery delivery)
        {
            _context.Add(delivery);
            _context.SaveChangesAsync();
            return Json(new { success = true, result = delivery });
        }

        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryId,Provider,Amount,Price,Date")] Delivery delivery)
        {
            if (id != delivery.DeliveryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.DeliveryId))
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
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery
                .FirstOrDefaultAsync(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Delivery.FindAsync(id);
            _context.Delivery.Remove(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Delivery.Any(e => e.DeliveryId == id);
        }

        [HttpGet]
        public List<Delivery> GetDeliveries()
        {
            return _context.Delivery.ToList();
        }

        [HttpPost]
        public Delivery GetDeliveries(int id)
        {
            return _context.Delivery.Find(id);
        }
    }
}
