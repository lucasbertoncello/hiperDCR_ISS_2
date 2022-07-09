using HackathonOCRApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;


namespace HackathonOCRApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        private readonly ILogger<OcrController> _logger;

        public OcrController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile arquivo, [FromQuery] string valorComparacao)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
                    {
                        _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    }

                    var caminhoArquivo = _environment.WebRootPath + "\\imagens";

                    if (!Directory.Exists(caminhoArquivo))
                    {
                        Directory.CreateDirectory(caminhoArquivo);
                    }

                    using (FileStream filestream =
                        System.IO.File.Create(Path.Combine(caminhoArquivo,arquivo.FileName)))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        
                    }

                    var ocr = new OcrService();
                    return new JsonResult(ocr.OcrReader(Path.Combine(caminhoArquivo, arquivo.FileName), valorComparacao));

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    return new JsonResult(ex.ToString());
                }
            }
            else
            {
                _logger.LogError("Nenhum arquivo carregado.");
                return new JsonResult("Nenhum arquivo carregado.");
            }
        }
    }
}