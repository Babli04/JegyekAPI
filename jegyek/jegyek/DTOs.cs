namespace jegyek
{
    public class DTOs
    {
        public record JegyDto(Guid Id, int Jegy, string Leiras, string Added);
        public record CreateJegyDto(int Jegy, string Leiras);
        public record UpdateJegyDto(int Jegy, string Leiras);
    }
}
