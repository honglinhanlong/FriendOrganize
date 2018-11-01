using Autofac;
using FriendOrganize.UI.Data;
using FriendOrganize.UI.Data.Lookups;
using FriendOrganize.UI.Data.Repositories;
using FriendOrganize.UI.View.Services;
using FriendOrganize.UI.ViewModel;
using FriendOrganizer.DataAccess;
using Prism.Events;

namespace FriendOrganize.UI.StartUp
{
    public class Bootstrapper
    {
        public IContainer Bootstrap() => new ContainerBuilder()
            .RegisterAs<FriendOrganizationDbContext>()
            .RegisterAs<MessageDialogService, IMessageDialogService>()
            .RegisterAs<MainWindow>()
            .RegisterAs<MainViewModel>()
            .RegisterAsSingInstance<EventAggregator, IEventAggregator>()
            .RegisterAs<NavigationViewModel, INavigationViewModel>()
            .ResisterAsKeyed<FriendDetailViewModel, IDetailViewModel>()
            .ResisterAsKeyed<MeetingDetailViewModel, IDetailViewModel>()
            .ResisterAsKeyed<ProgrammingLanguageDetailViewModel, IDetailViewModel>()
            .RegisterAsImplementedInterfaces<LookupDataService>()
            .RegisterAs<FriendRepository, IFriendRepository>()
            .RegisterAs<MeetingRepository, IMeetingRepository>()
            .RegisterAs<ProgrammingLanguageRepository, IProgrammingLanguageRepository>()
            .Build();
    }
}
