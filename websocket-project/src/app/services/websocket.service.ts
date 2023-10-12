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
    var self = this;
    this.waitForSocketConnection(this.socket, function(){
      self.send("GET")
    })
  }

  private waitForSocketConnection(socket: any, callback: any){
    var self = this;
    setTimeout(
        function () {
            console.log(socket.readyState)
            if (socket.readyState === 1) {
                console.log("Connection is made")
                if (callback != null){
                    callback();
                }
            } else {
                console.log("wait for connection...")
                self.waitForSocketConnection(socket, callback);
            }

        }, 5); // wait 5 milisecond for the connection...
}
  
  private onClose(event : CloseEvent) {
    console.log('Connexion WebSocket fermée.', event);
  }
  
  private onMessage(event : MessageEvent) {
    var msgAsString = (event.data as string)
    this.currentData.next(msgAsString)

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
