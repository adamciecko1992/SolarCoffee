import axios, { AxiosError, AxiosResponse } from "axios";
import { ICustomer } from "@/types/Customer";
// import { IServiceResponse } from "@/types/ServiceResponse";
import reachAPI from "./reachToApi-service"
import handleErrors from '@/helpers/handleErrors';
import isValidResponse from "@/helpers/axiosTypeGuard";


export default class CustomerService {
  public async getCustomers(): Promise<AxiosResponse<ICustomer[]> | AxiosError> {
    const result: AxiosResponse<ICustomer[]> | AxiosError = await reachAPI<ICustomer[]>("get", "customer");
    return result;
  }

  public async getCustomerById(id: number): Promise<AxiosResponse<ICustomer> | AxiosError> {
    const result = await reachAPI<ICustomer>("get", `customer/${id}`);
    return result;
  }

  public async addCustomer(newCustomer: ICustomer): Promise<AxiosResponse<ICustomer> | AxiosError> {
    const result = await reachAPI<ICustomer>("post", "customer", newCustomer);
    return result;
  }

  public async deleteCustomer(id: number): Promise<Promise<AxiosResponse<ICustomer> | AxiosError>> {
    const result = await reachAPI<ICustomer>("delete", `customer/${id}`, id);
    return result;
  }
}
