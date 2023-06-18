using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aquasoft_Reports.Data;
using Aquasoft_Reports.Models;

namespace Aquasoft_Reports.Controllers
{
    public class AQS_EventosController : Controller
    {
        private readonly Contexto _context;

        public AQS_EventosController(Contexto context)
        {
            _context = context;
        }

        // GET: AQS_Eventos
        public async Task<IActionResult> Index()
        {
            var reportes = await _context.AQS_Eventos
            .OrderByDescending(r => r.FechaEvento)
            .ToListAsync();

            return View(reportes);
        }

        // GET: AQS_Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AQS_Eventos == null)
            {
                return NotFound();
            }

            var aQS_Eventos = await _context.AQS_Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aQS_Eventos == null)
            {
                return NotFound();
            }

            return View(aQS_Eventos);
        }

        // GET: AQS_Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AQS_Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,FechaEvento,FotoEvento,Estado")] AQS_Eventos aQS_Eventos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aQS_Eventos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aQS_Eventos);
        }

        // GET: AQS_Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AQS_Eventos == null)
            {
                return NotFound();
            }

            var aQS_Eventos = await _context.AQS_Eventos.FindAsync(id);
            if (aQS_Eventos == null)
            {
                return NotFound();
            }
            return View(aQS_Eventos);
        }

        // POST: AQS_Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaEvento,FotoEvento,Estado")] AQS_Eventos aQS_Eventos)
        {
            if (id != aQS_Eventos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aQS_Eventos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AQS_EventosExists(aQS_Eventos.Id))
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
            return View(aQS_Eventos);
        }

        // GET: AQS_Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AQS_Eventos == null)
            {
                return NotFound();
            }

            var aQS_Eventos = await _context.AQS_Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aQS_Eventos == null)
            {
                return NotFound();
            }

            return View(aQS_Eventos);
        }

        // POST: AQS_Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AQS_Eventos == null)
            {
                return Problem("Entity set 'Contexto.AQS_Eventos'  is null.");
            }
            var aQS_Eventos = await _context.AQS_Eventos.FindAsync(id);
            if (aQS_Eventos != null)
            {
                _context.AQS_Eventos.Remove(aQS_Eventos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AQS_EventosExists(int id)
        {
          return (_context.AQS_Eventos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
