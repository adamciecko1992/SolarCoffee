import handleErrors from '@/helpers/handleErrors';
import axios, { AxiosError, AxiosResponse } from 'axios';

const baseAxios = axios.create({
    baseURL: 'https://localhost:5001/api/',
});

type HTTPAction = 'get' | 'post' | 'patch' | 'delete';

async function reachToApi<T>(method: HTTPAction, url: string, body = {}) {
    switch (method) {
        case 'get':
            return await baseAxios.get<T>(url);
        case 'delete':
            return await baseAxios.delete<T>(url);

        case 'patch':
            return await baseAxios.patch<T>(
                url,
                body
            );
        case 'post':
            return await baseAxios.post<T>(
                url,
                body
            );
        default:
            throw new Error('Request could not be handled');
    }
}

export default reachToApi;
