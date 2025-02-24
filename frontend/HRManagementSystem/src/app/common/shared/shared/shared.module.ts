import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ValidDirective } from '../../../components/directives/valid.directive';
import { TrCurrencyPipe } from 'tr-currency';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ValidDirective,
    TrCurrencyPipe
  ],
  exports:[
    CommonModule,
    FormsModule,
    RouterModule,
    ValidDirective,
    TrCurrencyPipe
  ]
})
export class SharedModule { }
