using System.Linq.Expressions;

namespace CoreDevextremeTheme.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddGlobalQueryFilter<TEntity>(this ModelBuilder modelBuilder, Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(filter);
        }


        //public static void AddIsDeletedFilter(this ModelBuilder modelBuilder)
        //{
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        var isDeletedProperty = entityType.FindProperty("IsDeleted");
        //        if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
        //        {
        //            var parameter = Expression.Parameter(entityType.ClrType, "p");
        //            var body = Expression.Not(Expression.Property(parameter, "IsDeleted"));
        //            var lambda = Expression.Lambda(body, parameter);

        //            var method = typeof(ModelBuilderExtensions).GetMethod("AddGlobalQueryFilter", new[] { typeof(ModelBuilder), typeof(Expression<>) });
        //            var genericMethod = method.MakeGenericMethod(entityType.ClrType);
        //            genericMethod.Invoke(null, new object[] { modelBuilder, lambda });
        //        }
        //    }
        //}
    }
}
