import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/nav-menu/nav-menu.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderSummaryComponent } from './components/header-summary/header-summary.component';

@NgModule({
  declarations: [NavbarComponent, HeaderSummaryComponent, FooterComponent],
  imports: [CommonModule],
  exports: [NavbarComponent, HeaderSummaryComponent, FooterComponent]
})
export class LayoutModule { }
