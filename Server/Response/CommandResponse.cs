namespace Server.Response
{
    public class CommandResponse
    {
        public CommandResponse(bool successed, string respnse)
        {
            Successed = successed;
            Respnse = respnse;
        }

        public bool Successed { get; set; }
        public string Respnse { get; set; }
    }
}
