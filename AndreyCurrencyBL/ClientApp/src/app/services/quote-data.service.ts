import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject,BehaviorSubject,  of } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { QuoteRecord ,DefaultQuotesMOK } from '../models/QuoteRecord';





 export class SubjectData{
  QuoteArray: QuoteRecord[] = [];
  get QuoteKeysArray(): string[] {
    return this.QuoteArray.map(p=>p.pair.toUpperCase());
  };
 
 }
@Injectable(
  { providedIn: 'root' }
 )
export class QuoteDataService {

 // private static QuotePairsDelimited$ : Subject<string> = new Subject<string>();
  static Data : SubjectData = new SubjectData();

  static readonly SubjectData$ : BehaviorSubject<SubjectData> 
      = new BehaviorSubject<SubjectData>(QuoteDataService.Data);

 
  constructor(private http: HttpClient) {
    console.log('+++QuoteDataService()');
    // QuoteDataService.Data.QuotePairsDelimited = 
    //       this.normDelim(environment.moneyPairsList);
   
    this.retrieveData$ (environment.moneyPairsList);
    // .catch(
    //   p=>console.log('+++retrieveData$,catch(' + p + '}' );
    // );
    // console.log('+++QuotePairsDelimited('+
    //   QuoteDataService.Data.QuotePairsDelimited  +')');
 
  }

 
  public  async getDelimidetPairs$ ( delimStrIn : string) : 
    Promise<QuoteRecord[]> {
    //debugger;
    let delimStr : string = delimStrIn.replace(/\//g,'-').toLowerCase();
    let url = environment.applicationUrl + 'delimited/' +delimStr;
    console.log(`+++QuoteDataService.retrieveData(`+ url +`)`);
   //let arr : QuoteRecord[] = [];
    let observ : Promise<QuoteRecord[]> = null;
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
        QuoteDataService.Data.QuoteArray.join(',').toLowerCase() ;
    }


   let ret =  this.getDelimidetPairs$(delimStrIn)
   .then(quotes => {
       let data = this.appendArrayOfQuotes(quotes);
       QuoteDataService.SubjectData$.next(data);
       return true;
 
    }).catch((reason)=>{
      console.log('reason: '+ reason);
      return false;
    });
    
  
  }
 

  private appendArrayOfQuotes( arr : QuoteRecord[]) : SubjectData{
   
    let map : Map<string,QuoteRecord> = new Map<string,QuoteRecord>();
    arr = arr || [];
    arr.forEach( (quote )=>{
      quote.pair = this.normKey(quote.pair);
      map.set(quote.pair,quote);
    })

    QuoteDataService.Data.QuoteArray.forEach((quote,)=>{
      map.set(quote.pair,quote);
    });

   
  
    var data  = new  SubjectData();
    data.QuoteArray =[ ...map.values() ];
    //data.QuoteKeysArray =[ ...map.keys() ];
    //data.QuotePairsDelimited =  data.QuoteKeysArray.join(',');
   // QuoteDataService.LastData = data;
     return data;
  }
  
  private normKey(str : string) { 
    return ('' + str).replace(/ /g,'').toUpperCase();
  }
 
  
}
