namespace MaidManagementSolutions.Web.Halper
{
    public static class TwoFactorHapler
    {
        public static async void SendOTP(string api_key, string phone_number, string otp)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://2factor.in/API/V1/" + api_key + "/SMS/" + phone_number + "/" + otp + "/OTP1");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public static void ValidateOTP() { }
    }
}
