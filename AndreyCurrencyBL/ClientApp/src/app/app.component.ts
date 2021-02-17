import { Component } from '@angular/core';
import { SignalRService } from './services/signal-r.service';
//import { HttpClient } from '@angular/common/http';
//import { environment } from '../../environments/environment';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Currency Monitor';

  constructor(public signalRService: SignalRService) { }
  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();   
    //this.startHttpRequest();
  }
  // private startHttpRequest = () => {
  //   this.http.get('https://localhost:5001/api/chart')
  //     .subscribe(res => {
  //       console.log(res);
      
}
