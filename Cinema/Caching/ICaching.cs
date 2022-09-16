namespace Cinema.Caching
{
    public interface ICaching
    {
        public string Get(string Key);
        public void Remove(string key);
        public void Set(string key, object data);
    }
}
