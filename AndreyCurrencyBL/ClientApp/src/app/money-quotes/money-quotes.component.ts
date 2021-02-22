import { typeWithParameters } from '@angular/compiler/src/render3/util';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder} from '@angular/forms';
import { debug } from 'console';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { QuoteRecord } from '../models/QuoteRecord';
import { QuoteDataService } from '../services/quote-data.service';

 
@Component({
  selector: 'app-money-quotes',
  templateUrl: './money-quotes.component.html',
  styleUrls: ['./money-quotes.component.css']
})
export class MoneyQuotesComponent implements OnInit , OnDestroy{
  selectedValue : any = "All";
  private subscription : Subscription;

  @Input('pairsDelim')
  get pairsDelim(): string {
      return this.f.pairsDelim.value;
  }
  set pairsDelim(val : string) {
    this.f.pairsDelim.setValue(val);
  }

  @Input('Ready')
  Ready : boolean = false;

  quoteArray : QuoteRecord[] = [];
  _quoteArraySubject$ : BehaviorSubject<QuoteRecord[]>;
  get QuoteArraySubject$() : BehaviorSubject<QuoteRecord[]>
  {
    return this._quoteArraySubject$;
  }


  form = this.fb.group({
    pairsDelim: [''],//, Validators.required],
    pairsSelect: [''],
 
  });

  get f() {return this.form.controls;}
 
    
  constructor( private fb: FormBuilder, 
    private dataSvc : QuoteDataService) { 
      this.pairsDelim = environment.moneyPairsList;
      this._quoteArraySubject$  = dataSvc.QuoteArraySubject$;
      this.QuoteArraySubject$.getValue()

      this.subscription =
       this.dataSvc.QuoteArraySubject$.subscribe(data=>{
       // debugger;
        this.quoteArray = data || [];
        //let b = this.quoteArray
        //this.Ready = true;
       // debugger;
        
        this.normPairsDelim(this.quoteArray);
      });
  }
  
  normPairsDelim(quoteArray: QuoteRecord[]) {
    if(quoteArray.length > 0 &&
      !quoteArray.find(p=> p.status > 1 )){
 
      this.pairsDelim = this.quoteArray
        .map(p=>p.pair.toUpperCase()).join(',');
   }

  }

  ngOnInit(): void {
    this.Ready = false;
    this.getNewPairs();
  }
  ngOnDestroy(): void {
   this.subscription.unsubscribe();
  }
   
  percentStyle(that: QuoteRecord) {
    //debugger;
      let val = that.percent;
      let color = ''
      if ( val >  .00001) {
        return {'color' : 'green', 'font-weight':'bold'};
      } else if (val < -.00001) {
        return {'color' : 'red', 'font-weight':'bold'}
      }

      return {'color' : 'black'}
   
  }

 
 
  onTry(){
   // debugger;
    this.dataSvc.testchange$().
    then(
      p=>{
         p
      });
  
   }
 
//Set all the new pairs 
  getNewPairs(){
    this.Ready = false;
    let dd = this.dataSvc.retrieveData$(this.pairsDelim) 
    .then((res : string) =>{
      if(res === 'OK'){
        ;//this.quoteArray = res.quotes;
       } else{
        console.error(res);
      }
      this.Ready = true;
       //console.log('quoteDataService.retrieveData\t\n=>('+this.pairsDelim +')');
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