import { IProductInventory } from "@/types/Product";
import { IShipment } from "@/types/Shipment";
import reachToApi from "./reachToApi-service";
import { AxiosError, AxiosResponse } from "axios";


export default class InventoryService {
  API_URL = process.env.VUE_APP_API_URL;
  public async getInventory(): Promise<AxiosResponse<IProductInventory[]> | AxiosError> {
    const response = await reachToApi<IProductInventory[]>("get", "inventory");
    return response;
  }

  public async updateInventoryQuantity(shipment: IShipment) {
    const response = await reachToApi<IShipment>("patch", "inventory", shipment);
    return response;
  }
}
