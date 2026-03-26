namespace MySession.MySession
{
    public static class MySessionRegistrationExtension
    {
        public static void AddMySession(this IServiceCollection services)
        {

            services.AddSingleton<IMySessionStorageEngine>(services =>
            {
                var path = Path.Combine(services.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
                Directory.CreateDirectory(path);

                return new FileMySessionStorageEngine(path);
            });

            services.AddSingleton<IMySessionStorage, MySessionStorage>();
            services.AddScoped<MySessionScopedContainer>();

        }
    }
}
