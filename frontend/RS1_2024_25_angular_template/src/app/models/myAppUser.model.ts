
import { City } from "./city.model";

export class MyAppUser {
    constructor(
        public id: number,
        public city: City,
        public cityId: number,
        public firstName: string,
        public lastName: string,
        public email: string,
        public username: string,
        public phoneNumber: string,
        public educationLevel: number,
        public preferredMode: string,
        public grade: string
    ) { }

}