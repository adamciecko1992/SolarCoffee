import axios from "axios";
import { IInvoice } from "../types/Invoice";

export default class InvoiceService {
  private apiUrl: string;
  constructor() {
    this.apiUrl = "https://localhost:5001/api/invoice";
  }
  public async makeNewInvoice(invoice: IInvoice): Promise<boolean> {
    const now = new Date();
    invoice.createdOn = now;
    invoice.updatedOn = now;
    return axios.post(`${this.apiUrl}/new`, invoice);
  }
}
