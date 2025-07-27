import { MyAppUser } from "./myAppUser.model";

export class Student {
    constructor(
        public id: number,
        public grade: string,
        public educationLevel: number,
        public preferredMode: string,
        public myAppUser: MyAppUser,
    ) { }
}