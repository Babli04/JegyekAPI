using jegyek.Models;
using static jegyek.DTOs;

namespace jegyek
{
    public static class Extensions
    {
        public static JegyDto AsDto(this Grades jegy)
        {
            return new JegyDto(jegy.Id, jegy.Jegy, jegy.Leiras, jegy.Added);
        }
    }
}
