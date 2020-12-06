import { AxiosError } from 'axios';

export default function handleErrors(error: AxiosError) {
    switch (error.response?.status) {
        case 404:
            alert("Resource not found");
            break;
        case 422:
            alert("Provided data is not valid for our server, if error will occure again please contact our support")
            break;
        default:
            throw new Error("Unknown server error");
    }
}