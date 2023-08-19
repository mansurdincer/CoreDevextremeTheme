namespace CoreDevextremeTheme.Controllers;

[Authorize(Roles = "Admin, HR")]
public class DepartmentsController : BaseController
{
    private readonly ApplicationDbContext _context;
    public DepartmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Departments
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Departments.Include(d => d.Parent);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Departments/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Departments == null)
        {
            return NotFound();
        }

        var department = await _context.Departments
            .Include(d => d.Parent)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    // GET: Departments/Create
    public IActionResult Create()
    {
        ViewData["ParentId"] = new SelectList(_context.Departments, "Id", "Name");
        return View();
    }

    // POST: Departments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid)
        {
            department.Id = Guid.NewGuid();
            _context.Add(department);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ParentId"] = new SelectList(_context.Departments, "Id", "Name", department.ParentId);
        return View(department);
    }

    // GET: Departments/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Departments == null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        ViewData["ParentId"] = new SelectList(_context.Departments, "Id", "Name", department.ParentId);
        return View(department);
    }

    // POST: Departments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Department department)
    {
        if (id != department.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(department.Id))
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
        ViewData["ParentId"] = new SelectList(_context.Departments, "Id", "Name", department.ParentId);
        return View(department);
    }

    // GET: Departments/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Departments == null)
        {
            return NotFound();
        }

        var department = await _context.Departments
            .Include(d => d.Parent)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Departments == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
        }
        var department = await _context.Departments.FindAsync(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DepartmentExists(Guid id)
    {
      return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
