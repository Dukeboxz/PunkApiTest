import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Beer } from '../Interfaces/Beer';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class BeerService {

  public baseUrl: string = 'https://api.punkapi.com/v2/beers';

  constructor(private http: HttpClient) { }

  GetBeersForList(page:number, perPage:number=10):Observable<Beer[]>
  {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('per_page', perPage.toString())

    return this.http.get<Beer[]>(this.baseUrl, {params})
  }

  GetBeersByNameSearch(searchTerm: string, page: number=1): Observable<Beer[]>
  {
    const params = new HttpParams()
      .set('beer_name', searchTerm)

      return this.http.get<Beer[]>(this.baseUrl, {params})
  }
}
