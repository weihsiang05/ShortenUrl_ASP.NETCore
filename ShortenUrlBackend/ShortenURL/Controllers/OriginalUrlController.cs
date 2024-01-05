using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortenURL.Data.Context;
using ShortenURL.Data.Models;
using System.Text.Json;

namespace ShortenURL.Controllers
{
    //[Route("api/")]
    [ApiController]
    public class OriginalUrlController : ControllerBase
    {
        private readonly ShortenUrlDbContext _shortenUrlDbContext;

        public OriginalUrlController(ShortenUrlDbContext shortenUrlDbContext)
        {
            _shortenUrlDbContext = shortenUrlDbContext;
        }

        private string GenerateNewLink(string originalLink)
        {
            const string fixLink = "http://localhost:3000/";
            const string randomLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            char[] createRandomLetters = new char[5];
            

            for (int i = 0; i < 5; i++)
            {
                createRandomLetters[i] = randomLetters[random.Next(randomLetters.Length)];
            }

            string newLink = fixLink + new string(createRandomLetters);

            return newLink;
        }

        [HttpPost]
        [Route("link")]
        public async Task<IActionResult> OriginalUrl([FromBody] JsonElement originalUrlRequest)
        {  
            //return await _shortenUrlDbContext.OriginalUrl.ToListAsync();
            try
            {
                
                originalUrlRequest.TryGetProperty("links", out var linksProperty);
                //Console.WriteLine(linksProperty);
                var originalLink = linksProperty.GetString();
                //Console.WriteLine(originalLink);

                string shorterLink = GenerateNewLink(originalLink);
                Console.WriteLine(shorterLink);
                

                var existingUrl = await _shortenUrlDbContext.OriginalUrl.FirstOrDefaultAsync(url => url.OriginalLink == originalLink);
                
                Console.WriteLine(existingUrl);
                if (existingUrl == null)
                {

                    var createOriginalLink = new OriginalUrl
                    {
                        OriginalLink = originalLink
                    };
                    _shortenUrlDbContext.OriginalUrl.Add(createOriginalLink);
                    await _shortenUrlDbContext.SaveChangesAsync();

                    var getOrignalLinkId = createOriginalLink.OriginalLinkId;
                   

                    var createNewLink = new NewUrl
                    {
                        NewLink = shorterLink,
                        OriginalLinkId= getOrignalLinkId
                    };

                    _shortenUrlDbContext.NewUrl.Add(createNewLink);
                    await _shortenUrlDbContext.SaveChangesAsync();

                    // Return a success response, indicating that the URL was successfully created
                    return Ok(new { fullLink = originalLink, newLink = shorterLink });
                }
                else
                {
                    var getOrignalLinkId = existingUrl.OriginalLinkId;
                    var findOrignalLinkId = await _shortenUrlDbContext.NewUrl.FirstOrDefaultAsync(url => url.OriginalLinkId == getOrignalLinkId);
                    if (findOrignalLinkId != null)
                    {
                        // Return a response indicating that the URL already exists
                        return Ok(new { fullLink = originalLink, newLink = findOrignalLinkId.NewLink });
                    }
                    else
                    {
                        return BadRequest(new { error = "Can't find the Link!" });
                    }
                }
                    
            }
            catch(Exception ex)
            {
                // Return an error response
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("link/findRandomLetter")]
        public async Task<IActionResult> NewUrl([FromBody] JsonElement newUrlRequest)
        {
            //return await _shortenUrlDbContext.NewUrl.ToListAsync();
            try
            {
                newUrlRequest.TryGetProperty("randomLetter", out var linksProperty);
                const string fixLink = "http://localhost:3000/";
                var randomLetter = linksProperty.GetString();
                var newLink = fixLink+ randomLetter;
                Console.WriteLine(newLink);

                var existingNewUrl = await _shortenUrlDbContext.NewUrl.FirstOrDefaultAsync(url => url.NewLink == newLink);
                if (existingNewUrl != null)
                {
                    var findOriginalLinkIdInNewUrlTable = existingNewUrl.OriginalLinkId;
                    var originalLinkId = await _shortenUrlDbContext.OriginalUrl.FirstOrDefaultAsync(url => url.OriginalLinkId == findOriginalLinkIdInNewUrlTable);
                    if (originalLinkId != null)
                    {
                        return Ok(new { fullLink = originalLinkId.OriginalLink });
                    }
                    else
                    {
                        return BadRequest(new { error = "Can't find the OriginalLink!" });
                    }
                }
                else
                {
                    return BadRequest(new { error = "Can't find the NewLink!" });
                }

                
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
