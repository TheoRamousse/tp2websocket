import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
  private socket: WebSocket;
  public currentData: Subject<string> = new Subject();
  
  constructor() {
    this.socket = new WebSocket(environment.urlWebSocket);
    this.socket.onopen = () => this.onOpen();
    this.socket.onclose = (event) => this.onClose(event);
    this.socket.onmessage = (event) => this.onMessage(event);
    this.socket.onerror = (event) => this.onError(event);
  }
  
  private onOpen() {
    console.log('Connexion WebSocket ouverte.');
  }
  
  private onClose(event : CloseEvent) {
    console.log('Connexion WebSocket fermée.', event);
  }
  
  private onMessage(event : MessageEvent) {
    this.currentData.next(event.data)
    console.log('Message reçu :', event.data);

  }
  
  private onError(event : Event) {
    console.error('Erreur WebSocket :', event);
  }
  
  send(message: string) {
    this.socket.send(message);
  }
  
  close() {
    this.socket.close();
  }
}
