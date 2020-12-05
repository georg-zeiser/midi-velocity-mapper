export interface Settings {
    midiIn: string;
    midiOut: string;
    selectedCurve: string;
    curves: VelocityCurve[];
}

export interface VelocityCurve {
    name: string;
    map: VelocityMap[];
}

export interface VelocityMap {
    in: number;
    out: number;
}