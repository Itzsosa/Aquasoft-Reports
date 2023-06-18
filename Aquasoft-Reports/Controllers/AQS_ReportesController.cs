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
    public class AQS_ReportesController : Controller
    {
        private readonly Contexto _context;

        public AQS_ReportesController(Contexto context)
        {
            _context = context;
        }

        // GET: AQS_Reportes
        public async Task<IActionResult> Index()
        {
            var reportes = await _context.AQS_Reportes
            .OrderByDescending(r => r.FechaPublicacion)
            .ToListAsync();

            return View(reportes);
        }

        // GET: AQS_Reportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AQS_Reportes == null)
            {
                return NotFound();
            }

            var AQS_Reportes = await _context.AQS_Reportes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AQS_Reportes == null)
            {
                return NotFound();
            }

            return View(AQS_Reportes);
        }

        // GET: AQS_Reportes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AQS_Reportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AQS_Reportes AQS_Reportes, IFormFile fotoInput)
        {
            if (fotoInput != null && fotoInput.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await fotoInput.CopyToAsync(ms);
                    AQS_Reportes.Fotografia = ms.ToArray();
                }
            }
            AQS_Reportes.FechaPublicacion = DateTime.Now;
            _context.Add(AQS_Reportes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AQS_Reportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AQS_Reportes == null)
            {
                return NotFound();
            }

            var AQS_Reportes = await _context.AQS_Reportes.FindAsync(id);
            if (AQS_Reportes == null)
            {
                return NotFound();
            }
            return View(AQS_Reportes);
        }

        // POST: AQS_Reportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaPublicacion,Fotografia,Autor")] AQS_Reportes AQS_Reportes, IFormFile nuevaFoto)
        {
            if (id != AQS_Reportes.Id)
            {
                return NotFound();
            }

            // Obtener el objeto original de la base de datos
            var originalAQS_Reportes = await _context.AQS_Reportes.FindAsync(id);
            if (originalAQS_Reportes == null)
            {
                return NotFound();
            }

            // Actualizar solo los campos 'Titulo' y 'Descripcion'
            originalAQS_Reportes.Titulo = AQS_Reportes.Titulo;
            originalAQS_Reportes.Descripcion = AQS_Reportes.Descripcion;
            originalAQS_Reportes.Autor = AQS_Reportes.Autor;

            // Verificar si se ha proporcionado una nueva foto
            if (nuevaFoto != null && nuevaFoto.Length > 0)
            {
                // Leer el contenido de la nueva foto y asignarlo al campo 'Fotografia'
                using (var memoryStream = new MemoryStream())
                {
                    await nuevaFoto.CopyToAsync(memoryStream);
                    originalAQS_Reportes.Fotografia = memoryStream.ToArray();
                }
            }

            try
            {
                _context.Update(originalAQS_Reportes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AQS_ReportesExists(originalAQS_Reportes.Id))
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


        // GET: AQS_Reportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AQS_Reportes == null)
            {
                return NotFound();
            }

            var AQS_Reportes = await _context.AQS_Reportes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AQS_Reportes == null)
            {
                return NotFound();
            }

            return View(AQS_Reportes);
        }

        // POST: AQS_Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AQS_Reportes == null)
            {
                return Problem("Entity set 'Contexto.AQS_Reportes'  is null.");
            }
            var AQS_Reportes = await _context.AQS_Reportes.FindAsync(id);
            if (AQS_Reportes != null)
            {
                _context.AQS_Reportes.Remove(AQS_Reportes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AQS_ReportesExists(int id)
        {
          return (_context.AQS_Reportes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
