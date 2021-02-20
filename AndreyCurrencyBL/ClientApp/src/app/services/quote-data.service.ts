import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject,  of } from 'rxjs';
import { environment } from '../../environments/environment';
import { QuoteRecord ,DefaultQuotesMOK } from '../models/QuoteRecord';

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
    arr.forEach(ev  =>{
      let pair = this.normKey(ev.pair) ;
      let data = this._quoteArray.find(p=> p.pair === pair);
      if(data) {
        try{
          data.ratio = ev.ratio;
          data.oldRatio = ev.oldRatio;
          data.updated = ev.updated ;
          data.status = 2;//Statys  means the changed value
        } catch(err){
          
          console.log(err.toString());
        }

      }
    
    })
  }
  
  normKey(str : string) { 
    return ('' + str).replace(/ /g,'').toUpperCase();
  }
 
  
}
