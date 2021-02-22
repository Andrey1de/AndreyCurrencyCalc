import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MoneyQuotesComponent } from './money-quotes/money-quotes.component';
import { HomeComponent } from './home/home.component';
import { ListQuotesComponent } from './list-quotes/list-quotes.component';

//import { MaterialsModule } from './modules/materials.module';
import { QuoteDataService } from './services/quote-data.service';
//import { QuotePairsService} from './services/quote-pairs.service_ts';
import { from } from 'rxjs';
import { SignalRService } from './services/signal-r.service';
import { MoneyTableComponent } from './money-table/money-table.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MoneyQuotesComponent,
    HomeComponent,
    ListQuotesComponent,
    MoneyTableComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
   
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      // { path: 'counter', component: CounterComponent },
      // { path: 'fetch-data', component: FetchDataComponent },
      { path: 'money-quotes', component: MoneyQuotesComponent },
      { path: 'list-quotes', component: ListQuotesComponent },
    ]) ,
    FormsModule,
    ReactiveFormsModule,
   // MaterialsModule.forRoot()

  ],
  providers: [ QuoteDataService, SignalRService],
  bootstrap: [AppComponent]
})
export class AppModule { }
