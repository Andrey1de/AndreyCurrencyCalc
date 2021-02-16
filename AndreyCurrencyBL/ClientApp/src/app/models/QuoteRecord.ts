export class QuoteRecord {
  //id : number = -1;
  pair : string = '';
  ratio: number = -1.0;
  updated : string = '';
  status : number = -1;
  isValid: boolean = false;
  // get updatedDate  () : Date {
  //   return new Date(this.uptated);
  // } 
 
   constructor() {
     //this.pair = (this.pair || '').toUpperCase() 
   }
  //    this.ratio = -1;
  //   this.uptated = new Date('1700-01-01');
  // } 
}

export const DefaultQuotesMOK : QuoteRecord[] = [
  {
    "pair": "usd/ils",
    "ratio": 3.2413,
    "updated": "2021-02-15",//"2021-02-15T16:22:59",
    "status": 1,
    "isValid": true
  },
  {
    "pair": "ils/usd",
    "ratio": 0.30851817,
    "updated": "2021-02-15T16:22:59",
    "status": 1,
    "isValid": true
  },
  {
    "pair": "gbp/eur",
    "ratio": 1.14659,
    "updated": "2021-02-15T16:22:59",
    "status": 1,
    "isValid": true
  },
  {
    "pair": "eur/jpy",
    "ratio": 127.828,
    "updated": "2021-02-15T16:22:59",
    "status": 1,
    "isValid": true
  },
  {
    "pair": "eur/usd",
    "ratio": 1.2137395,
    "updated": "2021-02-15T16:22:59",
    "status": 1,
    "isValid": true
  }
];