using FriendOrganize.UI.Data;
using FriendOrganize.UI.Data.Lookups;
using FriendOrganize.UI.Event;
using FriendOrganizer.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganize.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupService;
        private IEventAggregator _eventAggregator;
        private IMeetingLookupDataService _meetingLookupService;

        public NavigationViewModel(IFriendLookupDataService friendLookupService,
            IMeetingLookupDataService meetingLookupService,
            IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _meetingLookupService = meetingLookupService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator
                .GetEvent<AfterDetailSavedEvent>()
                .Subscribe(AfterDetailSaved);
            _eventAggregator
                .GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(i => i.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items, 
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                    args.ViewModelName, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        public async Task LoadAsync()
        {
            Friends.Clear();
            (
                await _friendLookupService
                .GetFriendLookupAsync()
            )
            .ForEach((f) => Friends.Add(new NavigationItemViewModel(f.Id, f.DisplayMember,
            nameof(FriendDetailViewModel), _eventAggregator)));

            Meetings.Clear();
            (
                await _meetingLookupService
                .GetMeetingLookupAsync()
            )
            .ForEach((m) => Meetings.Add(new NavigationItemViewModel(m.Id, m.DisplayMember,
            nameof(MeetingDetailViewModel), _eventAggregator)));
        }

        public ObservableCollection<NavigationItemViewModel> Friends { get; }
        public ObservableCollection<NavigationItemViewModel> Meetings { get; }

        
    }
}

