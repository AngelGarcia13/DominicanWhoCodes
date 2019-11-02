import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileCardComponent } from './components/profile-card/profile-card.component';

@NgModule({
  declarations: [ProfileCardComponent],
  imports: [ CommonModule ],
  exports: [ProfileCardComponent],
})
export class SharedModule {}
