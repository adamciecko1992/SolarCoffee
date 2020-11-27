export interface ICustomer {
  id: number;
  createdOn: Date;
  updatedOn: Date;
  firstName: string;
  lastName: string;
  primaryAdress: ICustomerAdress;
}

export interface ICustomerAdress {
  id?: number;
  createdOn?: Date;
  updatedOn?: Date;
  adressLine1?: string;
  adressLine2?: string;
  city?: string;
  state?: string;
  postalCode?: string;
  country?: string;
}
