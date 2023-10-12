import { Component, ElementRef, ViewChild } from '@angular/core';
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
  private cellSize = 20;
  rows = Array.from({ length: 100 }, (_, i) => i);
  cols = Array.from({ length: 100 }, (_, i) => i);
  colors: string[][] = Array.from({ length: 100 }, () => Array(100).fill('white'));
  @ViewChild('canvasElement') canvasElement!: ElementRef<HTMLCanvasElement>;
  private ctx!: CanvasRenderingContext2D;

  constructor(service: WebsocketService, private fb: FormBuilder){
    this.colorForm = this.fb.group({
      selectedColor: ['red'] // Initial value
    });

    this.service = service

    this.subsribeToColorsChange()
  }

  ngAfterViewInit() {
    this.ctx = this.canvasElement.nativeElement.getContext('2d')!;
    this.drawGrid();
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

      this.fillGridWithColors();
    })
  }

  private drawGrid() {
    for (let i = 0; i <= this.canvasElement.nativeElement.width; i += this.cellSize) {
      this.ctx.moveTo(i, 0);
      this.ctx.lineTo(i, this.canvasElement.nativeElement.height);
      this.ctx.stroke();
    }

    for (let i = 0; i <= this.canvasElement.nativeElement.height; i += this.cellSize) {
      this.ctx.moveTo(0, i);
      this.ctx.lineTo(this.canvasElement.nativeElement.width, i);
      this.ctx.stroke();
    }
  }

  private fillGridWithColors() {
    for (let i = 0; i < this.colors.length; i++) {
      for (let j = 0; j < this.colors[i].length; j++) {
        const color = this.colors[i][j];
        this.ctx.fillStyle = color;
        this.ctx.fillRect(j * this.cellSize, i * this.cellSize, this.cellSize, this.cellSize);
      }
    }
  }


  canvasClicked(event: MouseEvent) {
    const canvasRect = this.canvasElement.nativeElement.getBoundingClientRect();
    const x = event.clientX - canvasRect.left;
    const y = event.clientY - canvasRect.top;

    this.service.send(this.colorForm.value + " " + x + " " + y)
  }


}
