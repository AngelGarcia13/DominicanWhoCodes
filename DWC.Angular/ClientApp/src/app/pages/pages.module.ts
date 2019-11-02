import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../core/core.module';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [HomeComponent, NotFoundComponent],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
  ],
  exports: [HomeComponent, NotFoundComponent]
})
export class PagesModule {}
