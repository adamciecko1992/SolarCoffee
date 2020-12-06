import Axios from 'axios'
import { AxiosResponse, AxiosError } from "axios";

export default function axiosIsValid(apiResponse: AxiosError | AxiosResponse): apiResponse is AxiosResponse {
    return (apiResponse as AxiosResponse).data !== undefined;
}