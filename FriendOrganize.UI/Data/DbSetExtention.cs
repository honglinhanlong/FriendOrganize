using System.Data.Entity;
using System.Threading.Tasks;

namespace FriendOrganize.UI.Data
{
    public static class DbSetExtention
    {
        public static DbContext AttachAt<TEntity>(this DbSet<TEntity> @this, TEntity item, DbContext context) where TEntity : class, new()
        {
            @this.Attach(item);
            return context;
        }
        public static async Task<DbContext> AttachSave<T>(this DbContext @this, T item, EntityState state) where T : class, new()
        {
            @this.Entry<T>(item).State = state;
            await @this.SaveChangesAsync();
            return @this;
        }
    }
}
