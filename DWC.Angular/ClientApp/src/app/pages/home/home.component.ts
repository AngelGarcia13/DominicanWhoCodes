import { Component, OnInit } from '@angular/core';
import { Developer } from 'src/app/core/models/developer.model';
import { DeveloperService } from 'src/app/core/services/developer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

    developers: Array<Developer> = new Array<Developer>();

    constructor(private developerService: DeveloperService) {
    }

    ngOnInit(): void {
      this.developerService.getDevelopers().subscribe(x => this.developers = x);
    }
}
