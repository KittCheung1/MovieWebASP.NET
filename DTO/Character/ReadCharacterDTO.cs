namespace TestWebASP.NET.DTO.Responses
{
    public class ReadCharacterDTO
    {
        public int Id { get; init; }
        public string FullName { get; init; }
        public string Alias { get; init; }
        public string Gender { get; init; }
        public string Picture { get; init; }
        public int Movie { get; set; }
    }
}
