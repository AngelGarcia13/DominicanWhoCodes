import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../core/core.module';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule
  ]
})
export class PagesModule {}
