import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder} from '@angular/forms';
import { environment } from 'src/environments/environment';
import { QuoteRecord } from '../models/QuoteRecord';
import { QuoteDataService , SubjectData } from '../services/quote-data.service';
//import {QuotePairsService} from '../services/quote-pairs.service_ts'

 
@Component({
  selector: 'app-money-quotes',
  templateUrl: './money-quotes.component.html',
  styleUrls: ['./money-quotes.component.css']
})
export class MoneyQuotesComponent implements OnInit {
  selectedValue : any = "All";

  Data : SubjectData = new SubjectData();

  //pairsDelim : string;
  quoteArray :  QuoteRecord[] = [];

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
  
   
  
  constructor( private fb: FormBuilder, 
    private quoteDataService : QuoteDataService) { 
      this.pairsDelim = environment.moneyPairsList;

      QuoteDataService.SubjectData$.subscribe(data=>{
        this.Data = data;
        //debugger;
        this.updateProfile();
      });
  }

   updateProfile() {
    this.quoteArray =  this.Data.QuoteArray;
    if(!this.pairsDelim){
      this.pairsDelim = this.quoteArray.map(p=>p.pair.toUpperCase()).join(',');
    }
    // this.form.patchValue({
    //   pairsDelim: this.Data.QuoteKeysArray,
    // });
   }
    ngOnInit(): void {
  }

  onTry(){
    let dd = this.quoteDataService.retrieveData$(this.pairsDelim) 
      .then(p=>{
        console.log('quoteDataService.retrieveData\t\n=>('+this.pairsDelim +')');
      });

    console.log(this.selectedValue.value);
  }
 
  submit(){
     let formValue = this.form.value;
     console.log('Submit formValue' + formValue.toString());
   
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