using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronOcr;
using Tesseract;

namespace HackathonOCRApi.Services
{
    public class OcrService
    {
        public Ocr OcrReader(string caminhoArquivo, string valorComparacao)
        {
            var ocrReader = new IronTesseract();
            using (var Input = new OcrInput(caminhoArquivo))
            {
                var Result = ocrReader.Read(Input);

                var ocr = new Ocr();

                ocr.NomeDaImagem = Path.GetFileName(caminhoArquivo);
                ocr.ValordaComparacao = valorComparacao;
                ocr.ResultadoDaComparacao = Result.Text.ToUpper().Contains(valorComparacao.ToUpper());

                return ocr;

            }
            
        }
    }
}
