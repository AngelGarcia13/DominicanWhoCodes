import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/nav-menu/nav-menu.component';
import { FooterComponent } from './components/footer/footer.component';
import { ProfileCardComponent } from './components/profile-card/profile-card.component';
import { FilterPipe } from './pipes/filter.pipe';
import { HeaderSummaryComponent } from './components/header-summary/header-summary.component';

@NgModule({
  declarations: [NavbarComponent, HeaderSummaryComponent, FooterComponent, ProfileCardComponent, FilterPipe],
  imports: [CommonModule],
  exports: [NavbarComponent, HeaderSummaryComponent, FooterComponent, ProfileCardComponent, FilterPipe]
})
export class SharedModule { }
