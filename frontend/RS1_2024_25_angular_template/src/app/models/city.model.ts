export class City {
    constructor(
        public id: number,
        public name: string,
        public postalCode: string
    ) { }

    fullDescription(): string {
        return `${this.name} (${this.postalCode})`;
    }
}
