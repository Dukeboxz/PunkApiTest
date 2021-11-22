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

  //Get beers from punk api based on page number.  If page number higher than results expecting to return empty array
  GetBeersForList(page:number, perPage:number=10):Observable<Beer[]>
  {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('per_page', perPage.toString())

    return this.http.get<Beer[]>(this.baseUrl, {params})
  }

  //Gets beers by search term 
  GetBeersByNameSearch(searchTerm: string, page: number=1): Observable<Beer[]>
  {
    const params = new HttpParams()
      .set('beer_name', searchTerm)
      .set('page', page.toString())

      return this.http.get<Beer[]>(this.baseUrl, {params})
  }
}
