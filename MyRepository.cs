namespace Learn
{
    public class MyRepository : IRepository
    {
        private ILogger<MyRepository> logger;

        public MyRepository(ILogger<MyRepository> logger)
        {
            this.logger = logger;
            logger.LogInformation("new myrepo");
        }
        public string GetByID(string id)
                {
                    return "ID: " + id;

                }
            }
}
