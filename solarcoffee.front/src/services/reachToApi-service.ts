import handleErrors from '@/helpers/handleErrors';
import axios, { AxiosError, AxiosResponse } from 'axios';


const baseAxios = axios.create({
    baseURL: "https://localhost:5001/api/"
})

type HTTPAction = "get" | "post" | "patch" | "delete";


async function reachToApi<T>(method: HTTPAction, url: string, body = {}): Promise<AxiosResponse<T> | AxiosError> {
    let response: AxiosError | AxiosResponse<T>;
    switch (method) {
        case "get":
            try {
                response = await baseAxios.get<T>(url);
            }
            catch (error) {
                response = error as AxiosError;
                handleErrors(response);
            }
            break;
        case "delete":
            try {
                response = await baseAxios.delete<T>(url);
            }
            catch (error) {
                response = error as AxiosError;
                handleErrors(response);
            }
            break;
        case "patch":
            try {
                response = await baseAxios.patch<T>(url, body);
            }
            catch (error) {
                response = error as AxiosError;
                handleErrors(response);
            }
            break;
        case "post":
            try {
                response = await baseAxios.post<T>(url, body);
            }
            catch (error) {
                response = error as AxiosError;
                handleErrors(response);
            }
            break;
        default:
            throw new Error("Request could not be handled");
    }
    return response;
}

export default reachToApi;