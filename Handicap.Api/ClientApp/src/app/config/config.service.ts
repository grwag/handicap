import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, pipe } from 'rxjs';
import { HandicapConfig } from './handicapConfig';


@Injectable()
export class ConfigService {

  baseApiUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_API_URL') baseApiUrl: string) {
    this.baseApiUrl = baseApiUrl + 'config';
  }

  getConfig(): Observable<HandicapConfig> {
    return this.http.get<HandicapConfig>(this.baseApiUrl).pipe();
  }

  updateConfig(config: HandicapConfig): Observable<HandicapConfig> {
    return this.http.put<HandicapConfig>(this.baseApiUrl, config);
  }

  resetConfig(): Observable<HandicapConfig> {
    return this.http.put<HandicapConfig>(this.baseApiUrl, null);
  }
}
