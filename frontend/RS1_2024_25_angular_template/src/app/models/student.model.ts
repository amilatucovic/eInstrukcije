import { City } from "./city.model";
import { MyAppUser } from "./myAppUser.model";

export class Student {
    constructor(
        public id: number,
        public grade: string,
        public educationLevel: number,
        public myAppUser: MyAppUser,
    ) { }
}