import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder} from '@angular/forms';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { QuoteRecord } from '../models/QuoteRecord';
import { QuoteDataService } from '../services/quote-data.service';
//import {QuotePairsService} from '../services/quote-pairs.service_ts'

 
@Component({
  selector: 'app-money-quotes',
  templateUrl: './money-quotes.component.html',
  styleUrls: ['./money-quotes.component.css']
})
export class MoneyQuotesComponent implements OnInit , OnDestroy{
  selectedValue : any = "All";

  quoteArray : QuoteRecord[] = [];
  _quoteArraySubject$ : BehaviorSubject<QuoteRecord[]>;
  get QuoteArraySubject$() : BehaviorSubject<QuoteRecord[]>
  {
    return this._quoteArraySubject$;
  }

  //pairsDelim : string;
  //quoteArray :  QuoteRecord[] = [];

 //export class ProfileEditorComponent {
  form = this.fb.group({
    pairsDelim: [''],//, Validators.required],
    pairsSelect: [''],
 
  });

  get f() {return this.form.controls;}

  @Input('pairsDelim')
    get pairsDelim(): string {
        return this.f.pairsDelim.value;
    }

    set pairsDelim(val : string) {
      this.f.pairsDelim.setValue(val);
    }
  
    getPercentStyle(val: number) {
      let color = ''
      if (val >  .00001) {color = 'red';}
      else if (val < -.00001) {color = 'blue';}
      else  {color = 'black';}
      return {
        color: color
      };
  }
    private subscription : Subscription;

  constructor( private fb: FormBuilder, 
    private dataSvc : QuoteDataService) { 
      this.pairsDelim = environment.moneyPairsList;
      this._quoteArraySubject$  = dataSvc.QuoteArraySubject$;

      // this.subscription =
      //  this.dataSvc.QuoteArraySubject$.subscribe(data=>{
      //   this.quoteArray = data;
        //debugger;
      //   this.updateProfile();
      // });
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
   //this.subscription.unsubscribe();
  }

   updateProfile() {
      if(!this.pairsDelim){
      this.pairsDelim = this.quoteArray.map(p=>p.pair.toUpperCase()).join(',');
    }
  
   }
 
  onTry(){
   // debugger;
    this.dataSvc.testchange$().
    then(
      p=>{
         p
      }
      );
  
   }
 
  percent(ratio : number,oldRatio : number | null){
    oldRatio = oldRatio || ratio;
    let del = 100.0 * (1 - (oldRatio / ratio));
    return del.toFixed(3);
  }

  submit(){

    let dd = this.dataSvc.retrieveData$(this.pairsDelim) 
    .then(p=>{
      console.log('quoteDataService.retrieveData\t\n=>('+this.pairsDelim +')');
    });

  }

  
}


   //TBD to receive from services
  // retreivePairs(){
  //   this.moneyPairsList = [
  //      'USD/EUR'
  //     ,'USD/ILS'
  //     ,'GBP/EUR'
  //     ,'EUR/JPY'
  //     ,'EUR/USD'
       
  //   ];
  // }