using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganize.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository
        : GenericRepository<ProgrammingLanguage, FriendOrganizationDbContext>,
        IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(FriendOrganizationDbContext context) 
            : base(context)
        {
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}
