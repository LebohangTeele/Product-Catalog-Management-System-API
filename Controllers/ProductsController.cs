[HttpPost("manual")]
public IActionResult ManualBind()
{
    var name = Request.Form["Name"];
    var price = decimal.TryParse(Request.Form["Price"], out var p) ? p : 0;
    // Pattern matching for validation
    if (name is null or "" || price <= 0)
        return BadRequest("Invalid data");
    // ... create product
    return Ok();
    [HttpGet("{id}/customjson")]
    public IActionResult GetProductCustomJson(Guid id)
    {
        var product = _productRepo.GetById(id);
        if (product is null) return NotFound();
        var json = System.Text.Json.JsonSerializer.Serialize(product, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        return Content(json, "application/json");
    }
}