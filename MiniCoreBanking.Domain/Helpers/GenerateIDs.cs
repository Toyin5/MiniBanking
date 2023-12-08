using System.Text;

namespace MiniCoreBanking.Domain;
public class Generate
{
    public static string GenerateAccountNumber()
    {
        return DateTime.UtcNow.Ticks.ToString().Substring(10);
    }
}