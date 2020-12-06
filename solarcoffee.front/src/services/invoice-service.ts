import { IInvoice } from "../types/Invoice";
import reachToApi from "../services/reachToApi-service";


export default class InvoiceService {

  public async makeNewInvoice(invoice: IInvoice): Promise<any> {
    const now = new Date();
    invoice.createdOn = now;
    invoice.updatedOn = now;
    const response = await reachToApi<IInvoice>("post", "invoice/", invoice);
    return response;
  }
}
