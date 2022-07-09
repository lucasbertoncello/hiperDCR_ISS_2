from email.headerregistry import ContentTypeHeader
from pdf2image import convert_from_path
from PIL import Image
import requests

class Image_processing:

    def pdf2png_crop():

        pages = convert_from_path('sample01.pdf', 500, poppler_path=r'\poppler-0.68.0\bin')

        for i, image in enumerate(pages):
            fname = 'image'+str(i)+'.png'
            image.save('image.png', "PNG")

        im = Image.open('image1.png')
        im_crop = im.crop((3299,12,4034,288))
        im_crop.save('image1_crop.png', quality=95)

        campo = "378,69"
        url = f"https://hiperdcrreader.hiperstream.com/ocr?valorComparacao={campo}"
        files = {
            'arquivo': open('image1_crop.png', 'rb'),
        }
        
        response = requests.post(url, files=files, verify=False)

        status_code = response.status_code

        if status_code != 200:
            raise SystemError(
                "Bad response from Processing: {!r} / {!r} / {!r}".format(response.status_code, response.headers, response.text)
            )
       
        return response.json()