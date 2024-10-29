using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using MimeKit;

public class OtpService
{
    private readonly IConfiguration _configuration;
    private static readonly ConcurrentDictionary<string, (string Otp, DateTime Expiry)> OtpStorage =
        new();

    public OtpService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateOtp()
    {
        Random random = new Random();
        string otp = random.Next(100000, 999999).ToString();
        return otp;
    }

    public async Task<bool> SendOTP(string email)
    {
        // Generate the OTP and set its expiry time
        string _otp = GenerateOtp();
        DateTime _expiry = DateTime.UtcNow.AddMinutes(5);
        TimeSpan timeLeft = _expiry - DateTime.UtcNow;
        int _expiryMinute = (int)Math.Ceiling(timeLeft.TotalMinutes);
        OtpStorage.TryAdd(email, (_otp, _expiry));

        // Retrieve SMTP credentials from configuration
        NetworkCredential networkCredential = new NetworkCredential(
            _configuration["SmtpSettings:Username"],
            _configuration["SmtpSettings:AppPassword"]
        );

        try
        {
            // Configure the SMTP client
            using SmtpClient client = new SmtpClient
            {
                Host = _configuration["SmtpSettings:Host"],
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = networkCredential
            };

            // Create the email message
            MailAddress from = new MailAddress(
                _configuration["SmtpSettings:Username"],
                "TTT - OTP Service"
            );
            MailAddress to = new MailAddress(email);
            using MailMessage message = new MailMessage(from, to)
            {
                Subject = "OTP Verification",
                Body =
                    $@"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <body style=""font-family: Arial, sans-serif; background-color: #f4f4f9; margin: 0; padding: 20px;"">
                            <div style=""background-color: #ffffff; padding: 20px 30px; max-width: 400px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); text-align: center; margin: 0 auto;"">
                                <h1 style=""color: #333; font-size: 24px; margin-bottom: 20px;"">OTP Verification</h1>
                                <p style=""font-size: 16px; color: #555; margin-bottom: 10px;"">
                                    Your OTP code is 
                                    <strong style=""color: #007bff; font-size: 18px;"">{_otp}</strong>
                                </p>
                                <p style=""font-size: 14px; color: #888; margin-bottom: 20px;"">
                                    It will expire in {_expiryMinute} minutes.
                                </p>
                                <p style=""font-size: 14px; color: #ff0000; font-weight: bold;"">
                                    Please do not share this code with anyone for security reasons.
                                </p>
                            </div>
                        </body>
                        </html>",
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
            return true; // OTP sent successfully
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send OTP: {ex.Message}");
            return false; // OTP sending failed
        }
    }

    public async Task<bool> VerifyOTP(string email, string inputOTP)
    {
        if (OtpStorage.TryGetValue(email, out (string OTP, DateTime Expiry) stored))
        {
            if (stored.OTP == inputOTP && stored.Expiry > DateTime.UtcNow)
            {
                return true;
            }
            else
            {
                OtpStorage.TryRemove(email, out _);
            }
        }
        return false;
    }
}
