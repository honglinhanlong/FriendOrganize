using FriendOrganizer.Model;
using System.Collections.Generic;
using FriendOrganizer.DataAccess;
using System.Linq;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FriendOrganize.UI.Data.Repositories
{

    public class FriendRepository : GenericRepository<Friend, FriendOrganizationDbContext>, IFriendRepository
    {
        public FriendRepository(FriendOrganizationDbContext context)
        : base(context) { }

        public override async Task<Friend> GetByIdAsync(int friendId) =>
            await Context.Friends
            .Include(f => f.PhoneNumbers)
            .SingleAsync((f) => f.Id == friendId);

        public async Task<bool> HasMeetingsAsync(int friendId) =>
            await Context.Meetings.AsNoTracking()
            .Include(m => m.Friends)
            .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));

        public void RemovePhoneNumber(FriendPhoneNumber model) => Context.FriendPhoneNumbers.Remove(model);

    }
}
