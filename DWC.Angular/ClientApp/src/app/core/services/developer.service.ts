import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Developer } from '../models/developer.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {

  constructor(private http: HttpClient) {
  }

  getDevelopers(): Observable<Developer[]> {
    return this.http.get<any>('assets/data/developers.json').pipe(
      map((devs: any[]) => {
         const developerList: Developer[] = devs.map(x => {
          return new Developer(x);
         });
        return developerList;
      })
    );
  }
}
