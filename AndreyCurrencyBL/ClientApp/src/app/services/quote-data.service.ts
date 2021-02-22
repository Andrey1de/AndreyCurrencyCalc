import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject,  of } from 'rxjs';
import { environment } from '../../environments/environment';
import { QuoteRecord ,DefaultQuotesMOK } from '../models/QuoteRecord';

// export class RetrieveResult{
//   ret: boolean = false;
//   quotes: QuoteRecord[] = [];
//   reason : string = ""
// }
@Injectable(
  { providedIn: 'root' }
 )
export class QuoteDataService {

 // private members
  get QuoteArray(): QuoteRecord[] {return this._quoteArray; }

  private  _quoteArray : QuoteRecord[] = [];
  private _pairsDelim : string;
  get PairsDelim() : string {return this._pairsDelim;}
   
  readonly QuotesSubject$: BehaviorSubject<QuoteRecord[]>
  readonly PairsDelimdSubject$: BehaviorSubject<string>
     

  constructor(private http: HttpClient) {
    this._pairsDelim = environment.moneyPairsList;

    console.log('+++QuoteDataService()');
    this.PairsDelimdSubject$  =  
        new BehaviorSubject<string>(this._pairsDelim);
    this.QuotesSubject$  =  
        new BehaviorSubject<QuoteRecord[]>(this.QuoteArray);
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

  public  async retrieveData$ ( delimStrIn : string = 'all') : 
  Promise<string>
  {

    delimStrIn = (delimStrIn || '');
    if( delimStrIn.toLowerCase() === 'áll')
    {
      delimStrIn = 
        this.QuoteArray.join(',').toLowerCase() ;
    }

    let ret : string = "OK";
    let delim = '';
    try {
      this._quoteArray =  await this.getDelimidetPairs$(delimStrIn);
      if(this._quoteArray && Array.isArray(this._quoteArray)
         && this._quoteArray.length > 0){
        this._quoteArray.forEach(p=> {
          p.pair = this.normKey(p.pair);
          delim = delim + ((!delim) ? p.pair: ','+ p.pair) ;
        })
        this.QuotesSubject$.next(this._quoteArray);
        if(this._pairsDelim != delim)
        this.PairsDelimdSubject$.next(this._pairsDelim = delim);

      }
       
    } catch (error) {
      ret = error.toString();
     }
   
   return of(ret).toPromise();
  
  }

  async testchange$(){
    let url = environment.applicationUrl 
            + environment.ratioEvents + 'testchange' ;
    console.log(`+++testchange$({url})`)

    return this.http.get(url).toPromise();
    

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
  
  normKey(str : string) { 
    return ('' + str).replace(/ /g,'').toUpperCase();
  }
 
  
}
// private appendArrayOfQuotes( arr : QuoteRecord[]) : QuoteRecord[]{
   
//   let map : Map<string,QuoteRecord> = new Map<string,QuoteRecord>();
//   arr = arr || [];
//   arr.forEach( (quote )=>{
//     quote.pair = this.normKey(quote.pair);
//     map.set(quote.pair,quote);
//   })

//   this._quoteArray.forEach((quote,)=>{
//     map.set(quote.pair,quote);
//   });


//   this._quoteArray = [ ...map.values() ]
//   this.QuotesSubject$.next(this._quoteArray);
  
//   return this.QuoteArray;
// }