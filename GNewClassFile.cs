using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

public class NewsController : ApiController
{
    private const string GNEWS_API_KEY = "YOUR_API_KEY";

    [HttpGet]
    [Route("api/news")]
    public async Task<IHttpActionResult> GetNews(string query = "technology")
    {
        using (var httpClient = new HttpClient())
        {
            try
            {
                string url = $"https://gnews.io/api/v4/search?q={query}&token={GNEWS_API_KEY}";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    return BadRequest("Failed to fetch news articles");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
