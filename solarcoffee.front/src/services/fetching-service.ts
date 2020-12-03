import { ICustomer } from "@/types/Customer";
import { IServiceResponse } from "@/types/ServiceResponse";
import Axios, { AxiosInstance } from "axios";

const baseAxios = Axios.create({
  baseURL: "https://localhost:5001/api",
});

class FetchingService {
  private axios: AxiosInstance;
  constructor() {
    this.axios = baseAxios;
  }

  async fetchData(url: string, method: string, payload = {}) {
    let result: any;
    const { axios } = this;
    switch (method) {
      case "get":
        try {
          result = await axios.get(url);
          return result;
        } catch (error) {
          console.log(error, "catched");
        }
        break;
      case "post":
        break;
      case "patch":
        break;
      case "delete":
        break;
      default:
        return this.axios.get(url);
    }
    this.axios("/Customers");
  }
}

export default FetchingService;
