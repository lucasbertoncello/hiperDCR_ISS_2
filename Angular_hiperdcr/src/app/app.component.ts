import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'hiperDCR';

  fileName = '';

  isActive = false

    constructor(private http: HttpClient) {}

    onFileSelected(event: any) {

        const file:File = event.target.files[0];

        if (file) {

            this.fileName = file.name;

            const formData = new FormData();

            formData.append("pdf", file);



            this.http.post("https://hiperdcr.hiperstream.com/hiperDCR", formData).subscribe((data: any) => {
              data == false ? this.isActive = false : this.isActive = true
            });


        }
    }
}

    
