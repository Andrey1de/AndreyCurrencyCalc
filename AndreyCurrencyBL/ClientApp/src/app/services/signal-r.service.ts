import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";  // or from "@microsoft/signalr" if you are using a new library
import { RatioEnentsModel } from '../models/RatioEnentsModel';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: RatioEnentsModel;
  public Url : string = environment.applicationUrl + 'api/sigalr/ratioevents';
  private hubConnection: signalR.HubConnection
  /**
   *
   */
  constructor() {
    
    
  }
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(this.Url)
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public addTransferChartDataListener = () => {
    this.hubConnection.on('changeratios', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}