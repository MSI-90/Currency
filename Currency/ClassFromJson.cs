namespace Currency
{
    public class Rootobject
    {
        public string date { get; set; }
        public bool historical { get; set; }
        public Info info { get; set; }
        public Query query { get; set; }
        public float result { get; set; }
        public bool success { get; set; }
    }

    public class Info
    {
        public float quote { get; set; }
        public int timestamp { get; set; }
    }

    public class Query
    {
        public float amount { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }

}
