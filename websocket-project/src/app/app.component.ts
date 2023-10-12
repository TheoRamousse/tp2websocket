import { Component } from '@angular/core';
import { WebsocketService } from './services/websocket.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'websocket-project';
  service: WebsocketService
  colorForm: FormGroup;
  rows = Array.from({ length: 100 }, (_, i) => i); // Tableau de 0 à 99
  cols = Array.from({ length: 100 }, (_, i) => i); // Tableau de 0 à 99
  colors: string[][] = Array.from({ length: 100 }, () => Array(100).fill('white'));

  constructor(service: WebsocketService, private fb: FormBuilder){
    this.colorForm = this.fb.group({
      selectedColor: ['red'] // Initial value
    });

    this.service = service

    this.subsribeToColorsChange()
  }

  private subsribeToColorsChange() {
    this.service.currentData.subscribe(newGridAsString => {
      const cellData = newGridAsString.split(', ');

      for (const data of cellData) {
        const [xStr, yStr, color] = data.split(' ');
        const x = parseInt(xStr, 10);
        const y = parseInt(yStr, 10);

        if (this.colors[x] && this.colors[x][y]) {
          this.colors[x][y] = color;
        }
      }
    })
  }

  cellClicked(x: number, y: number) {
    this.service.send(this.colorForm.value + " " + x + " " + y)
  }


}
