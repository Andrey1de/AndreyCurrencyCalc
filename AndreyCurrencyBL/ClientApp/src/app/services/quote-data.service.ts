import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject,  of } from 'rxjs';
import { environment } from '../../environments/environment';
import { QuoteRecord ,DefaultQuotesMOK } from '../models/QuoteRecord';
import { Quote } from '@angular/compiler';

@Injectable(
  { providedIn: 'root' }
 )
export class QuoteDataService {

 // private members
  private  _quoteArray : QuoteRecord[] = [];
 
  private  readonly _quoteArraySubject$ : 
            BehaviorSubject<QuoteRecord[]>;
    
  
  get QuoteArray(): QuoteRecord[]
     {return this._quoteArray; }
  get QuoteArraySubject$(): BehaviorSubject<QuoteRecord[]>
     {return this._quoteArraySubject$; }

  constructor(private http: HttpClient) {
    console.log('+++QuoteDataService()');
    this._quoteArraySubject$  =  
        new BehaviorSubject<QuoteRecord[]>(this.QuoteArray);
     
    // QuoteDataService.Data.QuotePairsDelimited = 
    //       this.normDelim(environment.moneyPairsList);
  }
   
    
  public  async getDelimidetPairs$ ( delimStrIn : string) : 
    Promise<QuoteRecord[]> {
    //debugger;
    let delimStr : string = delimStrIn.replace(/\//g,'-').toLowerCase();
    let url = environment.applicationUrl 
            + environment.currencyRatios 
            + 'delimited/' +delimStr;
    console.log(`+++QuoteDataService.retrieveData(`+ url +`)`);
   
    if(environment.useMockHttp) {
      return of<QuoteRecord[]>(DefaultQuotesMOK).toPromise();
    }  
    return this.http.get<QuoteRecord[]>(url).toPromise();
    
  }

  public  async retrieveData$ ( delimStrIn : string = 'all') 
  {

    delimStrIn = (delimStrIn || '');
    if( delimStrIn.toLowerCase() === 'áll')
    {
      delimStrIn = 
        this.QuoteArray.join(',').toLowerCase() ;
    }


   let ret =  this.getDelimidetPairs$(delimStrIn)
   .then(quotes => {
       let data = this.appendArrayOfQuotes(quotes);
       
       return true;
 
    }).catch((reason)=>{
      console.log('reason: '+ reason);
      return false;
    });
    
  
  }
 
  async testchange$(){
    let url = environment.applicationUrl 
            + environment.ratioEvents + 'testchange' ;
    console.log(`+++testchange$({url})`)

    return this.http.get(url).toPromise();
    

  }
  private appendArrayOfQuotes( arr : QuoteRecord[]) : QuoteRecord[]{
   
    let map : Map<string,QuoteRecord> = new Map<string,QuoteRecord>();
    arr = arr || [];
    arr.forEach( (quote )=>{
      quote.pair = this.normKey(quote.pair);
      map.set(quote.pair,quote);
    })

    this._quoteArray.forEach((quote,)=>{
      map.set(quote.pair,quote);
    });


    this._quoteArray = [ ...map.values() ]
    this._quoteArraySubject$.next(this._quoteArray);
    
    return this.QuoteArray;
  }

  updateQuotesByEvents(arr : Array<any>){
    arr = arr || [];
    //arr.forEach(ev  =>{
       for (let index = 0; index < arr.length; index++) {
        let ev = arr[index];
        let pair = this.normKey(ev.pair) ;
  
        for (let j = 0; j < this._quoteArray.length; j++) {
          let data : any = this._quoteArray[j];
    
          if(data.pair === pair){
            data.pair = pair;
            data.ratio = ev.ratio;
            data.oldRatio = ev.oldRatio;
            data.updated = ev.updated ;
            data.percent = ev.percent ;
       //     data.percentStype = this.percentStyle(ev) 
            data.status = 2;//Status  means the changed value
            console.log("+++updateQuotesByEvent=>\n"
                + JSON.stringify(data,null,2) );
            break;
          }
        }

 
      }
    
    
  }
  // percentStyle(that: QuoteRecord) {
  //   let val = that.percent;
  //   let color = ''
  //   if ( val >  .00001) {color = 'red';}
  //   else if (val < -.00001) {color = 'blue';}
  //   else  {color = 'black';}
  //   return {
  //     color: color
  //   };
  // }
  normKey(str : string) { 
    return ('' + str).replace(/ /g,'').toUpperCase();
  }
 
  
}
