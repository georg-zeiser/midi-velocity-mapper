import { Component, OnInit } from '@angular/core';
import { MidiService } from '../services/midi.service';
import { SelectItem } from '../interfaces/select-item';
import { SettingsService } from '../services/settings.service';
import { Settings } from '../interfaces/settings';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styles: []
})
export class MainComponent implements OnInit {

  constructor(private midiService: MidiService,
    private settingsService: SettingsService) { }

  inputDevices: SelectItem[];
  outputDevices: SelectItem[];
  settings: Settings;
  initialized: boolean;

  ngOnInit(): void {
    this.settings = {
      midiIn: "",
      midiOut: "",
      selectedCurve: "",
      curves: []
    }

    Promise.all([
      this.getMidiInputDevices(),
      this.getMidiOutputDevices(),
      this.getSettings()
    ])
      .then(() => {
        setTimeout(() => {
          this.initialized = true;
        });
      });
  }

  getMidiInputDevices() {
    return this.midiService.getMidiInputDevices().toPromise()
      .then(result => {
        var results = result.map(result => {
          return {
            value: result,
            viewValue: result
          }
        })

        this.inputDevices = [{ value: undefined, viewValue: undefined }].concat(results);
      });
  }

  getMidiOutputDevices() {
    return this.midiService.getMidiOutputDevices().toPromise()
      .then(result => {
        var results = result.map(result => {
          return {
            value: result,
            viewValue: result
          }
        })

        this.outputDevices = [{ value: undefined, viewValue: undefined }].concat(results);
      });
  }

  onMidiInputDeviceChanged(device: string, $event: any) {
    if (!this.initialized || !$event.isUserInput) {
      return;
    }

    this.midiService.setMidiInputDevice(device).subscribe({
      next: result => {
        this.saveSettings();
      },
      error: err => { }
    });
  }

  onMidiOutputDeviceChanged(device: string, $event: any) {
    if (!this.initialized || !$event.isUserInput) {
      return;
    }

    this.midiService.setMidiOutputDevice(device).subscribe({
      next: result => {
        this.saveSettings();
      },
      error: err => { }
    });
  }

  getSettings() {
    this.settingsService.getSettings().toPromise()
      .then(result => {
        this.settings = result;
      });
  }

  saveSettings() {
    this.settingsService.saveSettings(this.settings).subscribe({
      next: result => { },
      error: err => { }
    });
  }

  onCurveSelected(curve: string) {
    this.settings.selectedCurve = curve;

    this.midiService.setCurve(this.settings.selectedCurve).subscribe({
      next: result => {
        this.saveSettings();
      },
      error: err => { }
    });
  }
}
