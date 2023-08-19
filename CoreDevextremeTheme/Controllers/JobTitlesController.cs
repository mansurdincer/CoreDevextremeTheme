﻿namespace CoreDevextremeTheme.Controllers;

[Authorize(Roles = "Admin, HR")]
public class JobTitlesController : BaseController
{
    private readonly ApplicationDbContext _context;
    
    public JobTitlesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: JobTitles
    public async Task<IActionResult> Index()
    {
        return _context.JobTitles != null ?
                    View(await _context.JobTitles.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.JobTitles'  is null.");
    }

    // GET: JobTitles/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.JobTitles == null)
        {
            return NotFound();
        }

        var jobTitle = await _context.JobTitles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (jobTitle == null)
        {
            return NotFound();
        }

        return View(jobTitle);
    }

    // GET: JobTitles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: JobTitles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(JobTitle jobTitle)
    {
        if (ModelState.IsValid)
        {
            jobTitle.Id = Guid.NewGuid();

            _context.Add(jobTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(jobTitle);
    }

    // GET: JobTitles/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.JobTitles == null)
        {
            return NotFound();
        }

        var jobTitle = await _context.JobTitles.FindAsync(id);
        if (jobTitle == null)
        {
            return NotFound();
        }
        return View(jobTitle);
    }

    // POST: JobTitles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] JobTitle jobTitle)
    {
        if (id != jobTitle.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(jobTitle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTitleExists(jobTitle.Id))
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
        return View(jobTitle);
    }

    // GET: JobTitles/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.JobTitles == null)
        {
            return NotFound();
        }

        var jobTitle = await _context.JobTitles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (jobTitle == null)
        {
            return NotFound();
        }

        return View(jobTitle);
    }

    // POST: JobTitles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.JobTitles == null)
        {
            return Problem("Entity set 'ApplicationDbContext.JobTitles'  is null.");
        }
        var jobTitle = await _context.JobTitles.FindAsync(id);
        if (jobTitle != null)
        {
            _context.JobTitles.Remove(jobTitle);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool JobTitleExists(Guid id)
    {
        return (_context.JobTitles?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
