export class QuoteRecord {
  //id : number = -1;
  pair : string = '';
  ratio: number = -1.0;
  oldRatio: number = -1.0;
  updated : string = '';
  status : number = -1;
  validate(a : any) {
    return !!this.pair && 
      !!this.ratio && this.ratio > 0;
 // !!this.status && this.status > 0
  }
  
   constructor() {
     //this.pair = (this.pair || '').toUpperCase() 
   }

  
  //    this.ratio = -1;
  //   this.uptated = new Date('1700-01-01');
  // } 
}

export const DefaultQuotesMOK : QuoteRecord[] = [
 
];