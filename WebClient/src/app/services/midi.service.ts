import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MidiService {
  private url = environment.apiUrl + 'Midi/';
  constructor(private http: HttpClient) { }
  getMidiInputDevices(): Observable<string[]> {
    return this.http.get<string[]>(this.url + "GetMidiInputDevices");
  }
  getMidiOutputDevices(): Observable<string[]> {
    return this.http.get<string[]>(this.url + "GetMidiOutputDevices");
  }
  getCurrentMidiInputDevice(): Observable<string> {
    return this.http.get(this.url + "GetCurrentMidiInputDevice", { responseType: "text" });
  }
  getCurrentMidiOutDevice(): Observable<string> {
    return this.http.get(this.url + "GetCurrentMidiOutputDevice", { responseType: "text" });
  }
  setMidiInputDevice(device: string): Observable<string> {
    return this.http.post<string>(this.url + "SetMidiInputDevice?device=" + encodeURIComponent(device + ""), null);
  }
  setMidiOutputDevice(device: string): Observable<string> {
    return this.http.post<string>(this.url + "SetMidiOutputDevice?device=" + encodeURIComponent(device + ""), null);
  }
  setCurve(curve:string):Observable<any>{
    return this.http.post<string>(this.url + "SetCurve?name=" + encodeURIComponent(curve + ""), null);
  }
}
