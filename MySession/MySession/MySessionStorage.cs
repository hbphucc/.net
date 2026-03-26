
namespace MySession.MySession
{

    public class MySessionStorage : IMySessionStorage
    {
        private readonly IMySessionStorageEngine engine;
       private readonly Dictionary<string, ISession> sessions = new Dictionary<string, ISession>();
        public MySessionStorage(IMySessionStorageEngine engine)
        {
            this.engine = engine;
        }
        public ISession Create()
        {
            var newSession =new MySession(Guid.NewGuid().ToString(), engine);
            sessions[newSession.Id] = newSession;
            return newSession;
        }

        public ISession? Get(string Id)
        {
            if (sessions.ContainsKey(Id)) 
            {
                return sessions[Id];
            }
            return null;
        }
    }
}
