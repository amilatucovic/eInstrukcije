
import { City } from "./city.model";

export class MyAppUser {
    constructor(
        public id: number,
        public city: City,
        public cityId: number,
        public firstName: string,
        public lastName: string,
        public email: string
    ) { }

    fullDescription(): string {
        return `${this.firstName} ${this.lastName}`;
    }
}