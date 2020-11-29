import { IProductInventory } from "@/types/Product";
import { IShipment } from "@/types/Shipment";
import { IInventoryTimeline} from "@/types/InventoryGraph";
import axios from "axios";

export default class InventoryService {
  API_URL = process.env.VUE_APP_API_URL;
  public async getInventory(): Promise<IProductInventory[]> {
    const response = await axios.get(`https://localhost:5001/api/inventory/`);
    return response.data;
  }

  public async updateInventoryQuantity(shipment: IShipment) {
    const result = await axios.patch(
      "https://localhost:5001/api/inventory/",
      shipment
    );
    return result.data;
  }


    public async getSnapshotHistory(): Promise<IInventoryTimeline> {
    const result: any = await axios.get(`${this.API_URL}/inventory/snapshot`);
    return result.data;
  }
}
