import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalRService } from './services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit , OnDestroy{
  title = 'Currency Ratios Monitor ';
  
  constructor(private signalRService : SignalRService) {
    
  }
  ngOnInit(): void {
    this.signalRService.start();
  }
  ngOnDestroy(): void {
    this.signalRService.stop();
  }

}
