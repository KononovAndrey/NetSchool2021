namespace CodeFirst.Controllers; 

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    private readonly ILogger<SampleController> _logger;

    public SampleController(ILogger<SampleController> logger)
    {
        _logger = logger;
    }

    //[HttpGet]
    //public IEnumerable<Article> GetAll()
    //{
    //    var result = articleRepository.GetAll();
    //    return result;
    //}

    //[HttpPost]
    //public IActionResult Insert(string Title, string Text)
    //{
    //    articleRepository.Insert(new Article()
    //    {
    //        Title = Title,
    //        Text = Text
    //    });
    //    return Ok();
    //}

    //[HttpPut]
    //public IActionResult Update(int id, string Title, string Text)
    //{
    //    articleRepository.Update(new Article()
    //    {
    //        Id = id,
    //        Title = Title,
    //        Text = Text
    //    });
    //    return Ok();
    //}

    //[HttpDelete]
    //public IActionResult Delete(int id)
    //{
    //    articleRepository.Remove(id);
    //    return Ok();
    //}
}
