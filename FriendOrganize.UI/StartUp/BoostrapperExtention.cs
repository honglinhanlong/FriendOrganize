using Autofac;

namespace FriendOrganize.UI.StartUp
{
    public static class BoostrapperExtention
    {
        public static ContainerBuilder RegisterAs<T, IT>(this ContainerBuilder @this) where T : class
        {
            @this.RegisterType<T>().As<IT>();
            return @this;
        }

        public static ContainerBuilder RegisterAsSingInstance<T, IT>(this ContainerBuilder @this) where T : class
        {
            @this.RegisterType<T>().As<IT>().SingleInstance();
            return @this;
        }

        public static ContainerBuilder ResisterAsKeyed<T, IKeyed>(this ContainerBuilder @this, string key) where T: class
        {
            @this.RegisterType<T>().Keyed<IKeyed>(key);
            return @this;
        }

        public static ContainerBuilder ResisterAsKeyed<T, IKeyed>(this ContainerBuilder @this)
        {
            @this.RegisterType<T>().Keyed<IKeyed>(typeof(T).Name);
            return @this;
        }

        public static ContainerBuilder RegisterAs<T>(this ContainerBuilder @this) where T : class
        {
            @this.RegisterType<T>().AsSelf();
            return @this;
        }

        public static ContainerBuilder RegisterAsImplementedInterfaces<T>(this ContainerBuilder @this)
        {
            @this.RegisterType<T>().AsImplementedInterfaces();
            return @this;
        }
    }
}
