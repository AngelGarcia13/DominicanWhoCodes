import { Component, OnInit, Input } from '@angular/core';
import { Developer } from 'src/app/core/models/developer.model';

@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html',
  styleUrls: ['./profile-card.component.css']
})
export class ProfileCardComponent implements OnInit {

  @Input() developer: Developer;

  constructor() { }

  ngOnInit(): void { }
}
