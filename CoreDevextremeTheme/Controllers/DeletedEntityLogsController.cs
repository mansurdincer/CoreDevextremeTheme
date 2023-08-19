namespace CoreDevextremeTheme.Controllers;

[Authorize(Roles = "Admin")]
public class DeletedEntityLogsController : BaseController
{
    private readonly ApplicationDbContext _context;

    public DeletedEntityLogsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DeletedEntityLogs
    public async Task<IActionResult> Index()
    {
          return _context.DeletedEntityLogs != null ? 
                      View(await _context.DeletedEntityLogs.OrderByDescending(log => log.DeletedAt).ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.DeletedEntityLog'  is null.");
    }

    // GET: DeletedEntityLogs/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.DeletedEntityLogs == null)
        {
            return NotFound();
        }

        var deletedEntityLog = await _context.DeletedEntityLogs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (deletedEntityLog == null)
        {
            return NotFound();
        }

        return View(deletedEntityLog);
    }

    // GET: DeletedEntityLogs/Recover/5
    public async Task<IActionResult> Recover(Guid? id)
    {
        if (id == null || _context.DeletedEntityLogs == null)
        {
            return NotFound();
        }

        var deletedEntityLog = await _context.DeletedEntityLogs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (deletedEntityLog == null)
        {
            return NotFound();
        }

        return View(deletedEntityLog);
    }

    // POST: DeletedEntityLogs/Recover/5
    [HttpPost, ActionName("Recover")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RecoverConfirmed(Guid id)
    {
        if (_context.DeletedEntityLogs == null)
        {
            return Problem("Entity set 'ApplicationDbContext.DeletedEntityLog'  is null.");
        }
        var deletedEntityLog = await _context.DeletedEntityLogs.FindAsync(id);
        if (deletedEntityLog != null)
        {
            #region tried generic but did not work!
            //var entityType = Type.GetType(deletedEntityLog.EntityType);
            //var dbSetType = typeof(DbSet<>).MakeGenericType(entityType);
            //dynamic dbSet = Activator.CreateInstance(dbSetType, _context);
            //var entity = await dbSet.FindAsync(deletedEntityLog.EntityId);
            //if (entity != null)
            //{
            //    var isDeletedProp = entityType.GetProperty("IsDeleted");
            //    if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
            //    {
            //        isDeletedProp.SetValue(entity, false);

            //        _context.DeletedEntityLogs.Remove(deletedEntityLog);

            //        await _context.SaveChangesAsync();
            //    }
            //}
            #endregion 

            var entityType = Type.GetType(deletedEntityLog.EntityType);

            IPluralize pluralizer = new Pluralizer();

            var tableName = pluralizer.Pluralize(entityType.Name);

            var sql = $"UPDATE {tableName} SET IsDeleted = 0 WHERE Id = @id";
            var parameters = new SqlParameter[] { new SqlParameter("@id", deletedEntityLog.EntityId) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            _context.DeletedEntityLogs.Remove(deletedEntityLog);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    //add methot to delete selected log and hard delete the soft deleted entity    
    
    // GET: DeletedEntityLogs/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.DeletedEntityLogs == null)
        {
            return NotFound();
        }

        var deletedEntityLog = await _context.DeletedEntityLogs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (deletedEntityLog == null)
        {
            return NotFound();
        }

        return View(deletedEntityLog);
    }

    // POST: DeletedEntityLogs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.DeletedEntityLogs == null)
        {
            return Problem("Entity set 'ApplicationDbContext.DeletedEntityLog'  is null.");
        }
        var deletedEntityLog = await _context.DeletedEntityLogs.FindAsync(id);
        if (deletedEntityLog != null)
        {
            var entityType = Type.GetType(deletedEntityLog.EntityType);

            IPluralize pluralizer = new Pluralizer();

            var tableName = pluralizer.Pluralize(entityType.Name);

            var sql = $"DELETE FROM {tableName} WHERE IsDeleted = 1 AND Id = @id ";
            var parameters = new SqlParameter[] { new SqlParameter("@id", deletedEntityLog.EntityId) };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            _context.DeletedEntityLogs.Remove(deletedEntityLog);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    //add method to delete all logs and hard delete all soft deleted entities
    [HttpPost, ActionName("DeleteAll")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAll()
    {
        if (_context.DeletedEntityLogs == null)
        {
            return Problem("Entity set 'ApplicationDbContext.DeletedEntityLog' is null.");
        }

        var deletedEntityLogs = await _context.DeletedEntityLogs.ToListAsync();

        foreach (var entity in deletedEntityLogs)
        {
            var entityType = Type.GetType(entity.EntityType);

            IPluralize pluralizer = new Pluralizer();

            var tableName = pluralizer.Pluralize(entityType.Name);

            var sql = $"DELETE FROM {tableName} WHERE IsDeleted = 1 AND Id = @id ";
            var parameters = new SqlParameter[] { new SqlParameter("@id", entity.EntityId) };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            }
            catch (SqlException ex)
            {
                // Handle the exception here or log it if needed
                // For example, you can continue the loop without throwing an exception
                continue;
            }

            _context.DeletedEntityLogs.Remove(entity);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }



    private bool DeletedEntityLogExists(Guid id)
    {
      return (_context.DeletedEntityLogs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
