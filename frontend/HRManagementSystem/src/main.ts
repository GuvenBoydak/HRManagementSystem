import { bootstrapApplication, BrowserModule } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app/app.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { routes } from './app/app.routes';

bootstrapApplication(AppComponent, {
  providers : [
   provideHttpClient(),
   importProvidersFrom(
     BrowserModule,
     CommonModule,
     BrowserAnimationsModule,
     NgxSpinnerModule,
     RouterModule.forRoot(routes),
     ToastrModule.forRoot({
       closeButton: true,
       progressBar: true
     }),
   )
  ] 
 })