import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Settings } from '../interfaces/settings';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  private url = environment.apiUrl + 'Settings/';
  constructor(private http: HttpClient) { }

  getSettings(): Observable<Settings> {
    return this.http.get<Settings>(this.url + "GetSettings");
  }
  saveSettings(settings: Settings): Observable<any> {
    return this.http.post<string>(this.url + "SaveSettings", settings);
  }
}
