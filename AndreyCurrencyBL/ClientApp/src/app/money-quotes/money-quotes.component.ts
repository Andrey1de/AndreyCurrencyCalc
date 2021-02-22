import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
// import { FormGroup, FormControl, Validators, FormBuilder} from '@angular/forms';
// import { debug } from 'console';
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
  
  readonly PairsDelimdSubject$: BehaviorSubject<string>;

  form = this.fb.group({
    pairsDelim: [''],//, Validators.required],
  //  pairsSelect: [''],
 
  });

  get f() {return this.form.controls;}
 
    
  constructor( private fb: FormBuilder, 
    private dataSvc : QuoteDataService) { 
        this.PairsDelimdSubject$  = dataSvc.PairsDelimdSubject$;
        this.subscription = this.PairsDelimdSubject$.subscribe(
          delim=> this.pairsDelim = delim
        );
   }
 
  ngOnInit(): void {
    this.Ready = false;
    this.pairsDelim = this.PairsDelimdSubject$.getValue();
    this.getNewPairs();
  }
  
  ngOnDestroy(): void {
   this.subscription.unsubscribe();
  }
  
  onTry(){
 
    this.dataSvc.testchange$();
    // then(p=>{
    //    p});
  
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