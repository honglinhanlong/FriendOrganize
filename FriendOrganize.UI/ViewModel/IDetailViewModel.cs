using System;
using System.Threading.Tasks;

namespace FriendOrganize.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int Id);
        bool HasChanges { get; }
        int Id { get; }
    }
}