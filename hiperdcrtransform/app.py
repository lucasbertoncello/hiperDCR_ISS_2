from flask import Flask
import image_processing as imagem
from flask_cors import cross_origin
app = Flask(__name__)

@app.route('/')
def hello_world():

    return app.response_class(
        response="Up",
        status=200,
        mimetype="application/json"     
    )

@app.route('/hiperDCR', methods=["POST", "GET"])
@cross_origin()

def validar_campo():

    print("teste")
    response = imagem.Image_processing.pdf2png_crop()

    return str(response["resultadoDaComparacao"]).lower()

if __name__ == '__main__':
    app.run(debug=True)