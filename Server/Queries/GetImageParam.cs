namespace Server.Queries
{
    public class GetImageParam
    {
        public string Id { get; set; }

        public GetImageParam(string id)
        {
            Id = id;
        }
    }
}
